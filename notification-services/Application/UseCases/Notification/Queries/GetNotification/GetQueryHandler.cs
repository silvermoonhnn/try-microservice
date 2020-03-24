using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_services.Application.UseCases.Model;
using notification_services.Domain.Context;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotification
{
    public class GetQueryHandler : IRequestHandler<GetQuery, GetDto>
    {
         private readonly NotifContext _context;

         public GetQueryHandler(NotifContext context)
         {
             _context = context;
         }

         public async Task<GetDto> Handle(GetQuery request, CancellationToken cancellation)
         {
             var result = await _context.Notifs.FirstAsync(i => i.Id == request.Id);
             var logresult = await _context.Logs.Where(i => i.Notification_Id == request.Id).ToListAsync();
             
             var logList = new List<NoLogData>();
             foreach(var i in logresult)
             {
                 logList.Add(new NoLogData()
                 {
                     Notification_Id = i.Notification_Id,
                     From = i.From,
                     ReadAt = i.ReadAt,
                     Target = i.Target
                 });
             }

             return new GetDto
             {
                 Success = true,
                 Message = "Notification successfully retrieved",
                 Data = new DataNotification()
                 {
                     noData = new NoData()
                     {
                         Id = result.Id,
                         Title = result.Title,
                         Message = result.Message
                     },
                     noLog = logList
                 }
             };
         }
    }
}