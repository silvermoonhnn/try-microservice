using System.Collections.Generic;
using System.Threading;
using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using notification_services.Application.UseCases.Model;
using notification_services.Domain.Context;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotificationLog
{
    public class GetLogQueryHandler : IRequestHandler<GetLogQuery, GetLogDto>
    {
         private readonly NotifContext _context;

         public GetLogQueryHandler(NotifContext context)
         {
             _context = context;
         }

         public async Task<GetLogDto> Handle(GetLogQuery request, CancellationToken cancellation)
         {
             var result = await _context.Notifs.ToListAsync();
             var logresult = await _context.Logs.ToListAsync();
             
             var noList = new List<DataNotification>();
             
             foreach(var i in result)
             {
                 var logList = new List<NoLogData>();
                 var logs = logresult.Where(h => h.Notification_Id == i.Id);
                 foreach(var h in logs)
                 {
                     logList.Add(new NoLogData{
                         Notification_Id = h.Notification_Id,
                         From = h.From,
                         ReadAt = h.ReadAt,
                         Target = h.Target
                     });
                     
                 }
                 noList.Add(new DataNotification()
                 {
                      noData = new NoData()
                      {
                          Id = i.Id,
                          Title = i.Title,
                          Message = i.Message
                      },
                      noLog = logList
                 });
             }

             return new GetLogDto
             {
                 Success = true,
                 Message = "Notification successfully retrieved",
                 Data = noList
             };
         }
    }
}