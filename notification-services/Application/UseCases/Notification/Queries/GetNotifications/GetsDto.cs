using System.Collections.Generic;
using notification_services.Application.Models;
using notification_services.Application.UseCases.Model;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotifications
{
    public class GetsDto : BaseDto
    {
        public List<NoData> Data { get; set; }
    }
}