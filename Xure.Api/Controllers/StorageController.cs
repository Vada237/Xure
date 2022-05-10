using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageRepository _storageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_storageRepository.GetAll() == null)
            {
                return NotFound("Склады не найдены");
            }
            else
            {
                return Ok(_storageRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (_storageRepository.GetStorage(id) == null) return NotFound("Склад не найден");
            else return Ok(_storageRepository.GetStorage(id));
        }

        [HttpPost]

        public IActionResult Post(Storage storage)
        {
            if (ModelState.IsValid)
            {
                _storageRepository.Create(storage);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + storage.id, storage);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(Storage storage)
        {
            if (ModelState.IsValid)
            {
                _storageRepository.Update(storage);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + storage.id, storage);
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
            if (_storageRepository.GetAll().FirstOrDefault(c => c.id == id) != null)
            {
                _storageRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
