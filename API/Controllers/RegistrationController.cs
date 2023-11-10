using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<User>> Post(Dictionary<string, string> data)
        {
            if (ApplicationContext.Context.Users.FirstOrDefault(x => x.UserLogin == data["login"]) != null)
            {
                return Conflict($"user с логином { data["login"]} уже существует");
            }

            User new_user = new User()
            {
                IdUser = ApplicationContext.Context.Users.Max(x => x.IdUser) + 1,
                UserPassword = data["password"],
                UserLogin = data["login"],
                UserEmail = data["email"],
                UserRole = 1,
                UserStatus = 1,
                UserName = "",
                UserImage = "",
            };

            ApplicationContext.Context.Users.Add(new_user);
            ApplicationContext.Context.SaveChanges();
            /*return new ObjectResult("user_id: " + new_user.IdUser) { StatusCode = StatusCodes.Status201Created }; */
            return new ObjectResult(new_user) { StatusCode = StatusCodes.Status201Created };

        }
    }
}
