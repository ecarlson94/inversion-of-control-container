﻿using IoC.Test.Test_Interfaces;

namespace IoC.Test.Test_Objects
{
    public class TestObj : ITest
    {
        public string RunTest()
        {
            return "Test Phrase";
        }
    }
}
