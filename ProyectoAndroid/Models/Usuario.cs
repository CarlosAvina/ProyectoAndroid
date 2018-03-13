using System;
using SQLite;

namespace ProyectoAndroid.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
