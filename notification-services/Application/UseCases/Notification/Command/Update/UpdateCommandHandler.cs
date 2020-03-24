using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using notification_services.Domain.Context;

namespace notification_services.Application.UseCases.Notification.Command.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, UpdateCommandDto>
    {
        private readonly NotifContext _context;
        
        public UpdateCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<UpdateCommandDto> Handle(UpdateCommand request, CancellationToken cancellation)
        {
            var no = _context.Logs.ToList();

            var lo = no.Where(i => i.Notification_Id == request.Data.Attributes.Notification_Id);
            foreach( var i in request.Data.Attributes.Targets)
            {
                var data = lo.First(j => j.Target == i.Id).Id;
                var dt = await _context.Logs.FindAsync(data);
                dt.ReadAt = request.Data.Attributes.ReadAt;
                await _context.SaveChangesAsync(cancellation);
            }
            
            return new UpdateCommandDto
            {
                Success = true,
                Message = "Notification successfully updated"
            };
        }
    }
}