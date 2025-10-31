using MVC.BLL.ModelVM.Employee;
using MVC.BLL.ModelVM.ResultResponse;


namespace MVC.BLL.Services.Abstraction
{
    public interface IEmployeeService
    {
        Response<List<GetEmployeeVM>>GetActiveEmployees();
        Response<List<GetEmployeeVM>> GetNotActiveEmployees();
        Response<bool> ToggleStatus(int Id);
        Response<GetEmployeeVM> GetById(int Id);
        Response<List<GetEmployeeVM>> GetAll(bool includeDeleted );
        Response<int> Create(CreateEmployeeVM createEmployee); 
        Response<bool> Edit(EditEmployeeVM editEmployee);


    }
}
