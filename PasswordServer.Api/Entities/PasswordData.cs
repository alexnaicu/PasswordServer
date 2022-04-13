using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordServer.Api.Entities
{
    public class PasswordData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public DateTimeOffset ValidFromUtc { get; set; }

        [Required]        
        public DateTimeOffset ValidUntilUtc { get; set; }
    }
}
