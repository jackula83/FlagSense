namespace FlagService.Api.Options
{
    internal class DatabaseOptions
    {
        public const string OptionName = "Database";

        public string Server { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
