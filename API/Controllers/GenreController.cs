using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        [HttpPost]
        [Route("AddGenre")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Genre>> AddGenre(string genre_name)
        {
            if (ApplicationContext.Context.Genres.FirstOrDefault(x => x.GenreName == genre_name) != null)
            {
                return Conflict("Жанр уже существует");
            }
            ApplicationContext.Context.Genres.Add(new Genre()
            {
                GenreId = ApplicationContext.Context.Genres.Max(x => x.GenreId) + 1,
                GenreName = genre_name,
            });
            ApplicationContext.Context.SaveChanges();
            return new ObjectResult("Добавлено") { StatusCode = StatusCodes.Status201Created }; ;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            List<Genre> genre_list = ApplicationContext.Context.Genres.ToList();
            if (genre_list == null)
            {
                return NotFound();
            }
            return Ok(genre_list);
        }
        [HttpDelete]
        [Route("DeleteGenre")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Genre>> DeleteGenre(int genre_id)
        {
            try
            {
                Genre genre = ApplicationContext.Context.Genres.Find(genre_id);
                if (genre == null)
                {
                    return BadRequest("Жанр не найден");
                }
                ApplicationContext.Context.Genres.Remove(genre);
                ApplicationContext.Context.SaveChanges();
                return Ok("Жанр удален");
            }
            
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return new ObjectResult("Жанр используется") { StatusCode = StatusCodes.Status403Forbidden };
                throw;
            }
        }
       
    }
}
