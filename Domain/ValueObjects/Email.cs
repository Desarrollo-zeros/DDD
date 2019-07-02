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
       [Column("Email")] public static string Email { set; get; }
    }
}
