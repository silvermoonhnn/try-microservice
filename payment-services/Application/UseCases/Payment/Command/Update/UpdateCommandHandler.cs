using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using payment_services.Domain.Context;

namespace payment_services.Application.UseCases.Payment.Command.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, UpdateCommandDto>
    {
        private readonly PaymentContext _context;
        
        public UpdateCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<UpdateCommandDto> Handle(UpdateCommand request, CancellationToken cancellation)
        {
            var payment = _context.Payments.Find(request.Data.Attributes.Id);
            
            payment.Order_Id = request.Data.Attributes.Order_Id;
            payment.Transaction_Id = request.Data.Attributes.Transaction_Id;
            payment.Payment_Type = request.Data.Attributes.Payment_Type;
            payment.Gross_Amount = request.Data.Attributes.Gross_Amount;
            payment.Transaction_Time = request.Data.Attributes.Transaction_Time;
            payment.Transaction_Status = request.Data.Attributes.Transaction_Status;

            await _context.SaveChangesAsync(cancellation);

            return new UpdateCommandDto
            {
                Success = true,
                Message = "Payment successfully updated"
            };
        }
    }
}