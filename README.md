# SimpleQiwiPaymentApi

Простая обертка для P2P платежей Qiwi

Пример использования:

```csharp

using var client = new QiwiClient("Qiwi Secret Key");

var billId = Guid.NewGuid().ToString(); // Уникальный идентификатор платежа
var paymentSum = 123M.ToString(CultureInfo.GetCultureInfo("en-US"));
var amount = new BillAmount(paymentSum, "RUB"); //Количество и тип валюты

var billdata = new PaymentData(billId, amount)
{
    Comment = "Тестовый платеж", //Комментарий к платежу, который виден на странице формы оплаты
    ExpirationDateTime = DateTime.Now.AddMinutes(5), //Дата автоматического завершения платежа
    Customer = new CustomerInfo() //Информация о пользователе
    {
        Account = "12345",
        Email = "grishadestroyer@bk.ru",
        Phone = "+7963111111"
    },
    CustomFields = new Dictionary<string, string>
    {
        { "ApiClient","Test Client" },
        { "Test", "Test123" },
    },
	
	//URL на который будет выполнен переход с платежной формы по завршению платежа
	//Добавляется к PaymentData.payUrl при первом запросе
    SuccessUrl = new Uri("https://husl.ru/") 
};

var bill = await client.CreatePaymentAsync(billdata); //Запрос на создание платежа
var billinfo = await client.GetBillInfoAsync(billId); //Запрос на получение данных платежа
var billStatus = await client.GetPaymentStatusAsync(billId); //Запрос статуса платежа
var canceledBill = await client.CancelPaymentWithResultAsync(billId); //Запрос отмены платежа

```
