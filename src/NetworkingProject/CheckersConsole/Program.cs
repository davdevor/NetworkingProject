using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersConsole.Interfaces;
using CheckersConsole.concrete;
using System.Configuration;
namespace CheckersConsole
{
    class CheckersConsole
    {
        public static void Main(string[] args)
        {
            
            string baseUrl = ConfigurationManager.AppSettings["BaseAPIUrl"];
            IHttpClient httpClient = new HttpClient();
            IHttpDeserializer httpDeserializer = new HttpDeserializer(new JSONDeserializer(), httpClient);
            CheckersClientGame cg = new CheckersClientGame(new CheckersRepository(baseUrl, httpDeserializer, httpClient));
            cg.StartAsync().Wait();
        }
    }
}