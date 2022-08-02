using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    public class OrderAlreadyDeliveredException : Exception
    {
        public OrderAlreadyDeliveredException()
        {
        }

        public OrderAlreadyDeliveredException(string? message) : base(message)
        {
        }

        public OrderAlreadyDeliveredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderAlreadyDeliveredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}