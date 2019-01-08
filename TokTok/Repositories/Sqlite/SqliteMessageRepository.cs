using System;
using System.Collections.Generic;
using System.Linq;
using TokTok.Database;
using TokTok.Models;

namespace TokTok.Repositories.Sqlite
{
    public class SqliteMessageRepository : SqliteRepository, IMessageRepository
    {
        public SqliteMessageRepository(SqliteDbContext context) : base(context)
        {
        }

        public List<Message> GetAll()
        {
            return Context.Messages.ToList();
        }

        public Message Get(Func<Message, bool> condition)
        {
            return Context.Messages.FirstOrDefault(condition);
        }

        public void Create(Message message)
        {
            message.Id = 0;
            Context.Messages.Add(message);
            SaveChanges();
        }

        public void Update(int messageId, Message message)
        {
            var messageToUpdate = Context.Messages.Find(messageId);
            if (messageToUpdate == null)
            {
                return;
            }

            messageToUpdate.Text = message.Text;
            Context.Messages.Update(messageToUpdate);
            SaveChanges();
        }

        public void Delete(int messageId)
        {
            var messageToDelete = Get(x => x.Id == messageId);
            if (messageToDelete == null)
            {
                return;
            }

            Context.Messages.Remove(messageToDelete);
            SaveChanges();
        }
    }
}
