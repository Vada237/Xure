using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _MessageRepository;

        public MessageController(IMessageRepository MessageRepository)
        {
            _MessageRepository = MessageRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_MessageRepository.GetAll() == null)
            {
                return NotFound("Сообщения не найдены");
            }
            else
            {
                return Ok(_MessageRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(int Id)
        {
            if (_MessageRepository.Get(Id) == null) return NotFound("Сообщение не найдено");
            else return Ok(_MessageRepository.Get(Id));
        }

        [HttpPost]

        public IActionResult Post(Message Message)
        {
            if (ModelState.IsValid)
            {
                _MessageRepository.Create(Message);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Message.Id, Message);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Message Message)
        {
            if (ModelState.IsValid)
            {
                _MessageRepository.Update(Message);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Message.Id, Message);
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
            if (_MessageRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _MessageRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
