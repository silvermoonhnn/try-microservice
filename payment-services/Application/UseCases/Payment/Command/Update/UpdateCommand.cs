
using MediatR;
using payment_services.Application.Models;
using payment_services.Domain.Entities;

namespace payment_services.Application.UseCases.Payment.Command.Update
{
    public class UpdateCommand : RequestData<PaymentEn>,IRequest<UpdateCommandDto>
    {
        
    }
}