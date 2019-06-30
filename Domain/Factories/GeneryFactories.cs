using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class FactoryPattern<T>
       where T : class, new() 
    {
        public static T CreateInstance()
        {
            return new T();
        }
    }
    
}
