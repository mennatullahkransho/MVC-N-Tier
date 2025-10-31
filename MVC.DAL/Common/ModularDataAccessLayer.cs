
namespace MVC.DAL.Common
{
    public static class ModularDataAccessLayer
    {
        public static IServiceCollection AddBusinessInDAl(this IServiceCollection service)
        {
            service.AddScoped<IEmployeeRepo, EmployeeRepo>();
            service.AddScoped<IDepartmentRepo, DepartmentRepo>();
            return service;
        }
    }
}
