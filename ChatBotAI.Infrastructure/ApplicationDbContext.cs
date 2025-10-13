using ChatBotAI.Domain.Conversations;
using ChatBotAI.Domain.Users;
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
    }
}
