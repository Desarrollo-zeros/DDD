using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CalcularPrimaException : Exception
    {
        public CalcularPrimaException()
        {
        }

        public CalcularPrimaException(string message) : base(message)
        {
        }

        public CalcularPrimaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CalcularPrimaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
