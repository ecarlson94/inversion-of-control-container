using System;
using System.Runtime.InteropServices;
using IoC.Exceptions;
using IoC.Test.Test_Interfaces;
using IoC.Test.Test_Objects;
using NUnit.Framework;

namespace IoC.Test
{
    [TestFixture]
    public class IoCContainerTests
    {
        [Test]
        public void ContainerCreateTest()
        {
            var container = new IoCContainer();
            Assert.NotNull(container);
        }

        [Test]
        public void Contains()
        {
            var container = new IoCContainer();
            Assert.False(container.Contains<ITest>());
        }

        [Test]
        public void Register()
        {
            var container = new IoCContainer();
            container.Register<ITest, TestObj>();
            Assert.True(container.Contains<ITest>());
        }

        [Test]
        public void Retrieve_UnsavedType()
        {
            var container = new IoCContainer();

            try
            {
                container.Resovle<IUnsaved>();
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
            var container = new IoCContainer();

            container.Register<ITest, TestObj>();
            var itest = container.Resovle<ITest>();
            Assert.NotNull(itest);
            Assert.True(itest.GetType() == typeof(TestObj));
            Assert.True(itest.RunTest() == "Test Phrase");
        }

        [Test]
        public void Retrieve_ComplexClassConsumer()
        {
            var container = new IoCContainer();
            
            container.Register<ITest, TestObj>();
            container.Register<ComplexClassConsumer, ComplexClassConsumer>();
            var classConsumer = container.Resovle<ComplexClassConsumer>();
            Assert.NotNull(classConsumer.Tester);
            Assert.Null(classConsumer.Saved);
        }

        [Test]
        public void Retrieve_ComplexClassConsumer2()
        {
            var container = new IoCContainer();

            container.Register<ITest, TestObj>();
            container.Register<ISaved, Saved>();
            container.Register<ComplexClassConsumer, ComplexClassConsumer>();
            var classConsumer = container.Resovle<ComplexClassConsumer>();
            Assert.NotNull(classConsumer.Tester);
            Assert.NotNull(classConsumer.Saved);
        }

        [Test]
        public void Retrieve_NoValidConstructor()
        {
            var container = new IoCContainer();
            container.Register<ComplexClassConsumer, ComplexClassConsumer>();

            try
            {
                container.Resovle<ComplexClassConsumer>();
                Assert.Fail();
            }
            catch (NoValidConstructorException nvce) { }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
