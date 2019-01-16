using System;
using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IChannelRepository
    {
        List<Channel> GetAll();

        Channel Get(Func<Channel, bool> condition);

        void Create(Channel channnel);

        void Update(int channelId, Channel channel);

        void Delete(int channelId);
    }
}
