using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        [Route("AddProductImage")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<ProductImage>> AddImage(int product_id, string image_path)
        {
            if (ApplicationContext.Context.Products.Find(product_id) == null)
            {
                return BadRequest("Товар не найден");
            }

            int product_image_id = 1;
            if (ApplicationContext.Context.ProductImages.Count() != 0)
            {
                product_image_id = ApplicationContext.Context.ProductImages.Max(x => x.ProductImageId) + 1;
            }

            ApplicationContext.Context.Add(new ProductImage
            {
                
                ProductImageId = product_image_id,
                ProductId = product_id,
                ProductImagePath = image_path,
            });
            ApplicationContext.Context.SaveChanges();
            return new ObjectResult("Изображение добавлено") { StatusCode = StatusCodes.Status201Created }; ;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductImage>>> GetImages()
        {
            List<ProductImage> productImages = ApplicationContext.Context.ProductImages.ToList();
            if (productImages == null)
            {
                return NotFound("Не найдено");
            }

            return Ok(productImages);

        }
        [HttpDelete]
        [Route("DeleteImage")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<List<ProductImage>>> DeleteImage(int product_image_id)
        {
            ProductImage productImage = ApplicationContext.Context.ProductImages.Find(product_image_id);
            if (productImage == null)
            {
                return BadRequest("Не найдено");
            }
            ApplicationContext.Context.ProductImages.Remove(productImage);
            ApplicationContext.Context.SaveChanges();

            return Ok("Изображение удалено");
        }

    }
}
