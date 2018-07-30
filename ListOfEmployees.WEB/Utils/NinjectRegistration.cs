using ListOfEmployees.DAL.Interfaces;
using ListOfEmployees.DAL.Repositories;
using Ninject.Modules;

namespace ListOfEmployees.WEB.Utils
{
    public class NinjectRegistration:NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeesRepository>().To<EmployeesRepository>();
        }
    }
}