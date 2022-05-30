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
    public class ApiBrandsController : ControllerBase
    {
        private IBrandRepository _brandRepository;

        public ApiBrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult Get()
        {
            if (_brandRepository.GetAll() == null)
            {
                return NotFound("Бренды не найдены");
            }
            else
            {
                return Ok(_brandRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Модератор,Администратор")]
        public IActionResult Get(int id)
        {
            if (_brandRepository.getById(id) == null) return NotFound("Бренд не найден");
            else return Ok(_brandRepository.getById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Поставщик,Администратор")]
        public IActionResult Post(Brands brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Create(brand);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + brand.Id, brand);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult Update(Brands brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Update(brand);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + brand.Id, brand);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Модератор,Администратор")]
        public ActionResult Delete(int id)
        {
            if (_brandRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _brandRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
