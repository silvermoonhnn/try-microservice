using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using payment_services.Domain.Context;

namespace payment_services.Application.UseCases.Payment.Queries.GetPayments
{
    public class GetsQueryHandler : IRequestHandler<GetsQuery, GetsDto>
    {
         private readonly PaymentContext _context;

         public GetsQueryHandler(PaymentContext context)
         {
             _context = context;
         }

         public async Task<GetsDto> Handle(GetsQuery request, CancellationToken cancellation)
         {
            var data = await _context.Payments.ToListAsync();

            return new GetsDto 
            {
                Success = true,
                Message = "Payment successfully retrieved",
                Data = data
            };
         }
    }
   


}