namespace ChatBotAI.Domain.Conversations
{
    public interface IMessageRespository
    {
        Task<Messages> AddMessageAsync(Guid conversationId, Messages message);
    }
}
