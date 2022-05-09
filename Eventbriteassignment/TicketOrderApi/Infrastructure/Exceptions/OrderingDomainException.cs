namespace TicketOrderApi.Infrastructure.Exceptions
{
    public class OrderingDomainException : Exception
    {
        public OrderingDomainException() : base() { }

        public OrderingDomainException(string message) : base(message) { }

        public OrderingDomainException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}

