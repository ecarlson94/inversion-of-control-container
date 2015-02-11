using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IoC.Interfaces
{
    public interface IContainer
    {
        void Register<T>();
        void Register(Type type);
        void Register<Tfrom, Tto>() where Tto : Tfrom;
        void Register(Type tFrom, Type tTo);
        void RegisterSingleton<Tfrom, Tto>() where Tto : Tfrom;
        void RegisterSingleton<T>();
        void RegisterSingleton<Tfrom, Tto>(Tto resolved) where Tto : Tfrom;
        void RegisterSingleton<T>(T resolved);
        bool Contains<T>();
        bool Contains(Type type);
        T Resolve<T>();
        object Resolve(Type type);
    }
}
