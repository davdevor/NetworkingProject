using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersConsole.Interfaces
{
   public  interface IDeserializer
    {
        T Deserialize<T>(string obj);

    }
}
