using System;
using IoC.Enums;
using NUnit.Framework;

namespace IoC.Test
{
    [TestFixture]
    public class IoCObjectTests
    {
        [Test]
        public void CreateIoCObjectTest()
        {
            var ioc = new IoCObject(typeof(String));
            Assert.NotNull(ioc);
        }

        [Test]
        public void IoCObjectTypeTest()
        {
            var ioc = new IoCObject(typeof(String));
            Assert.True(ioc.ObjectType == typeof(String));
        }

        [Test]
        public void DefaultLifestyleTypeTest()
        {
            var ioc = new IoCObject(typeof(String));
            Assert.True(ioc.Lifestyle == LifestyleType.Transient);
        }

        [Test]
        public void LifestyleTypeOverride()
        {
            var ioc = new IoCObject(typeof(String), LifestyleType.Singleton);
            Assert.True(ioc.Lifestyle == LifestyleType.Singleton);
        }
    }
}
