using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        public bool Add(Employee employee);
        public bool Edit(Employee employee);
        public bool ToggleStatus(string Id);
        public List<Employee> GetAll(Expression<Func<Employee, bool>>? Filter = null);
        public Employee GetById(string Id);


    }
}
