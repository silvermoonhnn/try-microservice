using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Collections.Generic;
using System;
using System.Text;
using notification_services.Domain.Context;
using notification_services.Application.UseCases.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotifications
{
    public class GetsQueryHandler : IRequestHandler<GetsQuery, GetsDto>
    {
        private readonly NotifContext _context;

        public GetsQueryHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<GetsDto> Handle(GetsQuery request, CancellationToken cancellation)
        {
            var data = await _context.Notifs.ToListAsync();
            var result = new List<NoData>();

            foreach (var i in data)
            {
                result.Add(new NoData
                {
                    Id = i.Id,
                    Title = i.Title,
                    Message = i.Message
                });
            }

            return new GetsDto 
            {
                Success = true,
                Message = "Notification successfully retrieved",
                Data = result
            };
        }

        public void Recieve()
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "notification", type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "this", durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue: "this", exchange: "notification", string.Empty);

                Console.WriteLine("Waiting for Message ...");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (sender, a) =>
                {
                    var body = a.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Message recieved {message}");
                    await client.PostAsync("http://localhost/notification", content);
                };

                channel.BasicConsume(queue: "this", autoAck: true, consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}