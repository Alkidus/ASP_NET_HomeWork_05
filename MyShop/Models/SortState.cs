using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public enum SortState
    {
        NameAsc,    // по имени по возрастанию
        NameDesc,   // по имени по убыванию
        CostAsc,     // по возрасту по возрастанию
        CostDesc,    // по возрасту по убыванию
        CompanyAsc, // по компании по возрастанию
        CompanyDesc // по компании по убыванию
    }
}
