namespace PasswordServer.Api.Configurations
{
    public class PasswordConfiguration
    {
        public const string Name = "PasswordConfiguration";
        public const string ConnectionString = "PasswordServer";
        public int Length { get; set; } = 10;
        public int ValidTimeInSeconds { get; set; } = 30;
        public bool ResetIfExisting { get; set; } = true;
    }
}
