using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ConnectFourClient.Interfaces;
namespace ConnectFourClient.concrete
{
    class HttpClient : IHttpClient
    {
        public async Task<string> MakeRequestAsync(string url)
        {
            string x;
            using (var client = new System.Net.Http.HttpClient())
            {
               x = await client.GetStringAsync(url);
            }
            return x;
        } 
    }
}
