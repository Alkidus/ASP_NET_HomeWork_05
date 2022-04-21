using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        ProductsContext db;
        public HomeController(ProductsContext context)
        {
            this.db = context;

            // добавляем начальные данные
            //if (db.Companies.Count() == 0)
            //{
            //    Company lenovo = new Company { Name = "Lenovo" };
            //    Company sven = new Company { Name = "SVEN" };
            //    Company hp = new Company { Name = "Hewlett-Packard" };
            //    Company dell = new Company { Name = "DELL" };
            //    Company asus = new Company { Name = "ASUS" };

            //    Product pr1 = new Product { Name = "Notebook Lenovo Y1", Company = lenovo, Cost = 999 };
            //    Product pr2 = new Product { Name = "HeadPhone SVEN-03", Company = sven, Cost = 280 };
            //    Product pr3 = new Product { Name = "Notebooh HP WWAD-78", Company = hp, Cost = 980 };
            //    Product pr4 = new Product { Name = "Tablet YZX-99", Company = dell, Cost = 456 };
            //    Product pr5 = new Product { Name = "HeadPhone YAGO-11", Company = sven, Cost = 322 };
            //    Product pr6 = new Product { Name = "Notebook DELL 234-ZZX", Company = dell, Cost = 760 };
            //    Product pr7 = new Product { Name = "Smartphone ASUS ROG4", Company = asus, Cost = 650 };
            //    Product pr8 = new Product { Name = "Notebook ASUS TUF1", Company = asus, Cost = 889 };
            //    Product pr9 = new Product { Name = "Camera Smartcam 221", Company = hp, Cost = 275 };
            //    Product pr10 = new Product { Name = "Notebook Lenovo Y2", Company = lenovo, Cost = 970 };

            //    db.Companies.AddRange(lenovo, sven, hp, dell, asus);
            //    db.Products.AddRange(pr1, pr2, pr3, pr4, pr5, pr6, pr7, pr8, pr9, pr10);
            //    db.SaveChanges();
            //}
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();


                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }



        public async Task<IActionResult> Index(int? company, string name, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 4;

            // фильтрация
            IQueryable<Product> products = db.Products.Include(x => x.Company);

            if (company != null && company != 0)
            {
                products = products.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case SortState.CostAsc:
                    products = products.OrderBy(s => s.Cost);
                    break;
                case SortState.CostDesc:
                    products = products.OrderByDescending(s => s.Cost);
                    break;
                case SortState.CompanyAsc:
                    products = products.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    products = products.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Companies.ToList(), company, name),
                Products = items
            };
            return View(viewModel);
        }
    }
}
