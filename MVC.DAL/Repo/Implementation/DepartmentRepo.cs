using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Repo.Implementation
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly MvcDbContext Db;

        public DepartmentRepo(MvcDbContext Db)
        {
            this.Db = Db;
        }

        public bool Add(Department department)
        {
            try
            {
                var dep = Db.Departments.Add(department);
                Db.SaveChanges();
                if (dep.Entity.Id > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(Department newDepartment)
        {
            try
            {
                var oldDepartment = Db.Departments.Where(d => d.Id == newDepartment.Id).FirstOrDefault();
                if (oldDepartment != null)
                {
                    var result = oldDepartment.Update(newDepartment.Name, newDepartment.Area, "Menna");
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

        public List<Department> GetAll(Expression<Func<Department, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {
                    var result = Db.Departments.Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = Db.Departments.ToList();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Department GetById(int Id)
        {
            try
            {
                var dep = Db.Departments.Where(d => d.Id == Id).FirstOrDefault();
                return dep;
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
                var oldDepartment = Db.Departments.Where(d => d.Id == Id).FirstOrDefault();
                if (oldDepartment != null)
                {
                    var result = oldDepartment.ToggleStatus("Essam");
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
