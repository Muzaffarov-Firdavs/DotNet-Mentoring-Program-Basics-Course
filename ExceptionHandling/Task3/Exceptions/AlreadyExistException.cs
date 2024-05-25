using System;

namespace Task3.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base("This task is already exist") { }
    }
}
