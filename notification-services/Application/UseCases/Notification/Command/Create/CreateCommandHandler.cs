using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using System.Net.Mail;
using System.Net;
using notification_services.Domain.Entities;
using notification_services.Domain.Context;

namespace notification_services.Application.UseCases.Notification.Command.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CreateCommandDto>
    {
        private readonly NotifContext _context;

        public CreateCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<CreateCommandDto> Handle(CreateCommand request, CancellationToken cancellation)
        {
            var noList = _context.Notifs.ToList();
            
            var no = new NotifEn
            {
                Title = request.Data.Attributes.Title,
                Message = request.Data.Attributes.Message,
            };

            if (!noList.Any(i => i.Title == request.Data.Attributes.Title))
            {
                _context.Notifs.Add(no);
            }
            await _context.SaveChangesAsync();

            var anotherno = _context.Notifs.First(i => i.Title == request.Data.Attributes.Title);
            foreach (var i in request.Data.Attributes.Targets)
            {
                _context.Logs.Add(new LogsEn
                {
                    Notification_Id = anotherno.Id,
                    Type = request.Data.Attributes.Type,
                    From = request.Data.Attributes.From,
                    Target = i.Id,
                    Email_Destination = i.Email_Destination
                });
                await _context.SaveChangesAsync();
                await SendMail("iniemail@email.com", i.Email_Destination, request.Data.Attributes.Title, request.Data.Attributes.Message);
            }
            await _context.SaveChangesAsync();

            return new CreateCommandDto
            {
                Success = true,
                Message = "Notification successfully created"
            };
        }

        public async Task SendMail(string from, string to, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("b0549629350b6a", "b4ff41cb5cfb79"),
                EnableSsl = true
            };
            await client.SendMailAsync(from, to, subject, body);
            Console.WriteLine("Sent");
        }
    }
}