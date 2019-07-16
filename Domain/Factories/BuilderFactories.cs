using Domain.Base;
using Domain.Entities.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Domain.Enum;
using Domain.Entities.Producto;

namespace Domain.Factories
{
    public class BuilderFactories
    {

        
        private BuilderFactories() { }

        public static Usuario Usuario(string username, string password, bool active, Enum.Rol rol)
        {
            if(username == "" || password == "")
            {
                throw new Exception("Factories Usuario no puede ser creado");
            }
            return new Usuario(username, password, active, rol);
        }

        public static Cliente Cliente(string documento, Nombre nombre, string Email, int Usuario_Id)
        {
            if(documento == null|| nombre == null|| Email == ""|| Usuario_Id == 0){
                throw new Exception("Factories Cliente no puede ser creado");
            }
            
            return new Cliente(documento, nombre, Email, Usuario_Id);
        }

        public static Telefóno Telefóno(string número, TipoTelefono tipoTelefono, int cliente_id)
        {
            if (número == null || tipoTelefono == TipoTelefono.DESCONOCIDO || cliente_id == 0)
            {
                throw new Exception("Factories Cliente no puede ser creado");
            }
            return new Telefóno(número, tipoTelefono, cliente_id);
        }

       

        public static Dirección Dirección(string barrio, string direccion, string codigoPostal, int municipio_Id, int cliente_Id)
        {
            if(string.Empty == barrio || string.Empty == direccion || string.Empty == codigoPostal || municipio_Id == 0|| cliente_Id == 0)
            {
                throw new Exception("Factories Dirección no puede ser creado");
            }
            return new Dirección(barrio, direccion, codigoPostal, municipio_Id, cliente_Id);
        }

        public static ClienteMetodoDePago ClienteMetodoDePago(int cliente_Id, bool activo, double saldo, CreditCardType cardType, string cardNumber, string securityNumber, string ownerName, DateTime expiration)
        {
            if (0 == cliente_Id || 0 == saldo || cardType == CreditCardType.Unknown || cardNumber == null || null == securityNumber || ownerName == null || expiration == null)
            {
                throw new Exception("Factories ClienteMetodoDePago no puede ser creado");
            }
           
            if (!CreditCard.VerificarTarjeta(cardNumber))
            {
                throw new Exception("Factories ClienteMetodoDePago no puede ser creado, Tarjeta invalidad");
            }

            if(expiration < DateTime.Now)
            {
                throw new Exception("Factories ClienteMetodoDePago no puede ser creado, tarjeta vencida");
            }
            return new ClienteMetodoDePago(cliente_Id, activo, new CreditCard(cardType, cardNumber, securityNumber, ownerName, expiration), saldo);
        }

        public static Categoria Categoria(string nombre, string descripción, DateTime fecha)
        {
            if(nombre == "" || descripción == "")
            {
                throw new Exception("Factories Categoria no puede ser creado");
            }

            if(fecha == null)
            {
                fecha = new DateTime();
            }

            return new Categoria(nombre, descripción, fecha);
        }

        public static Producto Producto(string nombre, string descripción, string imagen, double precioCompra, double precioVenta, int cantidadProducto, int categoria_Id)
        {
            if(string.Empty == nombre || string.Empty == descripción || 0== precioCompra || 0 == precioVenta || 0 == cantidadProducto || 0 == categoria_Id || precioCompra > precioVenta)
            {
                throw new Exception("Factories Producto no puede ser creado");
            }

            return new Producto(nombre,descripción,imagen,precioCompra,precioVenta,cantidadProducto,categoria_Id);
        }

        public static Descuento Descuento(TipoDescuento tipoDescuento, bool acomulable, DateTime fechaYHoraInicio, DateTime fechaYHoraTerminación, double descuento)
        {
            if(tipoDescuento == TipoDescuento.DESCONOCIDO || fechaYHoraInicio == null || fechaYHoraTerminación == null || descuento == 0 || descuento < 0)
            {
                throw new Exception("Factories Descuento no puede ser creado");
            }
            return new Descuento(tipoDescuento, acomulable, fechaYHoraInicio, fechaYHoraTerminación, descuento);
        }

        public static ProductoDescuento ProductoDescuento(int producto_Id, int descuento_Id, EstadoDescuento estadoDescuento)
        {
            if(producto_Id == 0 || descuento_Id == 0 || estadoDescuento == EstadoDescuento.DESCONOCIDO)
            {
                throw new Exception("Factories ProductoDescuento no puede ser creado");
            }
            return new ProductoDescuento(producto_Id,descuento_Id, estadoDescuento);
        }

    }

}
