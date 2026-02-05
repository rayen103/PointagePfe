using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Util
{
    public class MyBody<T, Z>
    {
        public string Status { get; set; }
        public string message { get; set; }
        public T results { get; set; }
        public Z result1 { get; set; }
    }
}