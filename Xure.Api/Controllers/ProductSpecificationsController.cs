using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationsController : ControllerBase
    {
        private IProductSpecificationsRepository _productSpecificationsRepository;

        public ProductSpecificationsController(IProductSpecificationsRepository productSpecificationsRepository)
        {
            _productSpecificationsRepository = productSpecificationsRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_productSpecificationsRepository.GetAll() != null)
            {
                return Ok(_productSpecificationsRepository.GetAll());
            }
            else
            {
                return NotFound("Характеристики не найдены");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            if (_productSpecificationsRepository.GetAll().FirstOrDefault(c => c.Id == id) == null)
            {
                return NotFound("Характеристика не найдена");
            }
            else
            {
                return Ok(_productSpecificationsRepository.GetById(id));
            }
        }
        [HttpPost]
        public ActionResult Create(ProductSpecifications productSpecifications)
        {
            if (ModelState.IsValid)
            {
                _productSpecificationsRepository.Create(productSpecifications);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + productSpecifications.Id, productSpecifications);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(ProductSpecifications productSpecifications)
        {
            if (ModelState.IsValid)
            {
                _productSpecificationsRepository.Update(productSpecifications);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + productSpecifications.Id, productSpecifications);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (_productSpecificationsRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _productSpecificationsRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
