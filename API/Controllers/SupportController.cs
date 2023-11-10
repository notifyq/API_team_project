using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        [HttpPost]
        [Route("AddSupportRequest")]
        [Authorize(Roles = "Клиент")]
        public async Task<ActionResult<Support>> AddSupportRequest(int user_id, int support_type_id)
        {

            var userId = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            if (user_id != int.Parse(userId))
            {
                return Forbid("Вы не авторизованы для выполнения этой операции");
            }

            User user = ApplicationContext.Context.Users.Find(user_id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            int support_id = 1;
            if (ApplicationContext.Context.Supports.Count() != 0)
            {
                support_id = ApplicationContext.Context.Supports.Max(x => x.SupportId) + 1;
            }

            Support support_request = new Support()
            {
                SupportId = support_id,
                SupportStatus = 7,
                SupportType = support_type_id,
                UserId = user_id,
            };
            ApplicationContext.Context.Supports.Add(support_request);
            ApplicationContext.Context.SaveChanges();

            return new ObjectResult("SupportRequestId: " + support_request.SupportId + "\nStatus: " + ApplicationContext.Context.Statuses.Find(support_request.SupportStatus).StatusName) { StatusCode = StatusCodes.Status201Created };
        }
        [HttpGet]
        [Route("GetSupportRequest")]
        [Authorize(Roles = "Сотрудник службы поддержки, Клиент")]

        public async Task<ActionResult<Support>> GetRequest(int support_request_id)
        {
            Support support_request = ApplicationContext.Context.Supports.Find(support_request_id);
            if (support_request == null)
            {
                return NotFound("Запрос не найден");
            }
            return Ok(support_request);
        }

        [HttpGet]
        [Route("GetSupportRequestsList")]
        [Authorize(Roles = "Сотрудник службы поддержки")]
        public async Task<ActionResult<List<Support>>> GetRequests()
        {
            List<Support> support_list = ApplicationContext.Context.Supports.ToList();
            if (support_list == null)
            {
                return NotFound("Запросы не найдены");
            }
            return Ok(support_list);
        }

        [HttpGet]
        [Route("GetUserSupportRequests")]
        [Authorize(Roles = "Сотрудник службы поддержки, Клиент")]
        public async Task<ActionResult<List<Support>>> GetUserRequests(int user_id)
        {
            if (User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value == "Клиент")
            {
                var userId = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                if (user_id != int.Parse(userId))
                {
                    return Forbid("Вы не авторизованы для выполнения этой операции");
                }
            }
            

            if (ApplicationContext.Context.Users.Find(user_id) == null)
            {
                return NotFound("Пользователь не найден");
            }
            List<Support> user_requests = ApplicationContext.Context.Supports.Where(x => x.UserId == user_id).ToList();
            return Ok(user_requests);

        }

        [HttpPut]
        [Route("ChangeStatusRequest")]
        [Authorize(Roles = "Сотрудник службы поддержки")]
        public async Task<ActionResult<Support>> ChangeStatus(int support_request_id, int support_status_id)
        {
            Support support_request = ApplicationContext.Context.Supports.Find(support_request_id);
            if (support_request == null)
            {
                return NotFound("Запрос не найден");
            }

            switch (support_status_id)
            {
                case 7:
                    support_request.SupportStatus = support_status_id;
                    break;
                case 8:
                    support_request.SupportStatus = support_status_id;
                    break;
                case 9:
                    support_request.SupportStatus = support_status_id;
                    break;
                case 10:
                    support_request.SupportStatus = support_status_id;
                    break;

                default:
                    return NotFound("Статус запроса не найден");
                    break;
            }

            ApplicationContext.Context.SaveChanges();
            return Ok("Статус запроса изменет на " + ApplicationContext.Context.Statuses.Find(support_status_id).StatusName);
        }
        
    }
}
