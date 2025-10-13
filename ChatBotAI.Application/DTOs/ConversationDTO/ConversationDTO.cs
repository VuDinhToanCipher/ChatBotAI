using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.DTOs.ConversationDTO
{
    public class ConversationDTO
    {
        public Guid ConversationId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public string? Title { get; set; }
        public User user { get; set; }
    }
}
