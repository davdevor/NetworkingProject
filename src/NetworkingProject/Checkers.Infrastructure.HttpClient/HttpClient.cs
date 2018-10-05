using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Domain.Interfaces;

namespace Checkers.Infrastructure.HttpClient
{
    public class HttpClient : IHttpClient
    {
        public async Task<string> MakeRequestAsync(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
