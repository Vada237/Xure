using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderReportController : ControllerBase
    {
        private IOrderReportRepository _OrderReportRepository;

        public OrderReportController(IOrderReportRepository OrderReportRepository)
        {
            _OrderReportRepository = OrderReportRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_OrderReportRepository.GetAll() == null)
            {
                return NotFound("Жалобы на заказ не найдены");
            }
            else
            {
                return Ok(_OrderReportRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(int Id)
        {
            if (_OrderReportRepository.Get(Id) == null) return NotFound("Жалоба на заказ не найдена");
            else return Ok(_OrderReportRepository.Get(Id));
        }

        [HttpPost]

        public IActionResult Post(OrderReport OrderReport)
        {
            if (ModelState.IsValid)
            {
                _OrderReportRepository.Create(OrderReport);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + OrderReport.Id, OrderReport);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(OrderReport OrderReport)
        {
            if (ModelState.IsValid)
            {
                _OrderReportRepository.Update(OrderReport);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + OrderReport.Id, OrderReport);
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
            if (_OrderReportRepository.GetAll().FirstOrDefault(c => c.Id == Id) != null)
            {
                _OrderReportRepository.Delete(Id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
