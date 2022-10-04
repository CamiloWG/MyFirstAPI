using Microsoft.AspNetCore.Mvc;
using ShopApi.Datos;
using ShopApi.Model;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public async Task <ActionResult<List<Mproductos>>> Get()
        {
            var funcion = new Dproductos();
            var lista = await funcion.MostrarProductos();
            return lista;
        }

        [HttpPost]
        public async Task Post([FromBody] Mproductos parametros)
        {
            var funcion = new Dproductos();
            await funcion.InsertarProducto(parametros);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Mproductos parametros)
        {
            var funcion = new Dproductos();
            parametros.id = id;
            await funcion.EditarProducto(parametros);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var funcion = new Dproductos();
            var parametros = new Mproductos();
            parametros.id = id;
            await funcion.EliminarProducto(parametros);
        }
    }
}
