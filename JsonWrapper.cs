using System.Text;
using System.Text.Json;
using QiwiPaymentApi.Entities;
using QiwiPaymentApi.Exceptions;

namespace QiwiPaymentApi
{
    internal static class JsonWrapper
    {
        /// <summary>
        /// Десериализует <see cref="HttpContent"/> контент запроса в казанный тип
        /// </summary>
        /// <param name="response">Контент запроса</param>
        /// <exception cref="SerializerException"></exception>
        public static async Task<T> DeserializeResponseAsync<T>(HttpContent response) where T : class
        {
            var content = await response
                .ReadAsStreamAsync()
                .ConfigureAwait(false);

            try
            {
                var jsonData = await JsonSerializer
                    .DeserializeAsync<T>(content)
                    .ConfigureAwait(false);

                if (jsonData == null)
                {
                    throw new SerializerException("Cant deserialize response data");
                }

                return jsonData;
            }
            catch
            {
                throw MapToError(content);
            }
        }

        /// <summary>
        /// Сериализует объект в <see cref="HttpContent"/>
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="SerializerException"></exception>
        public static HttpContent SerializeRequest(object data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return content;
            }
            catch (Exception ex)
            {
                throw new SerializerException(ex);
            }
        }

        /// <summary>
        /// Создает исключение с данными <see cref="ErrorResponse"/> об ошибке
        /// </summary>
        /// <param name="content"></param>
        public static ResponseException MapToError(Stream content)
        {
            try
            {
                var jsonData = JsonSerializer.Deserialize<ErrorResponse>(content);
                return new ResponseException("Client request error", jsonData);
            }
            catch (Exception ex)
            {
                return new ResponseException(ex);
            }
        }
    }
}
