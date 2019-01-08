using System.Collections.Generic;
using System.Linq;
using TokTok.Database;
using TokTok.Models;

namespace TokTok.Repositories.Sqlite
{
    public class SqliteChannelRepository : SqliteRepository, IChannelRepository
    {
        public SqliteChannelRepository(SqliteDbContext context) : base(context)
        {
        }

        public List<Channel> Get()
        {
            return Context.Channels.ToList();
        }

        public Channel Get(int channelId)
        {
            return Context.Channels.FirstOrDefault(x => x.Id == channelId);
        }

        public void Create(Channel channel)
        {
            channel.Id = 0;
            Context.Channels.Add(channel);
            SaveChanges();
        }

        public void Update(int channelId, Channel channel)
        {
            var channelToUpdate = Context.Channels.Find(channelId);
            if (channelToUpdate == null)
            {
                return;
            }

            channelToUpdate.Name = channel.Name;
            Context.Channels.Update(channelToUpdate);
            SaveChanges();
        }

        public void Delete(int channelId)
        {
            var channelToDelete = Get(channelId);
            if (channelToDelete == null)
            {
                return;
            }

            Context.Channels.Remove(channelToDelete);
            SaveChanges();
        }
    }
}
