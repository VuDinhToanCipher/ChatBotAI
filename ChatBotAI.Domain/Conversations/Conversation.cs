using ChatBotAI.Domain.Users;

namespace ChatBotAI.Domain.Conversations
{
    public class Conversation
    {
        public Guid ConversationId { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public ICollection<Messages> Messages { get; set; } = new List<Messages>();
        public User user { get; set; }
    }
}
