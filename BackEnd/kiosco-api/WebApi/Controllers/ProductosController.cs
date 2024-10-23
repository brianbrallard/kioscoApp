using Microsoft.AspNetCore.Mvc;
using kiosco_api.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kiosco_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductosController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoRepository.GetAll();
            return Ok(productos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            await _productoRepository.Add(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            await _productoRepository.Update(producto);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }

            await _productoRepository.Delete(id);
            return NoContent();
        }
    }
}
