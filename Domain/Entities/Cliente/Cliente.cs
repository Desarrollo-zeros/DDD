using Domain.Entities.Factura;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cliente
{
    public class Cliente : Entity<int>
    {
        public Cliente(Documento Documento, Nombre Nombre, EmailValueObject Email, int Usuario_Id)
        {
            this.Documento = Documento;
            this.Nombre = Nombre;
            this.Email = Email;
            this.Usuario_Id = Usuario_Id;
        }

        public Documento Documento { set; get; }
        public Nombre Nombre { set; get; }
        public EmailValueObject Email { set; get; }
        public virtual IEnumerable<TelefónoCliente> Telefónos { set; get; }

        public int Usuario_Id { set; get; }

        [ForeignKey("Usuario_Id")] public Usuario Usuario { set; get; }

        public virtual IEnumerable<ClienteDireccíon> ClienteDireccíones { set; get; }

        public IEnumerable<ProductoCliente> productos { set; get; }
    }
}
