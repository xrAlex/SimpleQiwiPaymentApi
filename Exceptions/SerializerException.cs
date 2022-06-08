using QiwiPaymentApi.Entities;

namespace QiwiPaymentApi.Exceptions
{
    public sealed class SerializerException : Exception
    {
        public ErrorResponse? ErrorResponse { get; }
        public SerializerException(Exception ex) : base(ex.Message, ex) {}

        public SerializerException(Exception ex, ErrorResponse? err) : base(ex.Message, ex)
        {
            ErrorResponse = err;
        }
        public SerializerException(string message) : base(message) {}
    }
}
