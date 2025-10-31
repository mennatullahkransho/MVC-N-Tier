using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.ModelVM.Employee
{
    public class EditEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int Age { get; set; }
        public IFormFile? Image { get; set; }

        public string? ExistingImage { get; set; }

        public int DepartmentId { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
