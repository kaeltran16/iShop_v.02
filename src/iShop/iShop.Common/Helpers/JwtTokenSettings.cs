using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace iShop.Common.Helpers
{
    public class JwtTokenSettings
    {
        public string Key { get; set; } = string.Empty;
        public int TokenLifeTime { get; set; } = 120;
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; }
        public SigningCredentials SigningCredentials { get; }

        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());

        public JwtTokenSettings()
        {
            ValidFor = TimeSpan.FromMinutes(120);
            if (Key != string.Empty)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
                    SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
