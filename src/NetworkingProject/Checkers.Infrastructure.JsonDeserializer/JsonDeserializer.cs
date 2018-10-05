using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Domain.Interfaces;
using Newtonsoft.Json;

namespace Checkers.Infrastructure.JsonDeserializer
{
    public class JsonDeserializer : IDeserializer
    {
        /// <summary>
        /// takes a string and deserializes it into a type T object
        /// </summary>
        /// <typeparam name="T">the type to deseriaize to</typeparam>
        /// <param name="obj">the serialized object</param>
        /// <returns>The deserialzied object</returns>
        public T Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
