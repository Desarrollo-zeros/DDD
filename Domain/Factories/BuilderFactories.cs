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
            if(nombre == "" && descripción == "")
            {
                throw new Exception("Factories Categoria no puede ser creado");
            }

            if(fecha == null)
            {
                fecha = new DateTime();
            }

            return new Categoria(nombre, descripción, fecha);
        }

    }

}
