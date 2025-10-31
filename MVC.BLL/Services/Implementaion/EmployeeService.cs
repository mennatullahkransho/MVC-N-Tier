using AutoMapper;
using MVC.BLL.Helper;
using MVC.BLL.ModelVM.Employee;
using MVC.DAL.Entities;
using MVC.DAL.Repo.Abstraction;
using MVC.DAL.Repo.Implementation;

namespace MVC.BLL.Services.Implementaion
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employee;
        private readonly IMapper mapper;
        private readonly IDepartmentService departmentService;


        public EmployeeService(IEmployeeRepo employee , IMapper mapper, IDepartmentService departmentService)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.departmentService = departmentService;

        }

        public Response<int> Create(CreateEmployeeVM createEmployee)
        {
            try
            {

                string? imageName = null;
                if (createEmployee.Image != null)
                {
                    imageName = Upload.UploadFile("Files", createEmployee.Image);
                }


                var emp = new Employee(createEmployee.Name,
                    createEmployee.Salary,
                    createEmployee.Age,
                    imageName,
                    createEmployee.DepartmentId ,
                    
                    "Menna");
               

                var added = employee.Add(emp);
                if (!added)
                    return new Response<int>(0, "Could not add employee", true);

                return new Response<int>(emp.Id, null, false);
            }
            catch (Exception ex)
            { 
                return new Response<int>(0, ex.Message, true);
            }
        }

        public Response<bool> Edit(EditEmployeeVM editEmployee)
        {
            try
            {
                var oldemployee = employee.GetById(editEmployee.Id);
                if (oldemployee == null)
                    return new Response<bool>(false, "Employee not found", true);

                if (departmentService != null)
                {
                    var dep = departmentService.GetById(editEmployee.DepartmentId);
                    if (dep == null)
                        return new Response<bool>(false, "Department not found", true);
                }

                string? imageName = null;
                if (editEmployee.Image != null)
                {
                    imageName = Upload.UploadFile("Files", editEmployee.Image);
                }

                var updated = oldemployee.Update(editEmployee.Name, editEmployee.Salary,editEmployee.Age,imageName,editEmployee.DepartmentId, "Mennatullah");
                if (!updated)
                    return new Response<bool>(false, "Update failed - maybe invalid updatedBy", true);

               

                var result = employee.Edit(oldemployee); 
                if (!result)
                    return new Response<bool>(false, "Save failed", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<bool> ToggleStatus(int Id)
        {
            try
            {
                var result = employee.ToggleStatus(Id); 
                if (!result)
                    return new Response<bool>(false, "Toggle failed", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<List<GetEmployeeVM>> GetAll(bool includeDeleted )
        {
            try
            {
                if (includeDeleted)
                {
                    var all = employee.GetAll(null);
                    return new Response<List<GetEmployeeVM>>(mapper.Map<List<GetEmployeeVM>>(all), null, false);
                }
                else
                {
                    var active = employee.GetAll(e => e.IsDeleted == false);
                    return new Response<List<GetEmployeeVM>>(mapper.Map<List<GetEmployeeVM>>(active), null, false);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }
        public Response<GetEmployeeVM> GetById(int Id)
        {
            try
            {
                var emp = employee.GetById(Id);
                if (emp == null)
                    return new Response<GetEmployeeVM>(null, "Employee not found", true);

                var vm = mapper.Map<GetEmployeeVM>(emp);
                return new Response<GetEmployeeVM>(vm, null, false);
            }
            catch (Exception ex)
            {
                return new Response<GetEmployeeVM>(null, ex.Message, true);
            }
        }
        public Response<List<GetEmployeeVM>> GetActiveEmployees()
        {
            try
            {
                var result = employee.GetAll(e => e.IsDeleted == false);
                //List<GetEmployeeVM> map = new List<GetEmployeeVM>();
                //foreach (var item in result)
                //{
                //    map.Add(new GetEmployeeVM { Id = item.Id, Name = item.Name });
                //}
                var map = mapper.Map<List<GetEmployeeVM>>(result);
                return new Response<List<GetEmployeeVM>>(map, null, false);
            }
            catch(Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }

        public Response<List<GetEmployeeVM>> GetNotActiveEmployees()
        {
            try
            {
                var result = employee.GetAll(e => e.IsDeleted == true);
                //List<GetEmployeeVM> map = new List<GetEmployeeVM>();
                //foreach (var item in result)
                //{
                //    map.Add(new GetEmployeeVM { Id = item.Id, Name = item.Name });
                //}
                var map = mapper.Map<List<GetEmployeeVM>>(result);
                return new Response<List<GetEmployeeVM>>(map, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }
    }
}
