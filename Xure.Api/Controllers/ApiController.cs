using Microsoft.AspNetCore.Mvc;
using Xure.Data;

namespace Xure.Api.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class ProductController : ControllerBase
    {
        private AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {            
           _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult list() => Ok(_appDbContext.Products);
    }
}
