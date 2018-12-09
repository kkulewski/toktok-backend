using System;
using System.Collections.Generic;
using System.Linq;
using TokTok.Models;

namespace TokTok.Repositories.Mock
{
    public class MockUserRepository : IUserRepository
    {
        private static List<User> _users;

        public MockUserRepository()
        {
            _users = new List<User> {
                new User { Username = "Mario", Password = "p@ssword" }
            };
        }

        public void Create(User newUser)
        {   
            _users.Add(newUser);
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(usr => usr.Id == userId);
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public void Update(int userId, User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
