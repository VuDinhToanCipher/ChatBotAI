using ChatBotAI.Application.DTOs.ConversationDTO;
using ChatBotAI.Domain.Conversations;
namespace ChatBotAI.Application.Services.ConversationServices
{
    public interface IConversationService
    {
        Task<Conversation> AddConversationAsync(ConversationDTO conversation);
        Task<Conversation> ViewConversationAsync(Guid conversationId);
        Task<bool> RemoveConversationAsync(Guid conversationId);
    }
}
