using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Domain.Interfaces
{
    public interface IDeserializer
    {
        /// <summary>
        /// takes a string and deserializes it into a type T object
        /// </summary>
        /// <typeparam name="T">the type to deseriaize to</typeparam>
        /// <param name="obj">the serialized object</param>
        /// <returns>The deserialzied object</returns>
        T Deserialize<T>(string obj);
    }
}
