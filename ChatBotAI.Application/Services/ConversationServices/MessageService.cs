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
        public async Task<Messages> AddMessageAsync(MessageDTO message)
        {
            var sentence = new Messages
            {
                ConversationId = message.ConversationId,
                Content = message.Content,
                IsUser = message.IsUser,
            };
            return await _messageRespository.AddMessageAsync(sentence);
        }

        public async Task<List<MessageDTO>> GetMessagesByConversationAsync(Guid conversationId)
        {
            var message = await _messageRespository.GetMessagesByConversationAsync(conversationId);
            return message.Select(m => new MessageDTO
            {
                MessageId = m.MessageId,
                ConversationId = m.ConversationId,
                Content = m.Content,
                IsUser = m.IsUser,
                CreateAt = m.CreateAt

            }).ToList();      
        }
    }
}
