using System;

namespace Mvc5Application1.Business.Contracts.Exceptions
{
    public class CannotAmendDataException : Exception
    {
        public CannotAmendDataException(string message)
            : base(message)
        {
        }
    }
}