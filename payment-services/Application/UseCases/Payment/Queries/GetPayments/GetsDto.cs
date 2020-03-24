using System.Collections.Generic;
using payment_services.Application.Models;
using payment_services.Domain.Entities;

namespace payment_services.Application.UseCases.Payment.Queries.GetPayments
{
    public class GetsDto : BaseDto
    {
        public IList<PaymentEn> Data { get; set; }
    }
}