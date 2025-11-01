using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.BLL.ModelVM.Role;

namespace MVC.PL.Controllers
{
    public class RoleController : Controller
    {

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }
        public RoleManager<IdentityRole> RoleManager { get; }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM roleVM)
        {

            var getRoleByName = await RoleManager.FindByNameAsync(roleVM.Name); //Admin
            if (getRoleByName is not { })
            {
                var role = new IdentityRole() { Name = roleVM.Name };
                var result = await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }


            return View(roleVM);
        }
        public IActionResult Index()
        {
            var roles = RoleManager.Roles.ToList();

            var roleVMs = roles.Select(r => new RoleVM
            {

                Name = r.Name
            }).ToList();

            return View(roleVMs);
        }


    }
}
