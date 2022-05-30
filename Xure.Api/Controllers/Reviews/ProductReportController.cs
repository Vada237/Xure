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
    public class ApiProductReportController : ControllerBase
    {
        private IProductReportRepository _ProductReportRepository;

        public ApiProductReportController(IProductReportRepository ProductReportRepository)
        {
            _ProductReportRepository = ProductReportRepository;
        }

        [HttpGet]
        [Authorize(Roles= "Модератор,Администратор")]
        public ActionResult Get()
        {
            if (_ProductReportRepository.GetAll() == null)
            {
                return NotFound("Жалобы на продукт не найдены");
            }
            else
            {
                return Ok(_ProductReportRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles= "Модератор,Администратор")]
        public IActionResult Get(int Id)
        {
            if (_ProductReportRepository.Get(Id) == null) return NotFound("Жалоба на продукт не найдена");
            else return Ok(_ProductReportRepository.Get(Id));
        }

        [HttpPost]
        [Authorize(Roles = "Покупатель,Поставщик,Администратор")]
        public IActionResult Post(ProductReport ProductReport)
        {
            if (ModelState.IsValid)
            {
                _ProductReportRepository.Create(ProductReport);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + ProductReport.Id, ProductReport);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Модератор,Адмнистратор")]
        public ActionResult Update(ProductReport ProductReport)
        {
            if (ModelState.IsValid)
            {
                _ProductReportRepository.Update(ProductReport);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + ProductReport.Id, ProductReport);
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
            if (_ProductReportRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _ProductReportRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
