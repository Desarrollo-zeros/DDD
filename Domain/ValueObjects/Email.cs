using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class EmailValueObject
    {
        public EmailValueObject(string email)
        {
            this.Email = email;
        }

       [Column("Email")] public string Email { set; get; }
    }
}
