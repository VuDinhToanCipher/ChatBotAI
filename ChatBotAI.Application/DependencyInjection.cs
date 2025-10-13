using ChatBotAI.Application.Services.ConversationServices;
using ChatBotAI.Application.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotAI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
