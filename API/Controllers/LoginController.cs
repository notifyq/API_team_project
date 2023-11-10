using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<User>> Get([FromBody] UserLogin userLogin)
        {

            var user = Authentecate(userLogin);
            /*User user = ApplicationContext.Context.Users.FirstOrDefault(x => x.UserPassword == password && x.UserLogin == login);*/
            if (user == null)
            {
                return new ObjectResult("Неверный логин или пароль") { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else 
            {
                var token = Generate(user);
                return Ok(token);
            }
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.UserLogin),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Role, user.UserRoleNavigation.RoleName),
            };

            var token = new JwtSecurityToken(AuthOptions.ISSUER, AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authentecate(UserLogin userLogin)
        {
            User user = ApplicationContext.Context.Users.Include(u => u.UserRoleNavigation).FirstOrDefault(x => x.UserPassword == userLogin.Password && x.UserLogin == userLogin.Login);
            if (user == null) 
            {
                return null;
            }
            else
            {
                return user;
            }


        }
    }
}
