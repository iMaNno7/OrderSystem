using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class OrderItemCountException : Exception
    {
        public OrderItemCountException()
        {
        }

        public OrderItemCountException(string? message) : base(message)
        {
        }

        public OrderItemCountException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderItemCountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}