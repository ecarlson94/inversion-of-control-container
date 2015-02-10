
using System;
using System.Collections.Generic;
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

        public T Retrieve<T>()
        {
            T obj;

            if (Contains<T>())
            {
                obj = ConstructObject<T>();
            }
            else
            {
                throw new UnsavedTypeException("The requested type '" + (typeof(T)).ToString() + "' does not exist in the container.");
            }

            return obj;
        }

        private T ConstructObject<T>()
        {
            return (T)Activator.CreateInstance(_container[typeof(T)].ObjectType);
        }
    }
}
