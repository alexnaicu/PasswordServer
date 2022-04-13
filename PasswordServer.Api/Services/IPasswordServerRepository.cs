using PasswordServer.Api.Entities;

namespace PasswordServer.Api.Services
{
    public interface IPasswordServerRepository
    {
        void AddPassword(PasswordData password);
        bool PasswordExistsForUserId(int userId);
        bool Save();
    }
}
