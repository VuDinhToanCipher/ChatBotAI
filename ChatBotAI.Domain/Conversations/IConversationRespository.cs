namespace ChatBotAI.Domain.Conversations
{
    public interface IConversationRespository
    {
        Task<Conversation> AddConversationAsync(Conversation conversation);
        Task<Conversation> ViewConversationAsync(Guid conversationId);
        Task<bool> RemoveConversationAsync(Guid conversationId);
    }
}
