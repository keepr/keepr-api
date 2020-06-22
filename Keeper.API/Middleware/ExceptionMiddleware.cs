using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Keeper.API.Models;
using Microsoft.AspNetCore.Http;

namespace Keeper.API.Middleware
{
    public class ExceptionMiddleware: IMiddleware
    {

        public ExceptionMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context, sw);
            }
        }

        private async Task HandleException(Exception ex, HttpContext context, Stopwatch sw)
        {
            sw.Stop();

            if (context.Response.HasStarted)
            {
                throw ex;
            }

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var apiError = new ErrorModel(ex);

            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(apiError));
        }
    }
}
