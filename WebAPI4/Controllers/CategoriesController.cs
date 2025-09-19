using Business.Abstract;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            Thread.Sleep(1000);

            var result = _categoryService.GetAll();

            if (result.Success)
            {
                return Ok(result);  // Başarılıysa 200 OK + ürün listesini döndürür ---result.Data idi data sildim
            }

            return BadRequest(result);

        }
    }
}
