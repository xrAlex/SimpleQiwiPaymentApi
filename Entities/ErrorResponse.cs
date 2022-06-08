using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class ErrorResponse
    {
        /// <summary>
        /// Информация о сервисе котоырй выдал ошибку
        /// </summary>
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; }

        /// <summary>
        /// Пользовательское сообщение
        /// </summary>
        [JsonPropertyName("userMessage")]
        public string UserMessage { get; }

        /// <summary>
        /// Время ошибки
        /// </summary>
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; }

        /// <summary>
        /// TraceID ошибки
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