using System.Threading;
using System.Threading.Tasks;
using MediatR;
using payment_services.Domain.Context;

namespace payment_services.Application.UseCases.Notification.Command.Delete
{
     public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteCommandDto>
    {
        private readonly PaymentContext _context;

        public DeleteCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<DeleteCommandDto> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.Payments.FindAsync(request.Id);
            _context.Payments.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteCommandDto { Message = "Successfull", Success = true };
        }
    }
}