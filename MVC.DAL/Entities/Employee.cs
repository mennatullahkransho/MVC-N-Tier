


using Microsoft.AspNetCore.Identity;

namespace MVC.DAL.Entities
{
    public class Employee :IdentityUser
    {
        protected Employee() { }
        public Employee( string name,  double salary, int age,string file,int? DeptId, string? createdBy)
        {
           
            Name = name;
            Salary = salary;
            Age = age;
            File = file;
            DepartmentId = DeptId;
            EmailConfirmed = true;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            IsDeleted = false;
          
        }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public double Salary { get; private set; }
        public string File { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }

        public int? DepartmentId { get; private set; }           
        public Department? Department { get; private set; }
        public bool Update(string name, double salary,int age,string file,int? DeptID, string? updatedBy)
        {
            var user = string.IsNullOrEmpty(updatedBy) ? "System" : updatedBy;

            Name = name;
            Salary = salary;
            Age = age;
            File = file;
            DepartmentId = DeptID;
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = user;

            return true;

        }

        public bool ToggleStatus(string? DeletedUser)
        {

            var user = string.IsNullOrEmpty(DeletedBy) ? "System" : DeletedBy;
            IsDeleted = !IsDeleted;
            DeletedOn = DateTime.Now;
            DeletedBy = user;
                return true;
            
            return false;
        }

    }
}
