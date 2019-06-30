using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Entities.Factura
{
    [Table("Producto_Cliente")]
    public class ProductoCliente : Entity<int>
    {
        public ProductoCliente(int cliente_Id, int producto_Id, int compra_Id, int cantidad)
        {
            Cliente_Id = cliente_Id;
            Producto_Id = producto_Id;
            Compra_Id = compra_Id;
            Cantidad = cantidad;
        }

        public ProductoCliente() { }

        public int Compra_Id { set; get; }
        [ForeignKey("Compra_Id")] public Compra Compra { set; get; }
        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")] public Cliente.Cliente Cliente { set; get; }
        public int Producto_Id { set; get; }
        [ForeignKey("Producto_Id")] public Producto.Producto Producto { set; get; }

        public int Cantidad { set; get; }
    }
}
