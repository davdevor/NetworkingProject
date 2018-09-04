using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourClient.Interfaces;
using ConnectFourClient.concrete;
using System.Configuration;
namespace ConnectFourClient
{
    class ConnectFourClient
    {
        public static void Main(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["BaseAPIUrl"];
            IHttpClient httpClient = new HttpClient();
            IHttpDeserializer httpDeserializer = new HttpDeserializer(new JSONDeserializer(), httpClient);
            ConnectFourClientGame cg = new ConnectFourClientGame(new ConnectFourAPIRepository(baseUrl, httpDeserializer, httpClient));
            cg.StartAsync().Wait();
        }
    }
}
