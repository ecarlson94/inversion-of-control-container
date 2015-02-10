using System;
using IoC.Exceptions;
using IoC.Test.Test_Objects;
using NUnit.Framework;

namespace IoC.Test
{
    [TestFixture]
    public class IoCContainerTests
    {
        private IoCContainer _container;

        public IoCContainerTests()
        {
            _container = new IoCContainer();
        }

        [Test]
        public void ContainerCreateTest()
        {
            Assert.NotNull(_container);
        }

        [Test]
        public void Contains()
        {
            Assert.False(_container.Contains<ITest>());
        }

        [Test]
        public void Register()
        {
            _container.Register<ITest, TestObj>();
            Assert.True(_container.Contains<ITest>());
        }

        [Test]
        public void Retrieve_UnsavedType()
        {
            try
            {
                _container.Retrieve<IUnsaved>();
                Assert.Fail();
            }
            catch (UnsavedTypeException ute) { }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Retrieve()
        {
            _container.Register<ITest, TestObj>();
            var itest = _container.Retrieve<ITest>();
            Assert.NotNull(itest);
            Assert.True(itest.GetType() == typeof(TestObj));
            Assert.True(itest.RunTest() == "Test Phrase");
        }

        [Test]
        public void Retrieve_ComplexClassConsumer()
        {
            _container.Register<ITest, TestObj>();
            _container.Register<ComplexClassConsumer, ComplexClassConsumer>();
            var classConsumer = _container.Retrieve<ComplexClassConsumer>();
            Assert.NotNull(classConsumer.Tester);
        }
    }
}
