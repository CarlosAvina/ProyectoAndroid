using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using ProyectoAndroid.Models;

namespace ProyectoAndroid.DAL
{
    public class BaseDatos
    {
        public static readonly string RutaBaseDatos =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "basedatos.db3");


        public static async Task<bool> CrearBaseDatos()
        {
            if (!File.Exists(RutaBaseDatos))
            {
                File.Create(RutaBaseDatos);
                var conexion = new SQLiteAsyncConnection(RutaBaseDatos);
                await conexion.CreateTableAsync<Producto>();
            }

            return false;
        }
    }
}
