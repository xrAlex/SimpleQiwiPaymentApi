# SimpleQiwiPaymentApi

Простой оберка для P2P платежей Qiwi

Пример использования:

```csharp

using var client = new QiwiClient("Qiwi Secret Code");
var billId = Guid.NewGuid().ToString(); // Уникальный идентификатор платежа
var paymentSum = 123M.ToString(CultureInfo.GetCultureInfo("en-US"));
var amount = new PaymentAmount(paymentSum,"RUB") //Количество и тип валюты

var billdata = new PaymentData(billid, amount)
{
    Comment = "Тестовый платеж", //Комментарий к платежу, который виден на странице формы оплаты
    ExpirationDateTime = DateTime.Now.AddMinutes(5), //Дата автоматического завершения платежа
    Customer = new CustomerInfo() //Информация о пользователе
    {
        Account = "12345",
        Email = "grishadestroyer@bk.ru",
        Phone = "+7963111111"
    },
    CustomFields = new CustomFields()
    {
        ApiClient = "Test Client",
        ApiClientVersion = "v1",
        ThemeCode = "default"
    },
    SuccessUrl = new Uri("https://husl.ru/") //URL на который будет выполнен переход с платежной формы по завршению платежа
};

var bill = await client.CreatePaymentAsync(billdata); //Запрос на создание платежа
var billinfo = await client.GetBillInfoAsync(billId); //Запрос на получение данных платежа
var billStatus = await client.GetPaymentStatusAsync(billId); //Запрос статуса платежа
var canceledBill = await client.CancelPaymentWithResultAsync(billId); //Запрос отмены платежа
```
