using Domain.Entities.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Localizacíon
{
    public class Dirección : Entity<int>
    {
        public Dirección(string barrio, string direccion, string codigoPostal, int municipio_Id, Municipio municipio, IEnumerable<ClienteDireccíon> clienteDireccíones)
        {
            Barrio = barrio;
            Direccion = direccion;
            CodigoPostal = codigoPostal;
            Municipio_Id = municipio_Id;
            Municipio = municipio;
            ClienteDireccíones = clienteDireccíones;
        }

        public Dirección(string barrio, string direccion, string codigoPostal, int municipio_Id)
        {
            Barrio = barrio;
            Direccion = direccion;
            CodigoPostal = codigoPostal;
            Municipio_Id = municipio_Id;
        }

        public string Barrio { set; get; }
        public string Direccion { set; get; }
        public string CodigoPostal { set; get; }
        public int Municipio_Id { set; get; }
        [ForeignKey("Municipio_Id")] public Municipio Municipio { set; get; }
        public virtual IEnumerable<ClienteDireccíon> ClienteDireccíones { set; get; }
    }


}
