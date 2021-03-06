using System;
using System.Runtime.Serialization;

namespace FubuMVC.Core.Validation
{
    [Serializable]
    public class FubuValidationException : Exception
    {
        public FubuValidationException()
        {
        }

        public FubuValidationException(string message) : base(message)
        {
        }

        public FubuValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FubuValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}