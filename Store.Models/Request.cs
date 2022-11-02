using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models
{
    public class Request<T>
    {
        public T Data { get; set; }

        public Request()
        {
        }


        public Request(T data)
        {
            this.Data = data;
        }
    }
}
