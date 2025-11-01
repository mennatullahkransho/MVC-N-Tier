using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVC.DAL.DataBase
{
    public class MvcDbContext :IdentityDbContext
    {

        public MvcDbContext(DbContextOptions<MvcDbContext>options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;database=MVC_Db;trusted_connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        //}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
