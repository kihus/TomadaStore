namespace TomadaStore.PaymentApi.Services.Interfaces;

public interface IPaymentService
{
    Task VerifyOrder();
}