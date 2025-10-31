using MVC.BLL.ModelVM.Department;
using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.ModelVM.Employee
{
    public class GetEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string File { get; set; }
        public bool IsDeleted { get; set; }
        public GetDepartmentVM? Department { get; set; }


    }
}
