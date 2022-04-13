namespace PasswordServer.Web.External.Models
{
    public class PasswordDtoResult
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public int ValidTimeInSeconds { get; set; }
    }
}
