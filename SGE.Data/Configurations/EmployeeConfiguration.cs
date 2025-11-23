using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.HireDate)
                   .IsRequired();

            builder.Property(e => e.Salary)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // FK Department
            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // FK Position
            builder.HasOne(e => e.Position)
                   .WithMany(p => p.Employees)
                   .HasForeignKey(e => e.PositionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
