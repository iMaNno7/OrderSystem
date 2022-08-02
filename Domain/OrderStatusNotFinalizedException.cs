using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    internal class OrderStatusNotFinalizedException : Exception
    {
        public OrderStatusNotFinalizedException()
        {
        }

        public OrderStatusNotFinalizedException(string? message) : base(message)
        {
        }

        public OrderStatusNotFinalizedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderStatusNotFinalizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}