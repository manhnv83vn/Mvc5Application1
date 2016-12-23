using System;

namespace Mvc5Application1.Business.Contracts.Exceptions
{
    public class DuplicateDrawingNumberException : Exception
    {
        public DuplicateDrawingNumberException(string message)
            : base(message)
        {
        }
    }
}