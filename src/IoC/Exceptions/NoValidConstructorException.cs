using System;

namespace IoC.Exceptions
{
    public class NoValidConstructorException : Exception
    {
        public NoValidConstructorException() { }
        public NoValidConstructorException(string message) : base(message) { }
    }
}
