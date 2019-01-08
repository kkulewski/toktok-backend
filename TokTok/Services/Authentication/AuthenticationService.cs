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
                errors.Add("Invalid username.");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (_userRepository.Get(x => x.Username == user.Username) != null)
            {
                errors.Add("This username is already taken.");
            }

            if (errors.Count > 0)
            {
                return new RegisterResult(false, errors);
            }

            user.Password = HashPassword(user.Password);
            user.Token = user.Username;
            _userRepository.Create(user);
            return new RegisterResult(true, new List<string>());
        }

        public LoginResult Login(User user)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Invalid username.");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (errors.Count > 0)
            {
                return new LoginResult(false, errors, string.Empty);
            }

            var matchingUser = _userRepository.Get(x => x.Username == user.Username);
            if (matchingUser == null)
            {
                errors.Add("User does not exist.");
                return new LoginResult(false, errors, string.Empty);
            }
            else if (HashPassword(user.Password) != matchingUser.Password)
            {
                errors.Add("Incorrect password.");
                return new LoginResult(false, errors, string.Empty);
            }

            return new LoginResult(true, errors, matchingUser.Token);
        }

        private string HashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var passwordHash = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }
    }
}
