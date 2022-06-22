using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class BillRequest
    {
        /// <summary>
        /// Информация о количестве средств и валюте
        /// </summary>
        [JsonPropertyName("amount")]
        public BillAmount Amount { get; }

        /// <summary>
        /// Комментарий который виден на странице платежа
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; }

        /// <summary>
        /// Дата завершения платежа
        /// </summary>
        [JsonPropertyName("expirationDateTime")]
        public DateTime ExpirationDateTime { get; }

        /// <summary>
        /// Информация о пользователе в вашей платежной системе
        /// </summary>
        [JsonPropertyName("customer")]
        public CustomerInfo? Customer { get; }

        /// <summary>
        /// Дополнительные поля
        /// </summary>
        [JsonPropertyName("customFields")]
        public Dictionary<string, string>? CustomFields { get; }

        public BillRequest(BillAmount amount, string? comment, DateTime expirationDateTime, CustomerInfo? customer, Dictionary<string, string>? customFields)
        {
            Amount = amount;
            Comment = comment;
            ExpirationDateTime = expirationDateTime;
            Customer = customer;
            CustomFields = customFields;
        }
    }
}
