using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourClient.Interfaces;
using System.Web.Script.Serialization;
namespace ConnectFourClient.concrete
{
    class JSONDeserializer : IDeserializer
    {
        public T Deserialize<T>(string obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return (T)serializer.Deserialize<T>(obj);
        }
    }
}
