using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class CustomerInfo
    {
        /// <summary>
        /// E-mail клиента.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Идентификатор аккаунта в вашей платежнной системе
        /// </summary>
        [JsonPropertyName("account")]
        public string? Account { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
