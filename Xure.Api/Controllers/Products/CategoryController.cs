using Microsoft.AspNetCore.Mvc;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Xure.Api.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    [Authorize]
    public class CategoryController : ControllerBase
    {        
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(AppDbContext appDbContext,ICategoryRepository categoryRepository)
        {                 
           this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Get()
        {
            if (_categoryRepository.GetAll() != null)
            {
                return Ok(_categoryRepository.GetAll());
            } else { 
                return NotFound("Категории не найдены"); 
            }
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult GetById(int id)
        {
            if (_categoryRepository.GetAll().FirstOrDefault(c => c.Id == id) == null)
            {
                return NotFound("Категория не найдена");
            } else
            {
                return Ok(_categoryRepository.GetById(id));
            }
        }
        [HttpPost]
        [Authorize(Roles = "Поставщик,Администратор")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
               _categoryRepository.Create(category);                
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + category.Id,category);
            } else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);                
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + category.Id, category);
            } else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public ActionResult Delete(int id)
        {
            if (_categoryRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _categoryRepository.Delete(id);                
                return Ok("Объект удален");
            } else {
                return NotFound("Объект не найден");
               }
        }
    }
}
