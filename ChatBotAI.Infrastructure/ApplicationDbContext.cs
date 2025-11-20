using ChatBotAI.Domain.Conversations;
using ChatBotAI.Domain.Users;
using ChatBotAI.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {   
        }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
