using Ninject;
using System;
using System.Reflection;

namespace TCN.WebAPI.App_Start
{
    public static class NinjectConfig
    {

        private static string interfacesNamespace = "STARTUP.Domain.Interfaces."; // Update with the Project Name
        private static string classesNamespace = "STARTUP.Domain.Services."; // Update with the Project Name

        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;

        });


        private static void RegisterServices(KernelBase kernel)
        {
            //kernel.Bind(typeof(IRepository<,>)).To(typeof(Repository<,>));
        }
    }
}