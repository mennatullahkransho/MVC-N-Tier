using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Repo.Abstraction
{
    public interface IDepartmentRepo
    {
        public bool Add(Department department);
        public bool Edit(Department department);
        public bool ToggleStatus(int Id);
        public List<Department> GetAll(Expression<Func<Department, bool>>? Filter = null);
        public Department GetById(int Id);
    }
}
