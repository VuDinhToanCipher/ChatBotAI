using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Domain.Users
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public ICollection<Conversation> conversations { get; set; } = new List<Conversation>();
    }
}
