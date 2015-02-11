using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IContainer = IoC.Interfaces.IContainer;

namespace IoC
{
    public class IoCDependencyResolver : IDependencyResolver
    {
        private IContainer _container;

        public IoCDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Contains(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }
    }
}
