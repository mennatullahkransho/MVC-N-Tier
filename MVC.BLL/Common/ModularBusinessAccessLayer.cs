
using MVC.BLL.Mapper;

namespace MVC.BLL.Common
{
    public static class ModularBusinessAccessLayer
    {
        public static IServiceCollection AddBusinessInBLL(this IServiceCollection service)
        {
            service.AddScoped<IEmployeeService, EmployeeService>();
            service.AddScoped<IDepartmentService, DepartmentService>();
            service.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            return service;
        }
    }
}
