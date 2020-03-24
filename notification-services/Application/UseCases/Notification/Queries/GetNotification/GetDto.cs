using notification_services.Application.Models;
using notification_services.Application.UseCases.Model;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotification
{
    public class GetDto : BaseDto
    {
        public  DataNotification Data { get; set; }
    }
}