using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetPublishers()
        {
            List<Publisher> publisherList = ApplicationContext.Context.Publishers.ToList();
            if (publisherList.Count == 0)
            {
                return NotFound("Не найдено");
            }
            return Ok(publisherList);
        }

        [HttpPost]
        [Route("AddPublisher")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Publisher>> AddPublisher(string publisher_name)
        {
            if (ApplicationContext.Context.Publishers.FirstOrDefault(x => x.PublisherName == publisher_name) != null)
            {
                return Conflict("Издатель уже существует");
            }

            int id = 1;
            if (ApplicationContext.Context.Publishers.Max(x => x.PublisherId) != 0)
            {
                id = ApplicationContext.Context.Publishers.Max(x => x.PublisherId) + 1;
            }
            ApplicationContext.Context.Publishers.Add(new Publisher
            {
                PublisherId = id,
                PublisherName = publisher_name,
                
            });
            ApplicationContext.Context.SaveChanges();
            return Ok("Добавлено");
        }
        [HttpDelete]
        [Route("DeletePublisher")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Publisher>> DeletePublisher(int publisher_id)
        {
           

            try
            {
                Publisher publisher = ApplicationContext.Context.Publishers.Find(publisher_id);
                if (publisher == null)
                {
                    return NotFound("Издатель не найден");
                }
                ApplicationContext.Context.Publishers.Remove(publisher);
                ApplicationContext.Context.SaveChanges();
                return Ok("Удалено");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return new ObjectResult("Издатель используется") { StatusCode = StatusCodes.Status403Forbidden };
                throw;
            }
            
        }
    }
}
