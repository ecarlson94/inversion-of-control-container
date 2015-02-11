using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using IoC;
using IoC.Interfaces;

namespace IoCSampleMVC
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController controller;

            try
            {
                if (controllerType == null)
                {
                    throw new ArgumentNullException("controllerType");
                }

                if (!typeof (IController).IsAssignableFrom(controllerType))
                {
                    throw new ArgumentException(string.Format(
                        "Type requested is not a controller: {0}",
                        controllerType.Name),
                        "controllerType");
                }

                RegisterController(controllerType);

                controller = MyIoCContainer.Container.Resolve(controllerType) as IController;
            }
            catch
            {
                return null;
            }

            return controller;
        }

        private void RegisterController(Type type)
        {
            if (!MyIoCContainer.Container.Contains(type))
            {
                MyIoCContainer.Container.Register(type, type);
            }
            
        }
    }

    public static class MyIoCContainer
    {
        public static IoCContainer Container { get; set; }
    }
}