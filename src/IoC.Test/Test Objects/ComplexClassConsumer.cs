using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.Test.Test_Objects
{
    public class ComplexClassConsumer
    {
        public ITest Tester { get; private set; } 
        public ComplexClassConsumer(ITest tester)
        {
            Tester = tester;
        }
    }
}
