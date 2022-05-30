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
    public class ApiReasonController : ControllerBase
    {
        private IReasonRepository _ReasonRepository;

        public ApiReasonController(IReasonRepository ReasonRepository)
        {
            _ReasonRepository = ReasonRepository;
        }

        [HttpGet]        
        public ActionResult Get()
        {
            if (_ReasonRepository.GetAll() == null)
            {
                return NotFound("Причины не найдены");
            }
            else
            {
                return Ok(_ReasonRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(int Id)
        {
            if (_ReasonRepository.Get(Id) == null) return NotFound("Причина не найдена");
            else return Ok(_ReasonRepository.Get(Id));
        }

        [HttpPost]

        public IActionResult Post(Reason Reason)
        {
            if (ModelState.IsValid)
            {
                _ReasonRepository.Create(Reason);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Reason.Id, Reason);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Reason Reason)
        {
            if (ModelState.IsValid)
            {
                _ReasonRepository.Update(Reason);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Reason.Id, Reason);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult Delete(int Id)
        {
            if (_ReasonRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _ReasonRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
