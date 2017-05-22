using System;
using System.Runtime.Serialization;

namespace Titan.Framework.Exceptions
{
    [Serializable]
    public class AutomationException : Exception
    {
        public AutomationException()
        {
        }

        public AutomationException(string message)
            : base(message)
        {
        }

        public AutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AutomationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}