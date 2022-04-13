using System.Threading.Tasks;

using PasswordServer.Web.External.Models;

namespace PasswordServer.Web.External
{
    public interface IPasswordServerClient
    {
        Task<PasswordDtoResult> GetPassword(int userId);
    }
}
