using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class RefundData
    {
        /// <summary>
        /// Информация о количестве средств и валюте
        /// </summary>
        [JsonPropertyName("amount")]
        public PaymentAmount Amount { get; }

        /// <summary>
        /// Время создания возврата
        /// </summary>
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; }

        /// <summary>
        /// Инеднтификатор возврата в вашей платежной системе
        /// </summary>
        [JsonPropertyName("refundId")]
        public string RefundId { get; }

        /// <summary>
        /// Статус возврата
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; }

        public RefundData(PaymentAmount amount, DateTime dateTime, string refundId, string status)
        {
            Amount = amount;
            DateTime = dateTime;
            RefundId = refundId;
            Status = status;
        }
    }
}
