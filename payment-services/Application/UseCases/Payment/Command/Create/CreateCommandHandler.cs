using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using payment_services.Domain.Context;
using payment_services.Domain.Entities;
using payment_services.Application.UseCases.Payment.Model;
using payment_services.Application.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace payment_services.Application.UseCases.Payment.Command.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CreateCommandDto>
    {
        private readonly PaymentContext _context;

        public CreateCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<CreateCommandDto> Handle(CreateCommand request, CancellationToken cancellation)
        {
            var payment = new PaymentEn
            {
                Order_Id = request.Data.Attributes.Order_Id,
                Transaction_Id = request.Data.Attributes.Transaction_Id,
                Payment_Type = request.Data.Attributes.Payment_Type,
                Gross_Amount = request.Data.Attributes.Gross_Amount,
                Transaction_Time = request.Data.Attributes.Transaction_Time,
                Transaction_Status = request.Data.Attributes.Transaction_Status
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellation);

            var pay = _context.Payments.First(i => i.Order_Id == request.Data.Attributes.Order_Id);
            var target = new Target()
            {
                Id = pay.Id, EmailDestination = "iniemail@email.com"
            };

            var po = new PaymentData()
            {
                Title = "This is the title of the massage",
                Message = "The massage is nothing, just this",
                Type = "Email",
                From = 1,
                Targets = new List<Target>() { target } 
            };

            var attributes = new Data<PaymentData>() { Attributes = po};
            var httpContent = new RequestData<PaymentData>() { Data = attributes };
            var convert = JsonConvert.SerializeObject(httpContent);

            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "notification", type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "this", durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue: "this", exchange: "notification", string.Empty);
                
                var body = Encoding.UTF8.GetBytes(convert);
                channel.BasicPublish(exchange: "notification", routingKey: "", basicProperties: null, body: body);

                Console.WriteLine($"Message has sent");
            }
            
            return new CreateCommandDto
            {
                Success = true,
                Message = "Payment successfully created"
            };

        }
    }
}