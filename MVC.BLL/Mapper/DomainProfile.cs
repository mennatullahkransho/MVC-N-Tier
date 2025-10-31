using AutoMapper;
using MVC.BLL.ModelVM.Department;
using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            CreateMap<Employee, GetEmployeeVM>().ReverseMap();
            CreateMap<Employee, CreateEmployeeVM>().ReverseMap();
            CreateMap<Department, GetDepartmentVM>().ReverseMap();
        }
    }
}
