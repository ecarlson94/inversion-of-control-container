
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IoC.Enums;
using IoC.Exceptions;
using IoC.Interfaces;

namespace IoC
{
    public class IoCContainer : IContainer
    {
        private Dictionary<Type, IoCObject> _container;

        public IoCContainer()
        {
            _container = new Dictionary<Type, IoCObject>();
        }

        public void Register<Tfrom, Tto>() where Tto : Tfrom
        {
            Register(typeof(Tfrom), typeof(Tto), LifestyleType.Transient);
        }

        public void Register(Type tFrom, Type tTo)
        {
            if (tFrom.IsAssignableFrom(tTo))
            {
                Register(tFrom, tTo, LifestyleType.Transient);
            }
            else
            {
                throw new InvalidCastException("Please ensure that the parameter 'tFrom' is assignable from the parameter 'tTo'");
            }
        }

        public void RegisterSingleton<Tfrom, Tto>() where Tto : Tfrom
        {
            Register(typeof(Tfrom), typeof(Tto), LifestyleType.Singleton);
        }

        public void RegisterSingleton<Tfrom, Tto>(Tto resolved) where Tto : Tfrom
        {
            Register(typeof(Tfrom), typeof(Tto), LifestyleType.Singleton, resolved);
        }

        private void Register(Type tFrom, Type tTo, LifestyleType lifestyle, object singletonOjb = null)
        {
            if (_container.ContainsKey(tFrom))
            {
                _container[tFrom] = new IoCObject(tTo, lifestyle){SingletonObject = singletonOjb};
            }
            else
            {
                _container.Add(tFrom, new IoCObject(tTo, lifestyle){SingletonObject = singletonOjb});
            }
        }

        public bool Contains<T>()
        {
            return _container.ContainsKey(typeof (T));
        }

        public bool Contains(Type type)
        {
            return _container.ContainsKey(type);
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof (T));
        }

        private object ResolveSingleton(Type type)
        {
            var resolvedType = _container[type];

            return resolvedType.SingletonObject ?? (resolvedType.SingletonObject = ResolutionHelper(type));
        }

        public object Resolve(Type type)
        {
            object obj = null;

            if (Contains(type))
            {
                var resolvedType = _container[type];
                switch (resolvedType.Lifestyle)
                {
                    case LifestyleType.Transient:
                        obj = ResolutionHelper(type);
                        break;
                    case LifestyleType.Singleton:
                        obj = ResolveSingleton(type);
                        break;
                }
            }
            else
            {
                throw new UnsavedTypeException("The requested type '" + type.Name + "' does not exist in the container.");
            }

            return obj;
        }

        private object ResolutionHelper(Type type)
        {
            object obj = null;

            var resolvedType = _container[type].ObjectType;
            var constructors = resolvedType.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                if (IsConstructable(constructor))
                {
                    var parameters = constructor.GetParameters();
                    if (parameters.Any())
                    {
                        obj = constructor.Invoke(
                            ResolveParameters(parameters)
                                .ToArray());
                    }
                    else
                    {
                        obj = Activator.CreateInstance(resolvedType);
                    }
                    break;
                }
            }

            if (obj == null)
            {
                throw new NoValidConstructorException("No valid instructor to call on object " + resolvedType.Name);
            }

            return obj;
        }

        private IEnumerable<object> ResolveParameters(
            IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(p => Resolve(p.ParameterType)).ToList();
        }

        private bool IsConstructable(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            return !parameters.Any() || parameters.All(x => _container.ContainsKey(x.ParameterType));
        }
    }
}
