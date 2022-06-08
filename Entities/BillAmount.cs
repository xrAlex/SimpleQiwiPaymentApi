using System.Text.Json.Serialization;

namespace QiwiPaymentApi.Entities
{
    public sealed class BillAmount
    {
        /// <summary>
        /// Сумма платежа
        /// </summary>
        [JsonPropertyName("value")]
        public string MoneyValueDecimal { get; }

        /// <summary>
        /// Тип валюты
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; }

        public BillAmount(string moneyValueDecimal, string currency)
        {
            MoneyValueDecimal = moneyValueDecimal;
            Currency = currency;
        }
    }
}
