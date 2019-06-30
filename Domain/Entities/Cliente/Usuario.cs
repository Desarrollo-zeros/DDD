using Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities.Cliente
{
    public class Usuario : Entity<int>
    {
        public Usuario()
        {
        }

        public Usuario(string username, string password, bool active)
        {
            Username = username;
            Password = password;
            Active = active;
        }

        public string Username { get;  set; }

        public string Password { get;  set; }
        public bool Active { get;  set; }


        public void Activate() => this.Active = true;

        public void DeActivate() => this.Active = false;

        public bool Authenticate(string username, string password)
        {
            return (Username == username && Password == EncryptPassword(password));
        }

        public string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += "|54be1d80-b6d0-45c0-b8d7-13b3c798729f";
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(password));
            var sbString = new System.Text.StringBuilder();

            for (var i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));

            return sbString.ToString();
        }

       
    }
}
