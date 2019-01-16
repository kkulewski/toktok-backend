using System;
using System.Collections.Generic;
using System.Linq;
using TokTok.Database;
using TokTok.Models;

namespace TokTok.Repositories.Sqlite
{
    public class SqliteUserInChannelRepository : SqliteRepository, IUserInChannelRepository
    {
        public SqliteUserInChannelRepository(SqliteDbContext context) : base(context)
        {
        }

        public List<UserInChannel> GetAll()
        {
            return Context.UserInChannels.ToList();
        }

        public UserInChannel Get(Func<UserInChannel, bool> condition)
        {
            return Context.UserInChannels.FirstOrDefault(condition);
        }

        public void Create(UserInChannel newUserInChannel)
        {
            newUserInChannel.Id = 0;
            Context.UserInChannels.Add(newUserInChannel);
            SaveChanges();
        }

        public void Delete(int userInChannelId)
        {
            var userInChannelToDelete = Get(x => x.Id == userInChannelId);
            if (userInChannelToDelete == null)
            {
                return;
            }

            Context.UserInChannels.Remove(userInChannelToDelete);
            SaveChanges();
        }
    }
}
