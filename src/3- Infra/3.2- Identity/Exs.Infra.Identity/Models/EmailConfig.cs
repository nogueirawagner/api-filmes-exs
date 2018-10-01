using Microsoft.Extensions.Configuration;

namespace Exs.Infra.Identity.Models
{
    public class EmailConfig
    {
        private readonly IConfiguration _configuration;
        public EmailConfig(string host, int port, string username, string password, string from, IConfiguration configuration)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            From = from;
            _configuration = configuration;
        }

        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string From { get; private set; }

        
    }
}
