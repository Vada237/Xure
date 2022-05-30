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
    [Authorize(Roles = "Модератор,Администратор")]
    public class ApiReceptionPointController : ControllerBase
    {
        private IReceptionPointRepository _receptionPointRepository;

        public ApiReceptionPointController(IReceptionPointRepository receptionPointRepository)
        {
            _receptionPointRepository = receptionPointRepository;
        }

        [HttpGet]              
        public ActionResult Get()
        {
            if (_receptionPointRepository.GetAll() == null)
            {
                return NotFound("Пункты доставки не найдены");
            }
            else
            {
                return Ok(_receptionPointRepository.GetAll());
            }

        }

        [HttpGet]        
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (_receptionPointRepository.GetById(id) == null) return NotFound("Пункт доставки не найден");
            else return Ok(_receptionPointRepository.GetById(id));
        }

        [HttpPost]

        public IActionResult Post(ReceptionPoint peceptionPoint)
        {
            if (ModelState.IsValid)
            {
                _receptionPointRepository.Create(peceptionPoint);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + peceptionPoint.id, peceptionPoint);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(ReceptionPoint peceptionPoint)
        {
            if (ModelState.IsValid)
            {
                _receptionPointRepository.Update(peceptionPoint);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + peceptionPoint.id, peceptionPoint);
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
            if (_receptionPointRepository.GetAll().FirstOrDefault(c => c.id == id) != null)
            {
                _receptionPointRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
