using System.Collections.Generic;
using System.Linq;
using TokTok.Models;

namespace TokTok.Repositories
{
    public class MockMessageRepository : IMessageRepository
    {
        private static List<Message> _messages;

        public MockMessageRepository()
        {
            _messages = new List<Message>
            {
                new Message {Id = 1, Text = "First message"},
                new Message {Id = 2, Text = "Second message"},
                new Message {Id = 3, Text = "Third message"}
            };
        }

        public List<Message> Get()
        {
            return _messages;
        }

        public Message Get(int messageId)
        {
            return _messages.FirstOrDefault(msg => msg.Id == messageId);
        }

        public void Create(Message message)
        {
            _messages.Add(message);
        }

        public void Update(int messageId, Message newMessage)
        {
            var oldMessage = Get(messageId);
            if (oldMessage != null)
            {
                _messages.Remove(oldMessage);
                _messages.Add(newMessage);
            }
        }

        public void Delete(int messageId)
        {
            var messageToDelete = Get(messageId);
            if (messageToDelete != null)
            {
                _messages.Remove(messageToDelete);
            }
        }
    }
}
