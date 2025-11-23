using Microsoft.EntityFrameworkCore;
using SGE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(d => d.Employees)
                      .WithOne(e => e.Department)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Positions");

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(p => p.Employees)
                      .WithOne(e => e.Position)
                      .HasForeignKey(e => e.PositionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.HireDate)
                    .IsRequired();

                entity.Property(e => e.Salary)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Recursos Humanos" },
                new Department { Id = 2, Name = "Tecnología" },
                new Department { Id = 3, Name = "Finanzas" },
                new Department { Id = 4, Name = "Marketing" }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Desarrollador" },
                new Position { Id = 2, Name = "Analista" },
                new Position { Id = 3, Name = "Gerente" },
                new Position { Id = 4, Name = "Director" }
            );
        }
    }
}
