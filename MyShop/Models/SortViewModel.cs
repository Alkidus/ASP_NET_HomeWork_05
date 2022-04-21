using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени
        public SortState CostSort { get; private set; }    // значение для сортировки по возрасту
        public SortState CompanySort { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;
            CompanySort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            Current = sortOrder;
        }
    }
}