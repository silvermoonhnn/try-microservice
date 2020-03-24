using System;
using notification_services.Application.Models;
using notification_services.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace notification_services.Application.UseCases.Notification.Command.Update
{
    public class UpdateCommand : RequestData<UpdateLogs>,IRequest<UpdateCommandDto>
    {
        
    }

    public class UpdateLogs
    {
        public int Notification_Id { get; set; }
        public DateTime ReadAt { get; set; }
        public List<Target> Targets { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
    }
}