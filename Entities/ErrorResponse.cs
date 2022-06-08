using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class ErrorResponse
    {
        /// <summary>
        /// ���������� � ������� ������� ����� ������
        /// </summary>
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; }

        /// <summary>
        /// ��� ������
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; }

        /// <summary>
        /// �������� ������
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; }

        /// <summary>
        /// ���������������� ���������
        /// </summary>
        [JsonPropertyName("userMessage")]
        public string UserMessage { get; }

        /// <summary>
        /// ����� ������
        /// </summary>
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; }

        /// <summary>
        /// TraceID ������
        /// </summary>
        [JsonPropertyName("traceId")]
        public string TraceId { get; }

        public ErrorResponse(string serviceName, string errorCode, string description, string userMessage, DateTime dateTime, string traceId)
        {
            ServiceName = serviceName;
            ErrorCode = errorCode;
            Description = description;
            UserMessage = userMessage;
            DateTime = dateTime;
            TraceId = traceId;
        }
    }
}