using System;
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
        public void Contains2()
        {
            var container = new IoCContainer();
            container.Register<ITest, TestObj>();
            Assert.True(container.Contains<ITest>());
        }

        [Test]
        public void Contains3()
        {
            var container = new IoCContainer();
            Assert.False(container.Contains(typeof(ITest)));
        }

        [Test]
        public void Contains4()
        {
            var container = new IoCContainer();
            container.Register<ITest, TestObj>();
            Assert.True(container.Contains(typeof(ITest)));
        }

        [Test]
        public void Register()
        {
            var container = new IoCContainer();
            container.Register<ITest, TestObj>();
            Assert.True(container.Contains<ITest>());
        }

        public void Register2()
        {
            var container = new IoCContainer();
            container.Register(typeof(ITest), typeof(TestObj));
            Assert.True(container.Contains<ITest>());
        }

        [Test]
        public void Retrieve_UnsavedType()
        {
            var container = new IoCContainer();

            try
            {
                container.Resolve<IUnsaved>();
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
            var itest = container.Resolve<ITest>();
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
            var classConsumer = container.Resolve<ComplexClassConsumer>();
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
            var classConsumer = container.Resolve<ComplexClassConsumer>();
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
                container.Resolve<ComplexClassConsumer>();
                Assert.Fail();
            }
            catch (NoValidConstructorException nvce) { }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void RegisterSingleton()
        {
            var container = new IoCContainer();
            container.RegisterSingleton<ITest, TestObj>();
            Assert.True(container.Contains<ITest>());
        }

        [Test]
        public void RegisterSingleton2()
        {
            var container = new IoCContainer();
            container.RegisterSingleton<ITest, TestObj>(new TestObj());
            Assert.True(container.Contains<ITest>());
        }

        [Test]
        public void Resolve_Singleton()
        {
            var container = new IoCContainer();
            container.RegisterSingleton<ITest, TestObj>();
            var test1 = container.Resolve<ITest>();
            var test2 = container.Resolve<ITest>();
            Assert.True(test1 == test2);
        }

        [Test]
        public void Resolve_Singleton2()
        {
            var container = new IoCContainer();
            var test1 = new TestObj();
            container.RegisterSingleton<ITest, TestObj>(test1);
            var test2 = container.Resolve<ITest>();
            Assert.True(test1 == test2);
        }
    }
}
