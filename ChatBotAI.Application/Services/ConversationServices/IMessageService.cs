using ChatBotAI.Application.DTOs.ConversationDTO;
using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Application.Services.ConversationServices
{
    public interface IMessageService
    {
        Task<Messages> AddMessageAsync(MessageDTO message);
        Task<List<MessageDTO>> GetMessagesByConversationAsync(Guid conversationId);
    }
}
