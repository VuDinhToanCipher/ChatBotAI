using ChatBotAI.Domain.Conversations;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Respository
{
    public class MessageRespository : IMessageRespository
    {
        private readonly ApplicationDbContext _context;
        public MessageRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Messages> AddMessageAsync(Messages message)
        {
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

            public async Task<List<Messages>> GetMessagesByConversationAsync(Guid conversationId)
            {
                var value = await _context.Messages
                    .Where(c => c.ConversationId == conversationId)
                    .OrderBy(c => c.CreateAt)
                    .ToListAsync();
                return value;
            }
    }
}
