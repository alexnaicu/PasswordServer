namespace PasswordServer.Api.Services
{
    public interface IPasswordGenerator
    {
        string Generate(int length, int nonAlphanumericNumber);
    }
}
