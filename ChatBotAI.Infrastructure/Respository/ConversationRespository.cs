using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Infrastructure.Respository
{
    public class ConversationRespository : IConversationRespository
    {
        private readonly ApplicationDbContext _context;
        public ConversationRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public Task<Conversation> AddConversationAsync(Conversation conversation)
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
