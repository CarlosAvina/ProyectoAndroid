using SQLite;

namespace ProyectoAndroid.Models
{
    public class Producto
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Unidad { get; set; }
        public int Cantidad { get; set; }

    }
}
