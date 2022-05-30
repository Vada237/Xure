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
    public class ApiOrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public ApiOrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Администратор,Менеджер")]
        public ActionResult Get()
        {
            if (_orderRepository.GetAll() == null)
            {
                return NotFound("Заказы не найдены");
            }
            else
            {
                return Ok(_orderRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public IActionResult Get(int id)
        {
            if (_orderRepository.GetOrder(id) == null) return NotFound("Заказ не найден");
            else return Ok(_orderRepository.GetOrder(id));
        }

        [HttpPost]
        [Authorize(Roles = "Администратор,Пользователь")]
        public IActionResult Post(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Create(order);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + order.Id, order);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Администратор")]

        public ActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + order.Id, order);
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
            if (_orderRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _orderRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
