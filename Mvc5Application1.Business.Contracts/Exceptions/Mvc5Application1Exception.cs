using System;
using System.Runtime.Serialization;

namespace Mvc5Application1.Business.Contracts.Exceptions
{
    public class Mvc5Application1Exception : Exception
    {
        public string FieldName { get; set; }

        public Mvc5Application1Exception(string message)
            : base(message)
        {
        }

        public Mvc5Application1Exception(string fieldName, string message)
            : base(message)
        {
            FieldName = fieldName;
        }
    }

    public class InvalidImportDataException : Exception, ISerializable
    {
        public InvalidImportDataException()
        { }

        public InvalidImportDataException(string message)
            : base(message) { }

        public InvalidImportDataException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InvalidImportDataException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidImportDataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InvalidImportDataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}