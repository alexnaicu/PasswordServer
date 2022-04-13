using System.ComponentModel.DataAnnotations;

namespace PasswordServer.Api.Models
{
    public class PasswordDtoCreate
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }        
    }
}
