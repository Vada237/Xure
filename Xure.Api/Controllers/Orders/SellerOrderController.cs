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
    public class ApiSellerOrderController : ControllerBase
    {
        private ISellerOrderRepository _sellerOrderRepository;

        public ApiSellerOrderController(ISellerOrderRepository sellerOrderRepository)
        {
            _sellerOrderRepository = sellerOrderRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Get()
        {
            if (_sellerOrderRepository.GetAll() == null)
            {
                return NotFound("Заказы поставщика не найдены");
            }
            else
            {
                return Ok(_sellerOrderRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Поставщик,Администратор")]
        public IActionResult Get(int id)
        {
            if (_sellerOrderRepository.GetSellerOrder(id) == null) return NotFound("Заказ поставщика не найден");
            else return Ok(_sellerOrderRepository.GetSellerOrder(id));
        }

        [HttpPost]
        [Authorize (Roles = "Поставщик,Администратор")]
        public IActionResult Post(SellerOrder sellerOrder)
        {
            if (ModelState.IsValid)
            {
                _sellerOrderRepository.Create(sellerOrder);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + sellerOrder.Id, sellerOrder);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize (Roles = "Поставщик,Администратор")]
        public ActionResult Update(SellerOrder sellerOrder)
        {
            if (ModelState.IsValid)
            {
                _sellerOrderRepository.Update(sellerOrder);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + sellerOrder.Id, sellerOrder);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize (Roles = "Поставщик,Администратор")]
        public ActionResult Delete(int id)
        {
            if (_sellerOrderRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _sellerOrderRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
