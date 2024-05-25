using System;

namespace Task3.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base("Invalid userId") { } 
    }
}
