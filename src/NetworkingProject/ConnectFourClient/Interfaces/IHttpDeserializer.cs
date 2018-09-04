﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourClient.Interfaces
{
    interface IHttpDeserializer
    {
        Task<T> GetObjectAsync<T>(string url);
    }
}
