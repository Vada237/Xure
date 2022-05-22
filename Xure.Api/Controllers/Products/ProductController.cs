using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductRepository _ProductRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_ProductRepository.GetAll() == null)
            {
                return NotFound("Продукты не найдены");
            }
            else
            {
                return Ok(_ProductRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(int Id)
        {
            if (_ProductRepository.GetById(Id) == null) return NotFound("Продукт не найден");
            else return Ok(_ProductRepository.GetById(Id));
        }

        [HttpPost]

        public IActionResult Post(Product Product)
        {
            if (ModelState.IsValid)
            {
                _ProductRepository.Create(Product);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Product.Id, Product);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Product Product)
        {
            if (ModelState.IsValid)
            {
                _ProductRepository.Update(Product);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Product.Id, Product);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult Delete(int Id)
        {
            if (_ProductRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _ProductRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
