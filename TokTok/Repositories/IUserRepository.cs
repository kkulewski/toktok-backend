using System;
using System.Collections.Generic;
using TokTok.Models;

namespace TokTok.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User Get(Func<User, bool> condition);

        void Create(User newUser);

        void Update(int userId, User newUser);

        void Delete(int userId);
    }
}
