using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:44348"; // издатель токена
        public const string AUDIENCE = "https://localhost:44348"; // потребитель токена
        public const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
