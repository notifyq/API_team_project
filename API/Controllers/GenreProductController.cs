using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreProductController : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<ProductGenre>> ProductGenreList()
        {
           List<ProductGenre> productGenres = ApplicationContext.Context.ProductGenres.ToList();
            if (productGenres.Count == 0)
            {
                return NotFound();
            }
            return Ok(productGenres);
        }
        [HttpPost]
        [Route("AddGenreToProduct")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<ProductGenre>> AddGenreToProduct(int genre_id, int product_id)
        {
            if (ApplicationContext.Context.Products.Find(product_id) == null)
            {
                return BadRequest("Товар не найден");
            }
            if (ApplicationContext.Context.Genres.Find(genre_id) == null)
            {
                return BadRequest("Жанр не найден");
            }
            if (ApplicationContext.Context.ProductGenres.FirstOrDefault(x => x.ProductId == product_id && genre_id == x.GenreId) != null)
            {
                return Conflict("У товара уже есть этот жанр");
            }
            ApplicationContext.Context.ProductGenres.Add(new ProductGenre()
            {
                GenreId = genre_id,
                ProductId = product_id
            });
            ApplicationContext.Context.SaveChanges();
            return new ObjectResult("Добавлено") { StatusCode = StatusCodes.Status201Created }; ;


        }
        [HttpDelete]
        [Route("DeleteGenreProduct")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<ProductGenre>> DeleteGenreProduct(int genre_id, int product_id)
        {
            if (ApplicationContext.Context.Products.Find(product_id) == null)
            {
                return NotFound("Товар не найден");
            }
            ProductGenre productGenre = ApplicationContext.Context.ProductGenres.FirstOrDefault(x => x.ProductId == product_id && genre_id == x.GenreId);
            if (productGenre == null)
            {
                return NotFound("Жанр у товара не найден");
            }
            ApplicationContext.Context.ProductGenres.Remove(productGenre);
            ApplicationContext.Context.SaveChanges();
            return Ok("Удалено");
        }
    }
}
