using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    public class OrderItemsNullException : Exception
    {
        public OrderItemsNullException()
        {
        }

        public OrderItemsNullException(string? message) : base(message)
        {
        }

        public OrderItemsNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderItemsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}