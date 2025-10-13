using System.ComponentModel.DataAnnotations;

namespace ChatBotAI.Domain.Conversations
{
    public class Messages
    {
        [Key]
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public Guid ConversationId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsUser { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
