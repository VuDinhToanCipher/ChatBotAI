using ChatBotAI.Domain.Conversations;
using ChatBotAI.Domain.Users;
using ChatBotAI.Infrastructure.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotAI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IConversationRespository, ConversationRespository>();
            services.AddScoped<IMessageRespository, MessageRespository>();

            return services;
        }
    }
}
