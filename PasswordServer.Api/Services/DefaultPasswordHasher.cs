using System;
using Microsoft.AspNetCore.Identity;

namespace PasswordServer.Api.Services
{
    public class DefaultPasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<DummyUserClass> hasher = new PasswordHasher<DummyUserClass>();
        private readonly DummyUserClass user = new DummyUserClass();
        public string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            
            return this.hasher.HashPassword(user, password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException(nameof(hashedPassword));
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            return this.hasher.VerifyHashedPassword(user, hashedPassword, password) == PasswordVerificationResult.Success;
        }

        private class DummyUserClass
        { }
    }
}
