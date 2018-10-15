using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IMessageRepository
    {
        List<Message> Get();

        Message Get(int messageId);

        void Create(Message message);

        void Update(int messageId, Message message);

        void Delete(int messageId);
    }
}
