using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.DTOs.ConversationDTO
{
    public class ConversationDTO
    {
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid ConversationId { get; internal set; }
    }
}
