using System.Collections.Generic;

namespace ListOfEmployees.WEB.Models
{
    public class IndexViewModel
    {
        public IEnumerable<EmployeeViewModel> EmployeeViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}