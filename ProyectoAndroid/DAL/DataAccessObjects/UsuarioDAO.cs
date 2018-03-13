using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoAndroid.Models;
using SQLite;

namespace ProyectoAndroid.DAL.DataAccessObjects
{
    public class UsuarioDAO
    {
        public static async Task<Usuario> Create(Usuario usuario)
        {

            var conexion = new SQLiteAsyncConnection(BaseDatos.RutaBaseDatos);

            var nombreUsuario = usuario.Nickname;

            var tablaUsuarios = await conexion
                .Table<Usuario>()
                .ToListAsync();

            var existe = tablaUsuarios.FirstOrDefault(
                u => nombreUsuario == u.Nickname);

            if (existe == null)
            {
                var idInsertado = await conexion.InsertAsync(usuario);

                usuario.Id = idInsertado;

                return usuario;
            }

            throw new Exception(
                "Ya existe el usuario con el nombre " + nombreUsuario);

        }

        public static async Task<List<Usuario>> GetAll()
        {
            var conexion = new SQLiteAsyncConnection(BaseDatos.RutaBaseDatos);

            var tablaUsuarios = await conexion
                .Table<Usuario>()
                .ToListAsync();

            return tablaUsuarios;
        }
    }
}
