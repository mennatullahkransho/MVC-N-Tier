
namespace MVC.DAL.Repo.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly MvcDbContext Db;

        public EmployeeRepo(MvcDbContext Db)
        {
            this.Db = Db;
        }
        public bool Add(Employee employee)
        {
            try
            {
                var emp = Db.Employees.Add(employee);
                Db.SaveChanges();
                if (emp.Entity.Id > 0)
                    return true;
                else return false;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(Employee newEmployee)
        {
            try
            {
                var oldEmployee = Db.Employees.Where(a => a.Id == newEmployee.Id).FirstOrDefault();
                if (oldEmployee != null)
                {
                    var result = oldEmployee.Update(newEmployee.Name, newEmployee.Salary,newEmployee.Age,newEmployee.File,newEmployee.DepartmentId, "Menna");
                    if (result)
                    {
                        Db.SaveChanges();
                        return true;
                    }

                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public List<Employee> GetAll(Expression<Func<Employee, bool>>? Filter = null)
        {
            try 
            {
                if(Filter!=null)
                {
                    var result = Db.Employees.Include(a=>a.Department).Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = Db.Employees.Include(a => a.Department).ToList();
                    return result;
                }
            
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public Employee GetById(int Id)
        {
            try
            {
                var emp = Db.Employees.Include(a => a.Department).Where(a => a.Id == Id).FirstOrDefault();
                return emp;
            }
            catch
            {
                throw;
            }
        }

        public bool ToggleStatus(int Id)
        {
            try
            {
                var oldEmployee = Db.Employees.Where(a => a.Id == Id).FirstOrDefault();
                if (oldEmployee != null)
                {
                    var result = oldEmployee.ToggleStatus("Essam");
                    if (result)
                    {
                        Db.SaveChanges();
                        return true;
                    }

                }
                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
