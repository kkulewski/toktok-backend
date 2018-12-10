using System.Collections.Generic;

namespace TokTok.Services.Authentication
{
    public class RegisterResult
    {
        public bool Success { get; }
        public List<string> Errors { get; }

        public RegisterResult(bool isSuccess, List<string> errors)
        {
            Success = isSuccess;
            Errors = errors;
        }
    }
}
