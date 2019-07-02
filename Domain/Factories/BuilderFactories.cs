using Domain.Base;
using Domain.Entities.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class BuilderFactories
    {

        
        private BuilderFactories() { }

        public static Usuario Usuario(string username, string password, bool active)
        {
            if(username == "" || password == "")
            {
                throw new Exception("Factories Usuario no puede ser creado");
            }
            return new Usuario(username, password, active);
        }

        public static Cliente Cliente(string Documento, string Nombre, string Email, int Usuario_Id)
        {

            if(Documento == ""|| Nombre == ""|| Email == ""|| Usuario_Id == 0){
                throw new Exception("Factories Cliente no puede ser creado");
            }

        
            var documento = new Domain.ValueObjects.Documento(Documento);
            var n = Nombre.Split(' ');

            if(n.Count() > 4 || n.Count() < 3)
            {
                throw new Exception("Factories Cliente no puede ser creado, nombre icompleto, permitido 'primerN segundoN primerA segundoA' o  'primerN primerA segundoA')");
            }

            var nombre = (n.ToList().Count() == 3) ? new Domain.ValueObjects.Nombre(n[0], " ", n[1], n[2]) : new Domain.ValueObjects.Nombre(n[0], n[1], n[2], n[3]);
            
            return new Cliente(documento,nombre, Email, Usuario_Id);
        }






        
    }



}
