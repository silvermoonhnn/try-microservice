using System.Threading;
using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using payment_services.Domain.Context;

namespace payment_services.Application.UseCases.Payment.Queries.GetPayment
{
    public class GetQueryHandler : IRequestHandler<GetQuery, GetDto>
    {
         private readonly PaymentContext _context;

         public GetQueryHandler(PaymentContext context)
         {
             _context = context;
         }

         public async Task<GetDto> Handle(GetQuery request, CancellationToken cancellation)
         {
             var result = await _context.Payments.FirstOrDefaultAsync( i => i.Id == request.Id);

             return new GetDto
             {
                 Success = true,
                 Message = "Payment successfully retrieved",
                 Data = result
             };
         }
    }
   


}