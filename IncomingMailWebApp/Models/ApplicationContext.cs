using Microsoft.EntityFrameworkCore;

namespace IncomingMailWebApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Addressee> Addressees { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
