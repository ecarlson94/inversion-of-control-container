using System;

namespace IoC.Exceptions
{
    public class UnsavedTypeException : Exception
    {
        public UnsavedTypeException() { }
        public UnsavedTypeException(string message) : base(message) { }
    }
}
