using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    /// <summary>
    /// The bill info for Qiwi billing system
    /// </summary>
    public sealed class PaymentData
    {
        /// <summary>
        /// Идентификатор сайта мерчанта в QIWI Кассе
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("siteId")]
        public string SiteId { get; internal set; } = null!;

        /// <summary>
        /// Уникальный идентификатор счета
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("billId")]
        public string BillId { get; internal set; }

        /// <summary>
        ///     The invoice amount info.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("amount")]
        public PaymentAmount Amount { get; internal set; }

        /// <summary>
        /// Данные о статусе счета
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("status")]
        public BillStatus Status { get; internal set; } = null!;

        /// <summary>
        /// Идентификаторы пользователя
        /// </summary>
        [JsonPropertyName("customer")]
        public CustomerInfo? Customer { get; set; }

        /// <summary>
        /// Дополнительные параметры
        /// </summary>
        [JsonPropertyName("customFields")]
        public CustomFields? CustomFields { get; set; }

        /// <summary>
        /// Комментарий который виден на странице платежа
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Url на который будет выполнен редирект после оплаты счета
        /// </summary>
        [JsonPropertyName("successUrl")]
        public Uri? SuccessUrl { get; set; }

        /// <summary>
        /// Дата создания платежа
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("creationDateTime")]
        public DateTime CreationDateTime { get; internal set; }

        /// <summary>
        /// Дата завершения платежа
        /// </summary>
        [JsonPropertyName("expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }

        /// <summary>
        /// Ссылка на созданную платежную форму
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("payUrl")]
        public string PayUrl { get; internal set; } = null!;

        /// <summary>
        /// Создает запрос оплаты на основе данных
        /// </summary>
        public BillRequest CreateBillRequest() 
            => new (Amount, Comment, ExpirationDateTime, Customer, CustomFields);

        public PaymentData(string billId, PaymentAmount amount)
        {
            BillId = billId;
            Amount = amount;
        }
    }
}
