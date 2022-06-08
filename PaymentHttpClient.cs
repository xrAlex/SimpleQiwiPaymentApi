using QiwiPaymentApi.Exceptions;

namespace QiwiPaymentApi
{
    internal sealed class PaymentHttpClient : IDisposable
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="method">Метод запроса</param>
        /// <param name="url">Ссылка по которой будет направлен запрос</param>
        /// <param name="requestData">Контен который будет передан с запросом</param>
        /// <exception cref="RequestException"></exception>
        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, HttpContent? requestData = null)
        {
            var message = new HttpRequestMessage(method, new Uri(url));

            if (requestData != null)
            {
                message.Content = requestData;
            }

            try
            {
                var response = await _client
                    .SendAsync(message)
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw JsonWrapper.MapToError(response.Content.ReadAsStream());
                }

                return response;
            }
            catch (Exception ex) when (ex is not ResponseException)
            {
                throw new RequestException(ex, message);
            }
        }

        public PaymentHttpClient(string qiwiSecretKey)
        {
            _client = new HttpClient()
            {
                DefaultRequestHeaders =
                {
                    {"Authorization", $"Bearer {qiwiSecretKey}"},
                    {"Accept", "application/json"}
                }
            };
        }

        public void Dispose() 
            => _client.Dispose();
    }
}
