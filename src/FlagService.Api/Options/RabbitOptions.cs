namespace FlagService.Api.Options
{
    internal class RabbitOptions
    {
        public const string OptionName = "RabbitMQ";

        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static implicit operator RabbitOptions?(ConfigurationManager configuration)
        {
            var section = configuration.GetSection(OptionName);

            var hostnameEnv = section[nameof(HostName)];
            var usernameEnv = section[nameof(UserName)];
            var passwordEnv = section[nameof(Password)];

            if (hostnameEnv == null || usernameEnv == null || passwordEnv == null)
                return default;

            return new()
            {
                HostName = Environment.GetEnvironmentVariable(hostnameEnv) ?? string.Empty,
                UserName = Environment.GetEnvironmentVariable(usernameEnv) ?? string.Empty,
                Password = Environment.GetEnvironmentVariable(passwordEnv) ?? string.Empty,
            };
        }
    }
}
