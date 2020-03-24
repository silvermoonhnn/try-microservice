using System.Collections.Generic;

namespace payment_services.Application.UseCases.Payment.Model
{
    public class PaymentData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public List<Target> Targets { get; set; }
    }
    
    public class Target
    {
        public int Id { get; set; }
        public string EmailDestination { get; set; }
    }
}