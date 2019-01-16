using System;
using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IMessageRepository
    {
        List<Message> GetAll();

        Message Get(Func<Message, bool> condition);

        void Create(Message message);

        void Update(int messageId, Message message);

        void Delete(int messageId);
    }
}
