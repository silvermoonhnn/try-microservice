using System;
using System.Collections.Generic;

namespace notification_services.Application.UseCases.Model
{
    public class DataNotification
    {
        public NoData noData { get; set; }
        public List<NoLogData> noLog { get; set; }
    }

    public class NoData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class NoLogData
    {
        public int Notification_Id { get; set; }
        public int From { get; set; }
        public DateTime ReadAt { get; set; }
        public int Target { get; set; }

    }
}