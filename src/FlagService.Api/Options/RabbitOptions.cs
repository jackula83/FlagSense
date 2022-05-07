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

            var hostnameEnvValue = section[nameof(HostName)];
            var usernameEnvValue = section[nameof(UserName)];
            var passwordEnvValue = section[nameof(Password)];

            if (hostnameEnvValue == null || usernameEnvValue == null || passwordEnvValue == null)
                return default;

            return new()
            {
                HostName = hostnameEnvValue,
                UserName = usernameEnvValue,
                Password = passwordEnvValue,
            };
        }
    }
}
