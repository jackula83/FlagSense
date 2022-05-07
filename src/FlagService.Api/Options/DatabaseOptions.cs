using Framework2.Application.Core.Env;

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

        public string ConnectionString => $@"Server={Server},{Port}; Database={Name}; User Id={Username}; Password={Password};";

        public static implicit operator DatabaseOptions?(ConfigurationManager configuration)
        {
            var section = configuration.GetSection(OptionName);

            var serverEnvWithFallback = section[nameof(Server)];
            var nameEnvWithFallback = section[nameof(Name)];
            var portEnvWithFallback = section[nameof(Port)];
            var usernameEnvWithFallback = section[nameof(Username)];
            var passwordEnvWithFallback = section[nameof(Password)];

            return new()
            {
                Server = Environment.GetEnvironmentVariable(serverEnvWithFallback) ?? serverEnvWithFallback,
                Name = Environment.GetEnvironmentVariable(nameEnvWithFallback) ?? nameEnvWithFallback,
                Username = Environment.GetEnvironmentVariable(usernameEnvWithFallback) ?? usernameEnvWithFallback,
                Password = Environment.GetEnvironmentVariable(passwordEnvWithFallback) ?? passwordEnvWithFallback,
                Port = int.Parse(Environment.GetEnvironmentVariable(portEnvWithFallback) ?? portEnvWithFallback),
            };
        }
    }
}
