using System.Linq.Expressions;
using NUnit.Framework;

namespace IoC.Test
{
    [TestFixture]
    public class IoCObjectTests
    {
        [Test]
        public void CreateIoCObjectTest()
        {
            var ioc = new IoCObject();
        }
    }
}
