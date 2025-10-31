namespace MVC.DAL.Entities
{
    public class Department
    {
        protected Department() { }

        public Department(string name, string area, string? createdBy)
        {
            Name = name;
            Area = area;
            IsDeleted = false;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Area { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }

        public ICollection<Employee>? Employees { get; private set; }

        public bool Update(string name, string area, string? updatedBy)
        {
            var user = string.IsNullOrEmpty(updatedBy) ? "System" : updatedBy;

            Name = name;
                Area = area;
                UpdatedOn = DateTime.Now;
                UpdatedBy = user;
                return true;
            
            return false;
        }

        public bool ToggleStatus(string? deletedUser)
        {
            var user = string.IsNullOrEmpty(deletedUser) ? "System" : deletedUser;

            IsDeleted = !IsDeleted;
                DeletedOn = DateTime.Now;
                DeletedBy = user;
                return true;
            
            return false;
        }

        
    }
}
