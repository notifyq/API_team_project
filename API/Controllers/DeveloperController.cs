using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Developer>>> GetDevelopers()
        {
            List<Developer> deves = ApplicationContext.Context.Developers.ToList();
            if (deves == null)
            {
                return NotFound();
            }
            return Ok(deves);
        }

        [HttpPost]
        [Route("AddDeveloper")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Developer>> AddDeveloper(string developer_name)
        {
            if (ApplicationContext.Context.Developers.FirstOrDefault(x => x.DeveloperName == developer_name) != null)
            {
                return Conflict("Разработчик уже существует");
            }
            ApplicationContext.Context.Developers.Add(new Developer
            {
                DeveloperId = ApplicationContext.Context.Developers.Max(x => x.DeveloperId) + 1,
                DeveloperName = developer_name,
            });
            ApplicationContext.Context.SaveChanges();
            return new ObjectResult("Добавлено") { StatusCode = StatusCodes.Status201Created }; ;
        }

        [HttpDelete]
        [Route("DeleteDeveloper")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Developer>> DeleteDeveloper(int developer_id)
        {
            try
            {
                Developer developer = ApplicationContext.Context.Developers.Find(developer_id);
                if (developer == null)
                {
                    return NotFound("Разработчик не найден");
                }
                ApplicationContext.Context.Developers.Remove(developer);
                ApplicationContext.Context.SaveChanges();
                return Ok("Удалено");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return new ObjectResult("Разработчик используется") { StatusCode = StatusCodes.Status403Forbidden };
                throw;
            }
        }
    }
}
