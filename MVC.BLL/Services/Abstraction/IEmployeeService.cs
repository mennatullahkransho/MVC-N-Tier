using Microsoft.AspNetCore.Identity;
using MVC.BLL.ModelVM.Account;
using MVC.BLL.ModelVM.Employee;
using MVC.BLL.ModelVM.ResultResponse;


namespace MVC.BLL.Services.Abstraction
{
    public interface IEmployeeService
    {
        Response<List<GetEmployeeVM>>GetActiveEmployees();
        Response<List<GetEmployeeVM>> GetNotActiveEmployees();
        Response<bool> ToggleStatus(string Id);
        Response<GetEmployeeVM> GetById(string Id);
        Response<List<GetEmployeeVM>> GetAll(bool includeDeleted );
        Task<IdentityResult> RegisterEmployeeAsync(RegisterEmployeeVM model);
        Response<string> Create(CreateEmployeeVM createEmployee); 
        Response<bool> Edit(EditEmployeeVM editEmployee);


    }
}
