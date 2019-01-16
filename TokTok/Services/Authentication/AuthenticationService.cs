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

            if (string.IsNullOrEmpty(user.UserName) || user.UserName.Length < 3 || user.UserName.Length > 20)
            {
                errors.Add("Invalid username.");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 3 || user.UserName.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (_userRepository.Get(x => x.UserName == user.UserName) != null)
            {
                errors.Add("This username is already taken.");
            }

            if (errors.Count > 0)
            {
                return new RegisterResult(false, errors);
            }

            user.Password = HashString(user.Password);
            user.Token = HashString(user.UserName);
            _userRepository.Create(user);
            return new RegisterResult(true, new List<string>());
        }

        public LoginResult Login(User user)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(user.UserName) || user.UserName.Length < 3 || user.UserName.Length > 20)
            {
                errors.Add("Invalid username.");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 3 || user.UserName.Length > 20)
            {
                errors.Add("Invalid password.");
            }

            if (errors.Count > 0)
            {
                return new LoginResult(false, errors, string.Empty, string.Empty);
            }

            var matchingUser = _userRepository.Get(x => x.UserName == user.UserName);
            if (matchingUser == null)
            {
                errors.Add("User does not exist.");
                return new LoginResult(false, errors, string.Empty, string.Empty);
            }
            else if (HashString(user.Password) != matchingUser.Password)
            {
                errors.Add("Incorrect password.");
                return new LoginResult(false, errors, string.Empty, string.Empty);
            }

            return new LoginResult(true, errors, matchingUser.Token, matchingUser.UserName);
        }

        private string HashString(string text)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var textHash = sha1.ComputeHash(Encoding.ASCII.GetBytes(text));
            return Convert.ToBase64String(textHash);
        }
    }
}
