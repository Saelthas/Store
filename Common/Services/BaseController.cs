using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        /// Executor of functions synchronous
        /// </summary>
        /// <typeparam name="TRequest">T</typeparam>
        /// <typeparam name="TResponse">T</typeparam>
        /// <param name="request">Request<T></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public IActionResult Execute<TRequest, TResponse>(Request<TRequest> request, Func<TRequest, Response> func)
        {
            try
            {
                var result = func(request.Data);
                if (result.Data == null)
                    return new BadRequestObjectResult(new Response()
                    {
                        State = result.State,
                        Data = null,
                        Message = result.Message
                    }); 
                return new OkObjectResult(new Response<TResponse>()
                {
                    State = result.State,
                    Data = (TResponse)result.Data,
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

        /// <summary>
        /// Executor of functions asynchronous
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(Request<TRequest> request, Func<TRequest, Task<Response>> func)
        {
            try
            {
                var result = await func(request.Data);
                if (result.Data == null)
                    return new BadRequestObjectResult(new Response()
                    {
                        State = result.State,
                        Data = null,
                        Message = result.Message
                    });
                return new OkObjectResult(new Response<TResponse>()
                {
                    State = result.State,
                    Data = (TResponse)result.Data,
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
                var result = func();

                return new OkObjectResult(new Response<TResponse>()
                {
                    State = result.State,
                    Data = (TResponse)result.Data,
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

    }
}
