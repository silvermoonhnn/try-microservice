using System.Collections.Generic;
using notification_services.Application.Models;
using notification_services.Application.UseCases.Model;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotificationLog
{
    public class GetLogDto : BaseDto
    {
        public  List<DataNotification> Data { get; set; }
    }
}