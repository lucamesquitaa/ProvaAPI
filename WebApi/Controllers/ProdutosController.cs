using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProductsService _service;
        public ProdutosController(IProductsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string term, int page, int pageSize)
        {
            var response = await _service.GetAll(term, page, pageSize);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Produtos products)
        {
            var response = await _service.Add(products);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Produtos products)
        {
            var response = await _service.Update(products);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.Delete(id);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

