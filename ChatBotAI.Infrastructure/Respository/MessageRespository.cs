using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Infrastructure.Respository
{
    public class MessageRespository : IMessageRespository
    {
        private readonly ApplicationDbContext _context;
        public MessageRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public Task<Messages> AddMessageAsync(Guid conversationId, Messages message)
        {
            throw new NotImplementedException();
        }
    }
}
