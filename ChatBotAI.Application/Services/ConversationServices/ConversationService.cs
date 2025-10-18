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
        public async Task<Conversation> AddConversationAsync(ConversationDTO conversation)
        {
            var conver = new Conversation()
            {
                UserId = conversation.UserId,
                Title = conversation.Title,
            };
            return await _conversationRespository.AddConversationAsync(conver);  
        }

        public async Task<List<Conversation>> GetConversationByUserAsync(Guid UserId)
        {
            return await _conversationRespository.GetConversationByUserAsync(UserId);
        }

        public async Task<bool> RemoveConversationAsync(Guid conversationId)
        {
            return await _conversationRespository.RemoveConversationAsync(conversationId);
        }

        public Task<Conversation> ViewConversationAsync(Guid conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
