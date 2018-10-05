using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Services.Interfaces;
using Checkers.Domain.Interfaces;

namespace Checkers.Services.HttpDeserializerService
{

    public class HttpDeserializer : IHttpDeserializerService
    {
        private readonly IHttpClient _httpClient;
        private readonly IDeserializer _deserializer;

        
        public HttpDeserializer(IHttpClient httpClient, IDeserializer deserializer)
        {
            _httpClient = httpClient;
            _deserializer = deserializer;
        }

        /// <summary>
        /// gets a object of type T from making a request to the url
        /// </summary>
        /// <typeparam name="T">the type T to deserialize into</typeparam>
        /// <param name="url">the url of the request</param>
        /// <returns>the deserialized object from the http request</returns>
        public async Task<T> GetObjectFromRequestAsync<T>(string url)
        {
            return  _deserializer.Deserialize<T>(
                await _httpClient.MakeRequestAsync(url)
                );
        }
    }
}
