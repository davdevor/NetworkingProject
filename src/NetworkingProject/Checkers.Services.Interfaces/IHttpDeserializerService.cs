using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Services.Interfaces
{
    public interface IHttpDeserializerService
    {
        /// <summary>
        /// gets a object of type T from making a request to the url
        /// </summary>
        /// <typeparam name="T">the type T to deserialize into</typeparam>
        /// <param name="url">the url of the request</param>
        /// <returns>the deserialized object from the http request</returns>
        Task<T> GetObjectFromRequestAsync<T>(string url);

    }
}
