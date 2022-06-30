using System;

namespace SMMP.Core.Exceptions
{
    public class NotFoundException : CoreException
    {
        public override int StatusCode => 404;

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
