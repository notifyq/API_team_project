using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("ProductList")]
        public async Task<ActionResult<List<Product>>> GetProductList()
        {
            List<Product> product_list = ApplicationContext.Context.Products.ToList();
            if (product_list.Count == 0)
            {
                return NotFound();
            }

            return product_list;
        }
        [HttpGet("{product_id}")]
        public async Task<ActionResult> GetProduct(int product_id)
        {
            var product = await ApplicationContext.Context.Products
                            .Include(p => p.ProductDeveloperNavigation)
                            .Include(p => p.ProductPublisherNavigation)
                            .Include(p => p.ProductStatusNavigation)
                            .Include(p => p.ProductGenres)
                            .Include(p => p.ProductImages)
                            .FirstOrDefaultAsync(p => p.ProductId == product_id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ActionName("AddProduct")]
        [Route("AddProduct")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Product>> AddProduct(ProductAdd product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (ApplicationContext.Context.Products.Find(product.ProductId) != null)
            {
                return Conflict($"Товар с {product.ProductId} уже сущетсвует") ;
            }

            Product new_product = new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductDeveloper = product.ProductDeveloper,
                ProductPublisher = product.ProductPublisher,
                ProductPrice = product.ProductPrice,
                ProductStatus = product.ProductStatus,
            };

            ApplicationContext.Context.Add(new_product);
            ApplicationContext.Context.SaveChanges();

            return new ObjectResult("Товар добавлен") { StatusCode = StatusCodes.Status201Created };
        }
        [HttpPut]
        [Route("ChangeStatus")]
        [Authorize(Roles = "Куратор контента")]
        public async Task<ActionResult<Product>> ChangeStatus(int product_id, int status_id)
        {
            Product product = ApplicationContext.Context.Products.Find(product_id);
            if (product == null)
            {
                return BadRequest("Товар не найден");
            }
            switch (status_id)
            {
                case 3:
                    product.ProductStatus = 3;
                    break;
                case 4:
                    product.ProductStatus = 4;
                    break;
                default:
                    return BadRequest("Статус может быть в продаже или не в продаже");
                    break;
            }
            
            return Ok("Статус товара изменен на " + ApplicationContext.Context.Statuses.Find(status_id).StatusName);
        }
    }
}
