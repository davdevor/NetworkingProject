using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourClient.Interfaces;

namespace ConnectFourClient.concrete
{
    class HttpDeserializer : IHttpDeserializer
    {
        IDeserializer _deserializer;
        IHttpClient _httpClient;

        public HttpDeserializer(IDeserializer deserializer, IHttpClient httpClient)
        {
            _deserializer = deserializer;
            _httpClient = httpClient;
        }

        public async Task<T> GetObjectAsync<T>(string url)
        {
            return _deserializer.Deserialize<T>(await _httpClient.MakeRequestAsync(url));
        }
    }
}
