using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using PasswordServer.Api.Entities;

namespace PasswordServer.Api.Contexts
{
    public class PasswordServerContext : DbContext
    {
        public PasswordServerContext(DbContextOptions<PasswordServerContext> options)
            : base(options)
        {
        }

        public DbSet<PasswordData> Passwords { get; set; }
    }
}
