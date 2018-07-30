using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ListOfEmployees.DAL.Entities;
using ListOfEmployees.DAL.Interfaces;
using ListOfEmployees.WEB.Models;
using ListOfEmployees.WEB.Utils;
using Newtonsoft.Json;

namespace ListOfEmployees.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeesRepository _repository;

        public HomeController(IEmployeesRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll(string sortOrder, string searchString, int pageSize = 5, int pageCurrent = 1)
        {
            var list = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(_repository.GetAll());
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(r => r.FirstName.ToLower().Contains(searchString) ||
                                       r.LastName.ToLower().Contains(searchString));
            }
            var employeesPerPages = list.Skip((pageCurrent - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo(list.Count(), pageSize, pageCurrent);
            switch (sortOrder)
            {
                case "Name desc":
                    employeesPerPages = employeesPerPages.OrderByDescending(e=>e.FirstName);
                    break;
                case "Date":
                    employeesPerPages = employeesPerPages.OrderBy(e=>e.Birthday);
                    break;
                case "Date desc":
                    employeesPerPages = employeesPerPages.OrderByDescending(e=>e.Birthday);
                    break;
                default:
                    employeesPerPages = employeesPerPages.OrderBy(s => s.LastName);
                    break;
            }
            IndexViewModel ivm = new IndexViewModel() {EmployeeViewModels = employeesPerPages, PageInfo = pageInfo};
            return new CustomJsonResult { Data = ivm };
        }
        public ActionResult Add()
        {
            return PartialView("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EmployeeViewModel empVm)
        {
            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<EmployeeViewModel, Employee>(empVm);
                _repository.Create(emp);
                _repository.SaveChanges();
                return PartialView("Success");
            }
            return PartialView(empVm);
        }
        public ActionResult Edit(int id)
        {
            Employee emp = _repository.Get(id);
            var empVm = Mapper.Map<Employee, EmployeeViewModel>(emp);
            if (empVm != null)
            {
                empVm.Birthday = empVm.Birthday.Date;
                return PartialView("Edit", empVm);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel empVm)
        {
            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<EmployeeViewModel, Employee>(empVm);
                _repository.Update(emp);
                _repository.SaveChanges();
                return PartialView("Success");
            }
            return PartialView(empVm);
        }
        public ActionResult Remove(int id)
        {
            Employee emp = _repository.Get(id);
            var empVm = Mapper.Map<Employee, EmployeeViewModel>(emp);
            if (empVm != null)
            {
                return PartialView("Remove", empVm);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Remove")]
        public ActionResult RemoveRecord(int id)
        {
            Employee emp = _repository.Get(id);
            if (emp != null)
            {
                _repository.Delete(id);
                _repository.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}