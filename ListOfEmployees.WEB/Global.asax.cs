using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ListOfEmployees.DAL.Entities;
using ListOfEmployees.WEB.Models;
using ListOfEmployees.WEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace ListOfEmployees.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registration = new NinjectRegistration();
            var kernel = new StandardKernel(registration);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeViewModel>());
           
            ModelValidatorProviders.Providers.Remove(
                ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().First());
        }
    }
}
