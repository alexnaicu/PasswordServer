using System;

namespace PasswordServer.Api.Models
{
    public class PasswordDtoRead
    {
        public string Password { get; set; }
        public int UserId { get; set; }
        public int ValidTimeInSeconds { get; set; }        
        public DateTimeOffset ValidUntilUtc { get; set; }
    }
}
