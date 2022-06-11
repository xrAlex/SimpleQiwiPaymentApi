using System.Text;
using System.Web;
using QiwiPaymentApi.Entities;

namespace QiwiPaymentApi
{
    public sealed class QiwiClient : IDisposable
    {
        private readonly PaymentHttpClient _client;
        private const string RequestUrl = "https://api.qiwi.com/partner/bill/v1/bills/";

        /// <summary>
        /// Создает платеж в системе оплаты Qiwi 
        /// </summary>
        /// <remarks>Если <see cref="PaymentData.SuccessUrl"/>, то преобразует полученную от сервера QIWI ссылку для редиректа по завершению платежа</remarks>
        /// <param name="paymentData">Данные платежа</param>
        /// <returns><see cref="PaymentData"/> Ответ от сервера</returns>
        public async Task<PaymentData> CreatePaymentAsync(PaymentData paymentData)
        {
            var billRequest = paymentData.CreateBillRequest();
            var requestContent = JsonWrapper.SerializeRequest(billRequest);
            
            var response = await _client
                .SendRequestAsync(HttpMethod.Put, RequestUrl + paymentData.BillId, requestContent)
                .ConfigureAwait(false);

            var data = await JsonWrapper
                .DeserializeResponseAsync<PaymentData>(response.Content)
                .ConfigureAwait(false);

            return paymentData.SuccessUrl != null ? AppendSuccessUrl(data, paymentData.SuccessUrl) : data;
        }

        /// <summary>
        /// Создает возврат платежа в системе оплаты Qiwi
        /// </summary>
        /// <param name="billID">Идентификатор платежа</param>
        /// <param name="refundId">Индекнтификатор возврата платежа</param>
        /// <param name="amount">Сумма возврата</param>
        public async Task<RefundData?> CreateRefundAsync(string billID, string refundId, BillAmount amount)
        {
            var requestContent = JsonWrapper.SerializeRequest(amount);

            var response = await _client
                .SendRequestAsync(HttpMethod.Put, RequestUrl + billID + "/refunds/" + refundId, requestContent)
                .ConfigureAwait(false);

            var refundData = await JsonWrapper
                .DeserializeResponseAsync<RefundData>(response.Content)
                .ConfigureAwait(false);

            return refundData;
        }

        /// <summary>
        /// Возвращает статус платежа в виде строки
        /// </summary>
        /// <param name="billId">Идентификатор платежа</param>
        public async Task<string?> GetPaymentStatusAsync(string billId)
        {
            var responseBillInfo = await GetBillInfoAsync(billId).ConfigureAwait(false);
            var value = responseBillInfo
                .Status
                .Value
                .ToLower();

            return value;
        }

        /// <summary>
        /// Запрашивает данные платежа <see cref="PaymentData"/> с сервера системы оплаты Qiwi
        /// </summary>
        /// <param name="billId">Billing id</param>
        public async Task<PaymentData> GetBillInfoAsync(string billId)
        {
            var response = await _client
                .SendRequestAsync(HttpMethod.Get, RequestUrl + billId)
                .ConfigureAwait(false);

            var paymentData = await JsonWrapper
                .DeserializeResponseAsync<PaymentData>(response.Content)
                .ConfigureAwait(false);

            return paymentData;
        }

        /// <summary>
        /// Запрашивает данные возврат платежа <see cref="RefundData"/> в системе оплаты Qiwi
        /// </summary>
        /// <param name="billId">Идентификатор платежа</param>
        /// <param name="refundId">Индекнтификатор возврата платежа</param>
        public async Task<RefundData> GetRefundInfoAsync(string billId, string refundId)
        {
            var response = await _client
                .SendRequestAsync(HttpMethod.Get, RequestUrl + billId + "/refunds/" + refundId)
                .ConfigureAwait(false);

            var refundData = await JsonWrapper
                .DeserializeResponseAsync<RefundData>(response.Content)
                .ConfigureAwait(false);

            return refundData;
        }

        /// <summary>
        /// Отменяет платеж в системе оплаты Qiwi
        /// </summary>
        /// <param name="billId">Идентификатор платежа</param>
        public async Task CancelPaymentAsync(string billId)
            => await _client
                .SendRequestAsync(HttpMethod.Post, RequestUrl + billId + "/reject", new StringContent("", Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

        /// <summary>
        /// Отменяет платеж в системе оплаты Qiwi
        /// </summary>
        /// <param name="billId">Идентификатор платежа</param>
        /// <returns>Данные платежа <see cref="PaymentData"/></returns>
        public async Task<PaymentData> CancelPaymentWithResultAsync(string billId)
        {
            var response = await _client
                .SendRequestAsync(HttpMethod.Post, RequestUrl + billId + "/reject", new StringContent("", Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            var data = await JsonWrapper
                .DeserializeResponseAsync<PaymentData>(response.Content)
                .ConfigureAwait(false);

            return data;
        }

        private static PaymentData AppendSuccessUrl(PaymentData paymentData, Uri successUrl)
        {
            var uriBuilder = new UriBuilder(paymentData.PayUrl!);
            var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            parameters["successUrl"] = successUrl.ToString();
            uriBuilder.Query = parameters.ToString();
            paymentData.PayUrl = uriBuilder.Uri.ToString();

            return paymentData;
        }

        public QiwiClient(string qiwiSecretKey)
        {
            _client = new PaymentHttpClient(qiwiSecretKey);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
