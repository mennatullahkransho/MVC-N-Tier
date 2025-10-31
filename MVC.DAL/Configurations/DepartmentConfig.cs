using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department").HasKey(d => d.Id);
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(d => d.Area)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    
    }
}
