using Microsoft.AspNetCore.Mvc;
using MVC.BLL.ModelVM.Department;
using MVC.BLL.ModelVM.ResultResponse;
using MVC.BLL.Services.Abstraction;
using MVC.BLL.Services.Implementaion;

namespace MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var result = departmentService.GetAll(true);
            return View(result);
        }

        public IActionResult GetById(int Id)
        {
            var result = departmentService.GetById(Id);
            if (result == null || result.IsHaveErrorOrNot || result.Result == null)
            {
                TempData["Error"] = result?.ErrorMessage ?? "Department not found";
                return RedirectToAction(nameof(Index));
            }

            return View(result.Result);
        }

        public IActionResult Create()
        {
            return View(new CreateDepartmentVM());
        }

        [ValidateAntiForgeryToken]
        public IActionResult SaveData(CreateDepartmentVM newDepartment)
        {
            if (!ModelState.IsValid)
            {
                return View(newDepartment);
            }

            var resp = departmentService.Create(newDepartment);
            if (resp == null || resp.IsHaveErrorOrNot)
            {
                return View(newDepartment);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            var resp = departmentService.GetById(Id);
            if (resp == null || resp.IsHaveErrorOrNot || resp.Result == null)
                return RedirectToAction(nameof(Index));

            var vm = new EditDepartmentVM
            {
                Id = resp.Result.Id,
                Name = resp.Result.Name,
                Area = resp.Result.Area,
            };

            return View(vm);
        }



        [ValidateAntiForgeryToken]
        public IActionResult SaveEditData(EditDepartmentVM editDepartment)
        {
           
            if (!ModelState.IsValid)
            {
                return View("Edit", editDepartment);
            }

            var resp = departmentService.Edit(editDepartment);
            if (resp == null)
            {
                return View("Edit", editDepartment);
            }

            if (resp.IsHaveErrorOrNot)
            {
                return View("Edit", editDepartment);
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult ToggleStatus(int id)
        {
            var result = departmentService.ToggleStatus(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
