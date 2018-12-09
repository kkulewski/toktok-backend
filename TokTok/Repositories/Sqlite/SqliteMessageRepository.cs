using System.Collections.Generic;
using System.Linq;
using TokTok.Data;
using TokTok.Models;

namespace TokTok.Repositories.Sqlite
{
    public class SqliteMessageRepository : SqliteRepository, IMessageRepository
    {
        public SqliteMessageRepository(TokTokDbContext context) : base(context)
        {
        }

        public List<Message> Get()
        {
            return Context.Messages.ToList();
        }

        public Message Get(int messageId)
        {
            return Context.Messages.FirstOrDefault(x => x.Id == messageId);
        }

        public void Create(Message message)
        {
            message.Id = 0;
            Context.Messages.Add(message);
            SaveChanges();
        }

        public void Update(int messageId, Message message)
        {
            Context.Messages.Update(message);
            SaveChanges();
        }

        public void Delete(int messageId)
        {
            var messageToDelete = Get(messageId);
            if (messageToDelete != null)
            {
                Context.Messages.Remove(messageToDelete);
            }

            SaveChanges();
        }
    }
}
