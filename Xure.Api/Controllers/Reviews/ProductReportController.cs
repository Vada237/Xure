using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;


namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReportController : ControllerBase
    {
        private IProductReportRepository _ProductReportRepository;

        public ProductReportController(IProductReportRepository ProductReportRepository)
        {
            _ProductReportRepository = ProductReportRepository;
        }

        [HttpGet]
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
        public IActionResult Get(int Id)
        {
            if (_ProductReportRepository.Get(Id) == null) return NotFound("Жалоба на продукт не найдена");
            else return Ok(_ProductReportRepository.Get(Id));
        }

        [HttpPost]

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
        public ActionResult Delete(int Id)
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
