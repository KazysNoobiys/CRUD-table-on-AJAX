using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using ListOfEmployees.DAL.Entities;

namespace ListOfEmployees.DAL.EF
{
    public class EmployeesContext:DbContext
    {
        public EmployeesContext():base("name=EmployeesDbContext")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmploeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //static EmployeesContext()
        //{
        //    Database.SetInitializer<EmployeesContext>(new ListOfEmploeesInitializer());
        //}

        public DbSet<Employee> Employees { get; set; }
    }

    public class EmploeeConfiguration:EntityTypeConfiguration<Employee>
    {
        public EmploeeConfiguration()
        {
            HasKey(employee => employee.Id);
            Property(employee => employee.FirstName).IsRequired().HasMaxLength(50);
            Property(employee => employee.LastName).IsRequired().HasMaxLength(50);
            Property(emploee => emploee.Birthday).IsRequired();
        }
    }

    public class ListOfEmploeesInitializer : DropCreateDatabaseAlways<EmployeesContext>
    {
        protected override void Seed(EmployeesContext context)
        {
            context.Employees.Add(new Employee()
            {
                FirstName = "Андрей",
                LastName = "Смирнов",
                Birthday = DateTime.Parse("1955-10-12")
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Степан",
                LastName = "Ляхов",
                Birthday = DateTime.Parse("1980-07-10")
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Василий",
                LastName = "Кирзачев",
                Birthday = DateTime.Parse("1977-08-05")
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Александр",
                LastName = "Нестеров",
                Birthday = DateTime.Parse("1961-04-28")
            });
            context.SaveChanges();
        }
    }
}