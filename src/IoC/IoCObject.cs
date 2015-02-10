using System;
using IoC.Enums;

namespace IoC
{
    public class IoCObject<T>
    {
        public LifestyleType Lifestyle { get; private set; }
        public Type ObjectType { get; private set; }

        public IoCObject(LifestyleType lifestyle = LifestyleType.Transient)
        {
            Lifestyle = lifestyle;
            ObjectType = typeof(T);
        }
    }
}
