using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewsRepository _ReviewsRepository;

        public ReviewsController(IReviewsRepository ReviewsRepository)
        {
            _ReviewsRepository = ReviewsRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_ReviewsRepository.GetAll() == null)
            {
                return NotFound("Отзывы не найдены");
            }
            else
            {
                return Ok(_ReviewsRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(int Id)
        {
            if (_ReviewsRepository.Get(Id) == null) return NotFound("Отзыв не найден");
            else return Ok(_ReviewsRepository.Get(Id));
        }

        [HttpPost]

        public IActionResult Post(Reviews Reviews)
        {
            if (ModelState.IsValid)
            {
                _ReviewsRepository.Create(Reviews);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + Reviews.Id, Reviews);
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
            if (_ReviewsRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _ReviewsRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
