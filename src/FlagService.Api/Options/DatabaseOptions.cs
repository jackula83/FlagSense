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

            var serverEnvValue = section[nameof(Server)];
            var nameEnvValue = section[nameof(Name)];
            var portEnvValue = section[nameof(Port)];
            var usernameEnvValue = section[nameof(Username)];
            var passwordEnvValue = section[nameof(Password)];

            return new()
            {
                Server = serverEnvValue,
                Name = nameEnvValue,
                Username = usernameEnvValue,
                Password = passwordEnvValue,
                Port = int.Parse(portEnvValue),
            };
        }
    }
}
