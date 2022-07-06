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
    public class ApiDeliveryController : ControllerBase
    {
        private IDeliveryRepository _deliveryRepository;

        public ApiDeliveryController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Get()
        {
            if (_deliveryRepository.GetAll() == null)
            {
                return NotFound("Поставки не найдены");
            }
            else
            {
                return Ok(_deliveryRepository.GetAll());
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public IActionResult Get(int id)
        {
            if (_deliveryRepository.GetDelivery(id) == null) return NotFound("Поставка не найдена");
            else return Ok(_deliveryRepository.GetDelivery(id));
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public IActionResult Post(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _deliveryRepository.Create(delivery);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + delivery.Id, delivery);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Администратор")]
        public ActionResult Update(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _deliveryRepository.Update(delivery);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + delivery.Id, delivery);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int id)
        {
            if (_deliveryRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _deliveryRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
