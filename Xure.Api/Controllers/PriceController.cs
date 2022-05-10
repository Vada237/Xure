using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private IPriceRepository _priceRepository;

        public PriceController(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_priceRepository.GetAll() == null)
            {
                return NotFound("Текущие цены не найдены");
            }
            else
            {
                return Ok(_priceRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (_priceRepository.GetById(id) == null) return NotFound("Текущая цена не найдена");
            else return Ok(_priceRepository.GetById(id));
        }

        [HttpPost]

        public IActionResult Post(Prices price)
        {
            if (ModelState.IsValid)
            {
                _priceRepository.Create(price);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + price.Id, price);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Prices price)
        {
            if (ModelState.IsValid)
            {
                _priceRepository.Update(price);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + price.Id, price);
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
            if (_priceRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _priceRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
