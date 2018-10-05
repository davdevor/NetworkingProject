using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Domain.Interfaces
{
    public interface IHttpClient
    {
        /// <summary>
        /// makes a http request and returns it as a string
        /// </summary>
        /// <param name="url">url for the request</param>
        /// <returns></returns>
        Task<string> MakeRequestAsync(string url);

    }
}
