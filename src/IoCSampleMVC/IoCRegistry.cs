using System.Web.Mvc;
using IoC;
using IoCSampleMVC.Controllers;

namespace IoCSampleMVC
{
    public class IoCRegistry
    {
        public static IoCContainer Initialise()
        {
            var container = BuildContainer();
            DependencyResolver.SetResolver(new IoCDependencyResolver(container));
            return container;
        }

        private static IoCContainer BuildContainer()
        {
            var container = new IoCContainer();

            //register controllers here -- next step, figure out how to do this automatically
            container.Register<HomeController>();
            container.Register<AccountController>();

            //register other stuff here
            container.Register<IControllerFactory, ControllerFactory>();

            //set the ControllerFactory's Container
            MyIoCContainer.Container = container;
            return container;
        }
    }
}