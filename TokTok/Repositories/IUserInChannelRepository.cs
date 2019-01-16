using System;
using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IUserInChannelRepository
    {
        List<UserInChannel> GetAll();

        UserInChannel Get(Func<UserInChannel, bool> condition);

        void Create(UserInChannel newUserInChannel);

        void Delete(int userInChannelId);
    }
}
