using System.Web.Mvc;
using IoC;

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

            //register stuff here
            

            //set the ControllerFactory's Container
            MyIoCContainer.Container = container;
            return container;
        }
    }
}