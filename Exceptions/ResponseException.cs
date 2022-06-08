using System.Net;
using QiwiPaymentApi.Entities;

namespace QiwiPaymentApi.Exceptions
{
    public sealed class ResponseException : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; }
        public ErrorResponse? ErrorResponse { get; }

        public ResponseException(Exception ex) : base(ex.Message, ex) { }

        public ResponseException(string message, ErrorResponse? error) : base(message)
        {
            ErrorResponse = error;
        }

        public ResponseException(string message, HttpStatusCode? code) : base(message)
        {
            HttpStatusCode = code;
        }
    }
}
