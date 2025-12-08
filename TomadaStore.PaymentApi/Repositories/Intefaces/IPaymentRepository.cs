using TomadaStore.Models.Entities;

namespace TomadaStore.PaymentApi.Repositories.Intefaces;

public interface IPaymentRepository
{
    Task VerifyOrder(Sale sale);
}
