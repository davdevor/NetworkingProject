using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersConsole.Interfaces;
using System.Xml.Serialization;
using System.IO;
namespace ConnectFourClient.concrete
{
    class XMLDeserializer : IDeserializer
    {
        public T Deserialize<T>(string obj)
        {   
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T returnObj = default(T);
            using (TextReader reader = new StringReader(obj))
            {
                returnObj = (T)xmlSerializer.Deserialize(reader);
            }
            return returnObj;
        }
    }
}
