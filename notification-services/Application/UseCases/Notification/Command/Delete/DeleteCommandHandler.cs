using System.Threading;
using System.Threading.Tasks;
using MediatR;
using notification_services.Domain.Context;

namespace notification_services.Application.UseCases.Notification.Command.Delete
{
     public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteCommandDto>
    {
        private readonly NotifContext _context;

        public DeleteCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<DeleteCommandDto> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.Notifs.FindAsync(request.Id);
            _context.Notifs.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteCommandDto { Message = "Successfull", Success = true };
        }
    }
}