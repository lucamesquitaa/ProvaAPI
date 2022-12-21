using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriesService _service;
        public CategoriasController(ICategoriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> Add(Categorias category)
        {
            var response = await _service.Add(category);
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
    
}
