using Domain.Base;
using Domain.Entities.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Domain.Enum;

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

        public static IEnumerable<Telefóno> Telefónos(IEnumerable<Telefóno> telefónos, int cliente_id)
        {
            telefónos.ToList().ForEach(x=> {
                if(x.Número == null || x.TipoTelefono == TipoTelefono.DESCONOCIDO)
                {
                    telefónos.ToList().Remove(x);
                }
                x.Cliente_Id = cliente_id;
            });
            return telefónos;
        }

        public static Dirección Dirección(string barrio, string direccion, string codigoPostal, int municipio_Id, int cliente_Id)
        {
            if(string.Empty == barrio || string.Empty == direccion || string.Empty == codigoPostal || municipio_Id == 0|| cliente_Id == 0)
            {
                throw new Exception("Factories Dirección no puede ser creado");
            }
            return new Dirección(barrio, direccion, codigoPostal, municipio_Id, cliente_Id);
        }


        public static IEnumerable<Dirección> Direcciónes(IEnumerable<Dirección> direcciónes, int cliente_id)
        {
            direcciónes.ToList().ForEach(x => {
                if (x.Barrio == null || x.Direccion == null || x.CodigoPostal == null || x.Municipio_Id == 0)
                {
                    direcciónes.ToList().Remove(x);
                }
                x.Cliente_Id = cliente_id;
            });
            return direcciónes;
        }

        public static ClienteMetodoDePago ClienteMetodoDePago(int cliente_Id, double saldo, CreditCardType cardType, string cardNumber, string securityNumber, string ownerName, DateTime expiration)
        {
            if (0 == cliente_Id || 0 == saldo || cardType == CreditCardType.Unknown || cardNumber == null || null == securityNumber || ownerName == null || expiration == null)
            {
                throw new Exception("Factories ClienteMetodoDePago no puede ser creado");
            }
            return new ClienteMetodoDePago(cliente_Id, new CreditCard(cardType, cardNumber, securityNumber, ownerName, expiration), saldo);
        }

    }

}
