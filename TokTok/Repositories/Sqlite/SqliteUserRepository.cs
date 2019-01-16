using System;
using System.Collections.Generic;
using System.Linq;
using TokTok.Database;
using TokTok.Models;

namespace TokTok.Repositories.Sqlite
{
    public class SqliteUserRepository : SqliteRepository, IUserRepository
    {
        public SqliteUserRepository(SqliteDbContext context) : base(context)
        {
        }

        public List<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public User Get(Func<User, bool> condition)
        {
            return Context.Users.FirstOrDefault(condition);
        }

        public void Create(User newUser)
        {
            if (Context.Users.FirstOrDefault(x => x.UserName == newUser.UserName) != null)
            {
                // another user with given username already exists
                // this should return some kind of error message rather than void
                return;
            }

            newUser.Id = 0;
            Context.Users.Add(newUser);
            SaveChanges();
        }

        public void Update(int userId, User newUser)
        {
            var userToUpdate = Context.Users.Find(userId);
            if (userToUpdate == null)
            {
                // user with given id does not exist
                return;
            }

            if (Context.Users.Count(x => x.UserName == newUser.UserName) > 1)
            {
                // another user with given username already exists
                return;
            }

            // copy new data
            userToUpdate.UserName = newUser.UserName;
            userToUpdate.Password = newUser.Password;
            userToUpdate.Token = newUser.Token;

            // save changes
            Context.Users.Update(userToUpdate);
            SaveChanges();
        }

        public void Delete(int userId)
        {
            var userToDelete = Context.Users.Find(userId);
            if (userToDelete == null)
            {
                // user with given id does not exist
                return;
            }

            Context.Users.Remove(userToDelete);
            SaveChanges();
        }
    }
}
