using System;

namespace payment_services.Domain.Entities
{
    public class PaymentEn
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Transaction_Id { get; set; }
        public string Payment_Type { get; set; }
        public string Gross_Amount { get; set; }
        public string Transaction_Time { get; set; }
        public string Transaction_Status { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
    }
}