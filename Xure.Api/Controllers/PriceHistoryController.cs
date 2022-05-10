﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private IPriceHistoryRepository _priceHistoryRepository;

        public PriceHistoryController(IPriceHistoryRepository priceHistoryRepository)
        {
            _priceHistoryRepository = priceHistoryRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_priceHistoryRepository.GetAll() == null)
            {
                return NotFound("Цены не найдены");
            }
            else
            {
                return Ok(_priceHistoryRepository.GetAll());
            }

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (_priceHistoryRepository.GetById(id) == null) return NotFound("Цена не найдена");
            else return Ok(_priceHistoryRepository.GetById(id));
        }

        [HttpPost]

        public IActionResult Post(PriceHistory priceHistory)
        {
            if (ModelState.IsValid)
            {
                _priceHistoryRepository.Create(priceHistory);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + priceHistory.Id, priceHistory);
            }
            else
            {
                return BadRequest("Некорректные данные");
            }
        }

        [HttpPut]
        public ActionResult Update(PriceHistory priceHistory)
        {
            if (ModelState.IsValid)
            {
                _priceHistoryRepository.Update(priceHistory);
                return Accepted(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + priceHistory.Id, priceHistory);
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
            if (_priceHistoryRepository.GetAll().FirstOrDefault(c => c.Id == id) != null)
            {
                _priceHistoryRepository.Delete(id);
                return Ok("Объект удален");
            }
            else
            {
                return NotFound("Объект не найден");
            }
        }
    }
}
