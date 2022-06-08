namespace QiwiPaymentApi.Exceptions
{
    public sealed class RequestException : Exception
    {
        public HttpRequestMessage? HttpRequestMessage { get; }

        public RequestException(Exception ex, HttpRequestMessage requestMessage) : base(ex.Message, ex)
        {
            HttpRequestMessage = requestMessage;
        }
    }
}
