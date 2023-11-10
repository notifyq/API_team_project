using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{user_id}")]
        public async Task<ActionResult<User>> Get(int user_id)
        {

            var user = ApplicationContext.Context.Users
                .Include(u => u.Libraries).FirstOrDefaultAsync(u => u.IdUser == user_id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            List<User> users = ApplicationContext.Context.Users.ToList();
            if (users == null)
            {
                return NotFound("Пользователи не найдены");
            }

            return Ok(users);
        }
        [HttpPut]
        [Route("PutImage")]
        [Authorize]
        public async Task<ActionResult<User>> PutUserImage(int user_id,string image_path)
        {

            User currectUser = GetCurrectUser();
            if (currectUser.IdUser == user_id)
            {
                return Forbid("Вы не авторизованы для выполнения этой операции");
            }

            User user = ApplicationContext.Context.Users.Find(user_id);
            if (user == null) 
            {
                return NotFound("Пользователь не найден");
            }
            user.UserImage = image_path;
            ApplicationContext.Context.SaveChanges();
            return Ok("Изображение обновлено");
        }
        [HttpPut]
        [Route("ChangeStatus")]
        [Authorize]
        public async Task<ActionResult<User>> ChangeStatus(int user_id, bool status)
        {

            User currectUser = GetCurrectUser();
            if (currectUser.IdUser == user_id) 
            {
                return Forbid("Вы не авторизованы для выполнения этой операции");
            }

            User user = ApplicationContext.Context.Users.Find(user_id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }
            
            if (status) 
            {
                user.UserStatus = 2; // online
            }
            else
            {
                user.UserStatus = 1; // offline
            }
            ApplicationContext.Context.SaveChanges();
            return Ok($"Статус изменен на {ApplicationContext.Context.Statuses.Find(user.UserStatus).StatusName}");
        }
        [HttpGet]
        [Route("GetCurrentUserInfo")]
        [Authorize]
        public async Task<ActionResult<User>> GetCurrentUserInfo()
        {
            User currectUser = GetCurrectUser();

            if (currectUser == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(currectUser);
        }
        private User GetCurrectUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    IdUser = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    UserLogin = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    UserEmail = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    UserRoleNavigation = new Role { RoleName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value, }
                };


            }
            return null;
        }
    }
}
