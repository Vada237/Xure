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
    public class ApiOrderProductController : ControllerBase
    {
        private IOrderProductRepository _orderProductRepository;

        public ApiOrderProductController(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Get()
        {
            if (_orderProductRepository.GetAll() == null)
            {
                return NotFound("Заказы не найдены");
            }
            else
            {
                return Ok(_orderProductRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("ProductId={id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public IActionResult GetByProductId(int id)
        {           
            if (_orderProductRepository.GetByProductId(id) == null) return NotFound($"Заказы с товаром {_orderProductRepository.GetProductName(id)} не найдены");
            else return Ok(_orderProductRepository.GetByProductId(id));           
        }

        [HttpGet]
        [Route("OrderId={id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public IActionResult GetByOrderId(int id)
        {
            if (_orderProductRepository.GetByOrderId(id) == null) return NotFound("Заказ не найден");
            else return Ok(_orderProductRepository.GetByOrderId(id));
        }

        [HttpGet]
        [Route("{orderId}/{productId}")]
        [Authorize (Roles = "Менеджер,Администратор")]
        public IActionResult GetByIds(int orderId,int productId)
        {
            if (_orderProductRepository.GetByIds(orderId, productId) == null) return NotFound($"Товар {_orderProductRepository.GetProductName(productId)} отсутствует в заказе");
            else return Ok(_orderProductRepository.GetByIds(orderId,productId));
        }

        [HttpPost]
        [Authorize(Roles = "Покупатель,Администратор")]

        public IActionResult Post(OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                _orderProductRepository.Create(orderProduct);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + orderProduct.OrderId + "&&" + orderProduct.ProductId, orderProduct);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Администратор")]
        public ActionResult Update(OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                _orderProductRepository.Update(orderProduct);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + orderProduct.OrderId + "&&" + orderProduct.ProductId, orderProduct);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Покупатель,Администратор")]
        public ActionResult Delete(long id)
        {
            if (_orderProductRepository.GetAll().FirstOrDefault(c => c.OrderId == id) != null)
            {
                foreach (var i in _orderProductRepository.GetByOrderId(id))
                {
                    _orderProductRepository.Delete(id);
                }
                return Ok("Заказ удален");
            }
            else
            {
                return NotFound("Заказ не найден");
            }
        }
    }
}
