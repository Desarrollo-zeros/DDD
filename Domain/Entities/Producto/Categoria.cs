using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Producto
{
    public class Categoria : Entity<int>
    {
        public Categoria(string nombre, string descripción, DateTime fechaCreacion)
        {
            Nombre = nombre;
            Descripción = descripción;
            FechaCreacion = fechaCreacion;
        }

        
        public Categoria(string nombre, string descripción, DateTime fechaCreacion, IEnumerable<Producto> productos)
        {
            Nombre = nombre;
            Descripción = descripción;
            FechaCreacion = fechaCreacion;
            Productos = new List<Producto>();
            Productos = productos;
        }

        public Categoria() { }

        [Column("Categoria")]
        public string Nombre { set; get; }
        public string Descripción { set; get; }
        public DateTime FechaCreacion { set; get; }

        public virtual IEnumerable<Producto> Productos { set; get; }
    }
}
