namespace PasswordServer.Api.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
