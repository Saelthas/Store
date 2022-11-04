using Models;
using Newtonsoft.Json;
using System;

namespace Store.Models
{
    public class Response<T>

    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
    public class Response
    {
        public int State { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Exceptions { get; set; }

        public static Response Success(object data)
        {
            Response res = new Response
            {

                State = 0,
                Message = Validation.Exception(Validation.Exceptions.COMPLETED),
                Data = data,
                Exceptions = null
            };
            return res;
        }
                
        public static Response ExceptionGenerate(string data)
        {
            Response res = new Response
            {
                State = 1,
                Message = data,
                Data = null,
                Exceptions = data
            };
            return res;
        }
        public static Response Error(string message)
        {
            Response res = new Response
            {
                State = 1,
                Message = message,
                Data = null,
                Exceptions = null
            };
            return res;
        }
        public static Response ExceptionGenerate(string data, Validation.SuggestedMessages suggestedMessage)
        {
            Response res = new Response
            {
                State = 1,
                Message = Validation.SuggestedMessage(suggestedMessage),
                Data = null,
                Exceptions = data
            };
            return res;
        }

    }
}
