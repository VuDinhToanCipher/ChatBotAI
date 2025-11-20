using ChatBotAI.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBotAI.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(500); 
            builder.Property(u => u.IsAdmin)
                   .IsRequired(false);

            builder.HasMany(u => u.conversations) 
                   .WithOne(c => c.user)         
                   .HasForeignKey(c => c.UserId) 
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
