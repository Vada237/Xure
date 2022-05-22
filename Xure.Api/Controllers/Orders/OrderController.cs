using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
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
        public IActionResult Get(int id)
        {
            if (_orderRepository.GetOrder(id) == null) return NotFound("Заказ не найден");
            else return Ok(_orderRepository.GetOrder(id));
        }

        [HttpPost]

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
