using Microsoft.EntityFrameworkCore;
using payment_services.Domain.Entities;

namespace payment_services.Domain.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> op) : base(op) {}

        public DbSet<PaymentEn> Payments { get; set; }
    }
}