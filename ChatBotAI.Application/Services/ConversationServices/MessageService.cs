using ChatBotAI.Application.DTOs.ConversationDTO;
using ChatBotAI.Domain.Conversations;

namespace ChatBotAI.Application.Services.ConversationServices
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRespository _messageRespository;
        public MessageService(IMessageRespository messageRespository)
        {
            this._messageRespository = messageRespository;
        }
        public Task<Messages> AddMessageAsync(Guid conversationId, MessageDTO message)
        {
            throw new NotImplementedException();
        }
    }
}
