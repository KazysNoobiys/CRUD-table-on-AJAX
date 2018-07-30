using System;
using System.Collections.Generic;
using ListOfEmployees.DAL.Entities;

namespace ListOfEmployees.DAL.Interfaces
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        IEnumerable<Employee> Find(Func<Employee, Boolean> predicate);
        void Create(Employee item);
        void Update(Employee item);
        void Delete(int id);
        void SaveChanges();

    }
}