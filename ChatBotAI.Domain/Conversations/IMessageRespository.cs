namespace ChatBotAI.Domain.Conversations
{
    public interface IMessageRespository
    {
        Task<Messages> AddMessageAsync(Messages message);
        Task<List<Messages>> GetMessagesByConversationAsync(Guid conversationId);
    }
}
