using IoC.Test.Test_Interfaces;

namespace IoC.Test.Test_Objects
{
    public class ComplexClassConsumer
    {
        public ITest Tester { get; private set; }
        public ISaved Saved { get; private set; }

        public ComplexClassConsumer(ITest tester)
        {
            Tester = tester;
        }

        public ComplexClassConsumer(ITest tester, ISaved unsaved)
        {
            Tester = tester;
            Saved = unsaved;
        }
    }
}
