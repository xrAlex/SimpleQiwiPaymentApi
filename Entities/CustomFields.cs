using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class CustomFields 
    {
        /// <summary>
        /// Название API
        /// </summary>
        [JsonPropertyName("apiClient")]
        public string? ApiClient { get; set; }

        /// <summary>
        /// Версия API
        /// </summary>
        [JsonPropertyName("apiClientVersion")]
        public string? ApiClientVersion { get; set; }

        /// <summary>
        /// Идентификатор темы стиля платежа
        /// </summary>
        [JsonPropertyName("themeCode")]
        public string? ThemeCode { get; set; }
    }
}
