using System;

namespace Mvc5Application1.Business.Contracts.Exceptions
{
    public class CannotDeleteDataException : Exception
    {
        public CannotDeleteDataException(string message)
            : base(message)
        {
        }
    }
}