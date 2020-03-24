using System;

namespace notification_services.Domain.Entities
{
    public class LogsEn
    {
        public int Id { get; set; }
        public int Notification_Id { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public int Target { get; set; }
        public string Email_Destination { get; set; }
        public DateTime ReadAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        public NotifEn Notif { get; set; }
    }
}