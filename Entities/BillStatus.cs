using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class BillStatus
    {
        /// <summary>
        /// Состоние платежа
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;

        /// <summary>
        /// Время последнего изменения
        /// </summary>
        [JsonPropertyName("changedDateTime")]
        public DateTime? ChangedDateTime { get; set; }
    }
}
