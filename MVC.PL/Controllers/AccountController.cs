using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.BLL.Helper;
using MVC.BLL.ModelVM.Account;
using MVC.BLL.ModelVM.Employee;
using MVC.BLL.Services.Abstraction;
using MVC.BLL.Services.Implementaion;
using MVC.DAL.Entities;

namespace MVC.PL.Controllers
{
    public class AccountController : Controller
    {


        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly SignInManager<Employee> signInManager;
        public AccountController(IEmployeeService employeeService, IDepartmentService departmentService, SignInManager<Employee> signInManager)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            PopulateDepartmentsInViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDepartmentsInViewBag();
                return View(model);
            }
            var result = await employeeService.RegisterEmployeeAsync(model);

            if (result.Succeeded)
            {
                
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Password", error.Description);
            }
            PopulateDepartmentsInViewBag();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        private void PopulateDepartmentsInViewBag()
        {
            var  depsResponse = departmentService.GetActiveDepartments();

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
