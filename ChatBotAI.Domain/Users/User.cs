using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Domain.Users
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string UserName { get; set; }
        public ICollection<Conversation> conversations { get; set; } = new List<Conversation>();
        public bool? IsAdmin { get; set; }
    }
}
