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
    public class MessageController : ControllerBase
    {
        private IMessageRepository _MessageRepository;

        public MessageController(IMessageRepository MessageRepository)
        {
            _MessageRepository = MessageRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        private ActionResult Get()
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
        [Authorize(Roles = "Администратор")]
        private IActionResult Get(int Id)
        {
            if (_MessageRepository.Get(Id) == null) return NotFound("Сообщение не найдено");
            else return Ok(_MessageRepository.Get(Id));
        }

        [HttpPost]
        [Authorize(Roles = "Поставщик,Покупатель,Администратор")]
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
        [Authorize(Roles = "Администратор")]
        private ActionResult Update(Message Message)
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
        [Authorize(Roles = "Администратор")]
        private ActionResult Delete(int Id)
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
