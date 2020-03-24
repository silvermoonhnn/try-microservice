using payment_services.Application.Models;
using payment_services.Domain.Entities;

namespace payment_services.Application.UseCases.Payment.Queries.GetPayment
{
    public class GetDto : BaseDto
    {
        public PaymentEn Data { get; set; }
    }
}