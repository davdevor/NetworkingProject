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
        public T Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
