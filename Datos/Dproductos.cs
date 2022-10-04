using ShopApi.Conexion;
using ShopApi.Model;
using System.Data.SqlClient;

namespace ShopApi.Datos
{
    public class Dproductos
    {
        ConexionDB connect = new ConexionDB();
        public async Task <List<Mproductos>> MostrarProductos()
        {
            var lista = new List<Mproductos>();
            using (var sql = new SqlConnection(connect.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            var mproductos = new Mproductos();
                            mproductos.id = (int)reader["id"];
                            mproductos.descripcion = (string)reader["descripcion"];
                            mproductos.precio = (decimal)reader["precio"];
                            lista.Add(mproductos);
                        }
                    }
                }
            }
            return lista;
        }

        public async Task InsertarProducto(Mproductos parametros)
        {
            using(var sql = new SqlConnection(connect.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditarProducto(Mproductos parametros)
        {
            using (var sql = new SqlConnection(connect.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarProducto(Mproductos parametros)
        {
            using (var sql = new SqlConnection(connect.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
