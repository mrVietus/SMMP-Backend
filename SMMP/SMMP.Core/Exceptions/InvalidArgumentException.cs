using System;

namespace SMMP.Core.Exceptions
{
    public class InvalidArgumentException : CoreException
    {
        public override int StatusCode => 400;

        public InvalidArgumentException(string message)
            : base(message)
        {
        }
    }
}
