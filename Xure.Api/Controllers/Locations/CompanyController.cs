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
    public class CompanyController : ControllerBase
    {
        private ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Get()
        {
            if (_companyRepository.GetAll() == null)
            {
                return NotFound("Компании не найдены");
            }
            else
            {
                return Ok(_companyRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Администратор")]
        public IActionResult Get(int id)
        {
            if (_companyRepository.Get(id) == null) return NotFound("Компания не найдена");
            else return Ok(_companyRepository.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public IActionResult Post(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Create(company);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + company.Id, company);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Администратор")]
        public ActionResult Update(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Update(company);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + company.Id, company);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int id)
        {
            if (_companyRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _companyRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
