using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        User GetUser(int userId);

        void Create(User newUser);

        void Update(int userId, User newUser);

        void Delete(int userId);
    }
}
