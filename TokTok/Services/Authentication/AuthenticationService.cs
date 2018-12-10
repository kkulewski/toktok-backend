using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public RegisterResult Register(User user)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (_userRepository.GetUser(x => x.Username == user.Username) != null)
            {
                errors.Add("This username is already taken.");
            }

            if (errors.Count > 0)
            {
                return new RegisterResult(false, errors);
            }

            user.Password = HashPassword(user.Password);
            _userRepository.Create(user);
            return new RegisterResult(true, new List<string>());
        }

        private string HashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var passwordHash = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }
    }
}
