using System;
using IoC.Enums;

namespace IoC
{
    public class IoCObject
    {
        public LifestyleType Lifestyle { get; private set; }
        public Type ObjectType { get; private set; }

        public object SingletonObject { get; set; }

        public IoCObject(Type type, LifestyleType lifestyle = LifestyleType.Transient)
        {
            Lifestyle = lifestyle;
            ObjectType = type;
        }
    }
}
