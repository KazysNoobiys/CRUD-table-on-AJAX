using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ListOfEmployees.DAL.EF;
using ListOfEmployees.DAL.Entities;
using ListOfEmployees.DAL.Interfaces;

namespace ListOfEmployees.DAL.Repositories
{
    public class EmployeesRepository:IEmployeesRepository
    {
        private readonly EmployeesContext _db;

        public EmployeesRepository()
        {
            _db = new EmployeesContext();
        }
        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees;
        }

        public Employee Get(int id)
        {
            return _db.Employees.Find(id);
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return _db.Employees.Where(predicate).ToList();
        }

        public void Create(Employee item)
        {
            _db.Employees.Add(item);
        }

        public void Update(Employee item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var empl = _db.Employees.Find(id);
            if (empl != null)
                _db.Employees.Remove(empl);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}