using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceController : ControllerBase
    {
        private IPriceRepository _priceRepository;

        public PriceController(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Модератор,Администратор")]
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
        [Authorize(Roles = "Модератор,Администратор")]
        public IActionResult Get(int id)
        {
            if (_priceRepository.GetById(id) == null) return NotFound("Текущая цена не найдена");
            else return Ok(_priceRepository.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Поставщик,Администратор")]
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
        [Authorize(Roles = "Поставщик,Администратор")]
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
        [Authorize(Roles = "Модератор,Администратор")]
        private ActionResult Delete(int id)
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
