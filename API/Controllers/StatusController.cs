using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Status>>> GetStatuses()
        {
            List<Status> status_list = ApplicationContext.Context.Statuses.ToList();
            if (status_list.Count == 0)
            {
                return NotFound("Статусы не найдены");
            }
            return Ok(status_list);
        }

        [HttpPost]
        [Route("AddStatus")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Status>> AddStatus(string status_name)
        {
            if (ApplicationContext.Context.Statuses.FirstOrDefault(x => x.StatusName == status_name) != null)
            {
                return Conflict("Статус уже существует");
            }
            ApplicationContext.Context.Statuses.Add(new Status
            {
                StatusId = ApplicationContext.Context.Statuses.Max(x => x.StatusId) + 1,
                StatusName = status_name,
            });
            ApplicationContext.Context.SaveChanges();
            return new ObjectResult("Добавлено") { StatusCode = StatusCodes.Status201Created } ;
        }
        [HttpDelete]
        [Route("DeleteStatus")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Status>> DeleteStatus(int status_id)
        {
            try
            {
                Status status = ApplicationContext.Context.Statuses.Find(status_id);
                if (status == null)
                {
                    return NotFound("Статус не найден");
                }
                ApplicationContext.Context.Statuses.Remove(status);
                ApplicationContext.Context.SaveChanges();
                return Ok("Удалено");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return new ObjectResult("Статус используется") { StatusCode = StatusCodes.Status403Forbidden };
            }
            
        }
    }
}
