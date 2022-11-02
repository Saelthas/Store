using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public class BaseController : ControllerBase
    {
        protected ILogger logger;


        /// <summary>
        /// Gets by DI the depedencies
        /// </summary>
        /// <param name="logger"></param>
        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }


        /// <summary>
        /// Execute
        /// </summary>
        public IActionResult Execute<TRequest, TResponse>(Request<TRequest> request, Func<TRequest, Response> func)
        {
            try
            {
                var tiempoSolicitud = DateTime.Now;
                var result = func(request.Data);
                var tiempoRespuesta = DateTime.Now;

                return new OkObjectResult(new Response<TResponse>()
                {
                    State = result.State,
                    Data = (TResponse)result.Data,//==null? default(TResponse) :(TResponse)result.Data,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(new Response()
                {
                    State = 0,
                    Data = null,
                    Message = ex.Message
                });
            }
        }

        public IActionResult Execute<TResponse>(Func<Response> func)
        {
            try
            {
                var tiempoSolicitud = DateTime.Now;
                var result = func();
                var tiempoRespuesta = DateTime.Now;

                return new OkObjectResult(new Response<TResponse>()
                {
                    State = 1,
                    Data = (TResponse)result.Data,
                    Message = string.Empty
                });
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(new Response()
                {
                    State = 0,
                    Data = null,
                    Message = ex.Message
                });
            }
        }

    }
}
