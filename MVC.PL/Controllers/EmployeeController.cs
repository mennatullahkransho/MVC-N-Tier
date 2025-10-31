using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.BLL.Helper;
using MVC.BLL.ModelVM.Department;
using MVC.BLL.ModelVM.Employee;
using MVC.BLL.Services.Abstraction;
using MVC.BLL.Services.Implementaion;

namespace MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var result = employeeService.GetAll(true);
            return View(result);
        }
        public IActionResult GetById(int Id)
        {
            var result = employeeService.GetById(Id);
            if (result == null || result.IsHaveErrorOrNot || result.Result == null)
            {
                TempData["Error"] = result?.ErrorMessage ?? "Employee not found";
                return RedirectToAction(nameof(Index));
            }

            return View(result.Result);

        }
        public IActionResult Create()
        {
            PopulateDepartmentsInViewBag();
            return View(new CreateEmployeeVM());
        }
        [ValidateAntiForgeryToken]

        public IActionResult SaveData(CreateEmployeeVM newEmployee)
        {
            if (!ModelState.IsValid)
            {
                PopulateDepartmentsInViewBag();
                return View("create",newEmployee);
            }

            var resp = employeeService.Create(newEmployee);
            if (resp == null || resp.IsHaveErrorOrNot)
            {
                PopulateDepartmentsInViewBag();
                return View("Create",newEmployee);
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int Id)
        {
            var resp = employeeService.GetById(Id);
            if (resp == null || resp.IsHaveErrorOrNot || resp.Result == null)
                return RedirectToAction(nameof(Index));

            var vm = new EditEmployeeVM
            {
                Id = resp.Result.Id,
                Name = resp.Result.Name,
                Age = resp.Result.Age,
                ExistingImage= resp.Result.File,
                Salary = resp.Result.Salary,
                DepartmentId = resp.Result.Department?.Id ?? 0
            };

            PopulateDepartmentsInViewBag();
            return View(vm);
        }

        [ValidateAntiForgeryToken]

        public IActionResult SaveEditData(EditEmployeeVM editEmployee)
        {

            if (!ModelState.IsValid)
            {
                PopulateDepartmentsInViewBag();
                return View("Edit", editEmployee);
            }

            
            var resp = employeeService.Edit(editEmployee);
            if (resp == null || resp.IsHaveErrorOrNot)
            {
                PopulateDepartmentsInViewBag();
                return View("Edit", editEmployee);
            }



            return RedirectToAction(nameof(Index));
        }

        public IActionResult ToggleStatus(int id)
        {
            var result = employeeService.ToggleStatus(id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDepartmentsInViewBag()
        {
            var depsResponse = departmentService.GetActiveDepartments();

            if (depsResponse == null || depsResponse.IsHaveErrorOrNot || depsResponse.Result == null)
            {
                ViewBag.Departments = new List<SelectListItem>();
                return;
            }

            ViewBag.Departments = depsResponse.Result
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.Name} - {d.Area}"
                })
                .ToList();
        }
    }
}
