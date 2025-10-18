using ChatBotAI.Domain.Conversations;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Respository
{
    public class ConversationRespository : IConversationRespository
    {
        private readonly ApplicationDbContext _context;
        public ConversationRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Conversation> AddConversationAsync(Conversation conversation)
        {
            try
            {
                if (conversation == null)
                {
                    throw new ArgumentNullException(nameof(conversation), "Conversation không được null.");
                }

                if (conversation.UserId == null || conversation.UserId == Guid.Empty)
                {
                    throw new ArgumentException("UserId không hợp lệ.", nameof(conversation.UserId));
                }

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Đã lưu hội thoại với ID: {conversation.ConversationId}");
                return conversation;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Lỗi DB khi lưu hội thoại: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu hội thoại: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Conversation>> GetConversationByUserAsync(Guid UserId)
        {
            return await _context.Conversations.Where(c => c.UserId == UserId).OrderByDescending(c => c.CreateAt).ToListAsync();
        }

        public async Task<bool> RemoveConversationAsync(Guid conversationId)
        {
            var result = await _context.Conversations
                .FirstOrDefaultAsync(c => c.ConversationId == conversationId);
            if(result == null)
            {
                return false;
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<Conversation> ViewConversationAsync(Guid conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
