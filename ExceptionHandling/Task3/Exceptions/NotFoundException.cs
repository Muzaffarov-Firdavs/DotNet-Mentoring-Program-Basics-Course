using System;

namespace Task3.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("User not found") { }
    }
}
