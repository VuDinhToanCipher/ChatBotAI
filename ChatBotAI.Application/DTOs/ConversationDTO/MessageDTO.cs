namespace ChatBotAI.Application.DTOs.ConversationDTO
{
    public class MessageDTO
    {
        public Guid MessageId { get; set; } 
        public Guid ConversationId { get; set; }
        public string Content { get; set; }
        public bool IsUser { get; set; }
        public DateTime CreateAt { get; set; } 
    }
}
