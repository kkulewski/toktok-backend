using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IChannelRepository
    {
        List<Channel> Get();

        Channel Get(int channelId);

        void Create(Channel channnel);

        void Update(int channelId, Channel channel);

        void Delete(int channelId);
    }
}
