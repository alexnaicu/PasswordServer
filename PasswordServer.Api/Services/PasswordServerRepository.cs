using System;
using System.Collections.Generic;
using System.Linq;

using PasswordServer.Api.Contexts;
using PasswordServer.Api.Entities;

namespace PasswordServer.Api.Services
{
    public class PasswordServerRepository : IPasswordServerRepository, IDisposable
    {
        private PasswordServerContext context;

        public PasswordServerRepository(PasswordServerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddPassword(PasswordData password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            this.context.Passwords.Add(password);
        }        

        public bool PasswordExistsForUserId(int userId)        
            => this.context.Passwords.Any(p => p.UserId == userId && p.ValidFromUtc <= DateTime.UtcNow && p.ValidUntilUtc > DateTime.UtcNow);        

        public bool Save()
            => this.context.SaveChanges() > 0;

        #region IDispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        { 
            if (isDisposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                    this.context = null;
                }
            }
        }

        #endregion IDispose
    }
}