using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiProductSpecificationsValueController : ControllerBase
    {
        private IProductSpecificationsValueRepository _productSpecificationsValueRepository;

        public ApiProductSpecificationsValueController(IProductSpecificationsValueRepository _productSpecificationsValueRepository)
        {
            this._productSpecificationsValueRepository = _productSpecificationsValueRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult Get()
        {
            if (_productSpecificationsValueRepository.GetAll() != null)
            {
                return Ok(_productSpecificationsValueRepository.GetAll());
            }
            else
            {
                return NotFound("Значения характеристик не найдены");
            }
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult GetById(int id)
        {
            if (_productSpecificationsValueRepository.GetAll().FirstOrDefault(c => c.Id == id) == null)
            {
                return NotFound("Значение не найдено");
            }
            else
            {
                return Ok(_productSpecificationsValueRepository.getById(id));
            }
        }
        [HttpPost]
        [Authorize(Roles = "Поставщик,Администратор")]

        public ActionResult Create(ProductSpecificationsValue productSpecificationsValue)
        {
            if (ModelState.IsValid)
            {
                _productSpecificationsValueRepository.Create(productSpecificationsValue);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + productSpecificationsValue.Id, productSpecificationsValue);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult Update(ProductSpecificationsValue productSpecificationsValue)
        {
            if (ModelState.IsValid)
            {
                _productSpecificationsValueRepository.Update(productSpecificationsValue);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + productSpecificationsValue.Id, productSpecificationsValue);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Поставщик,Модератор,Администратор")]

        public ActionResult Delete(int id)
        {
            if (_productSpecificationsValueRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _productSpecificationsValueRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }

    }
}
