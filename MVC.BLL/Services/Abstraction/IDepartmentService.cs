using MVC.BLL.ModelVM.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Services.Abstraction
{
    public interface IDepartmentService
    {
        Response<List<GetDepartmentVM>> GetActiveDepartments();
        Response<List<GetDepartmentVM>> GetNotActiveDepartments();
        Response<bool> ToggleStatus(int Id);

        Response<GetDepartmentVM> GetById(int Id);
        Response<List<GetDepartmentVM>> GetAll(bool includeDeleted );
        Response<int> Create(CreateDepartmentVM createDepartment);
        Response<bool> Edit(EditDepartmentVM editDepartment);
    }
}
