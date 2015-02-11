
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IoC.Exceptions;

namespace IoC
{
    public class IoCContainer
    {
        private Dictionary<Type, IoCObject> _container;

        public IoCContainer()
        {
            _container = new Dictionary<Type, IoCObject>();
        }

        public void Register<Tfrom, Tto>() where Tto : Tfrom
        {
            if (_container.ContainsKey(typeof (Tfrom)))
            {
                _container[typeof (Tfrom)] = new IoCObject(typeof (Tto));
            }
            else
            {
                _container.Add(typeof(Tfrom), new IoCObject(typeof(Tto)));
            }
        }

        public bool Contains<T>()
        {
            return _container.ContainsKey(typeof (T));
        }

        public T Resovle<T>()
        {
            T obj;

            if (Contains<T>())
            {
                obj = (T) Resolve(typeof (T));
            }
            else
            {
                throw new UnsavedTypeException("The requested type '" + (typeof(T)).ToString() + "' does not exist in the container.");
            }

            return obj;
        }

        private object Resolve(Type type)
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
                throw new NoValidConstructorException("No valid instructor to call on object " + resolvedType);
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
