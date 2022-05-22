using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class UnitController : ControllerBase
    {           
        
        private IUnitRepository _unitRepository;

        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public ActionResult Get() { 
           if (_unitRepository.GetAll() == null)
            {
                return NotFound("Единицы измерения не найдены");
            } else
            {
                return Ok(_unitRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (_unitRepository.GetById(id) == null) return NotFound("Единица измерения не найдена");
            else return Ok(_unitRepository.GetById(id));
        }

        [HttpPost]

        public IActionResult Post(Units unit)
        {
            if (ModelState.IsValid)
            {
                _unitRepository.Create(unit);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + unit.id, unit);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Units unit)
        {
            if (ModelState.IsValid)
            {
                _unitRepository.Update(unit);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + unit.id, unit);
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
            if (_unitRepository.GetAll().FirstOrDefault(c => c.id == id) != null)
            {
                _unitRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
