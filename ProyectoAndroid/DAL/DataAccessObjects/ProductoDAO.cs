using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoAndroid.Models;
using SQLite;

namespace ProyectoAndroid.DAL.DataAccessObjects
{
    public class ProductoDAO
    {
        public static async Task<Producto> Create(Producto producto){
            
            var conexion = new SQLiteAsyncConnection(BaseDatos.RutaBaseDatos);

            var nombreProducto = producto.Nombre;

            var tablaProductos = await conexion
                .Table<Producto>()
                .ToListAsync();

            var existe = tablaProductos.FirstOrDefault(
                p => nombreProducto == p.Nombre);

            if (existe == null)
            {
                var idInsertado = await conexion.InsertAsync(producto);

                producto.Id = idInsertado;

                return producto;
            }

            throw new Exception(
                "Ya existe el producto con el nombre " + nombreProducto);
            
        }

        public static async Task<int> GetAll() {
            var conexion = new SQLiteAsyncConnection(BaseDatos.RutaBaseDatos);

            var tablaProductos = await conexion
                .Table<Producto>()
                .ToListAsync();

            return tablaProductos.Count;
        }
    }
}
