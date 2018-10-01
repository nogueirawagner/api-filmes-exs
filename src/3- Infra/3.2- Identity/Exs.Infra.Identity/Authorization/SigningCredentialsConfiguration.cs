using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Exs.Infra.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "nogueira@Wagner151091";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}