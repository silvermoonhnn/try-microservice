using Microsoft.EntityFrameworkCore;
using notification_services.Domain.Entities;

namespace notification_services.Domain.Context
{
    public class NotifContext : DbContext
    {
        public NotifContext(DbContextOptions<NotifContext> op) : base(op) {}

        public DbSet<NotifEn> Notifs { get; set; }
        public DbSet<LogsEn> Logs { get; set; } 

        protected override void OnModelCreating(ModelBuilder model)
        {
            model
                .Entity<LogsEn>()
                .HasOne(i => i.Notif)
                .WithMany()
                .HasForeignKey(i => i.Notification_Id);
        }
    }
}