using ChatBotAI.Application.DTOs.ConversationDTO;
using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Application.Services.ConversationServices
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRespository _conversationRespository;
        public ConversationService(IConversationRespository _conversationRespository)
        {
            this._conversationRespository = _conversationRespository;
        }
        public Task<Conversation> AddConversationAsync(ConversationDTO conversation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveConversationAsync(Guid conversationId)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation> ViewConversationAsync(Guid conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
