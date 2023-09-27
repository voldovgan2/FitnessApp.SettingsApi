using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FitnessApp.Common.Middleware;
using FitnessApp.Common.Serializer.JsonSerializer;
using Microsoft.AspNetCore.Http;

namespace FitnessApp.SettingsApi.Middleware
{
    public class ErrorHandlerMiddleware : AbstractErrorHandlerMiddleware
    {
        public ErrorHandlerMiddleware(RequestDelegate next, IJsonSerializer serializer)
            : base(next, serializer) { }

        protected override Task HandleGlobalError(HttpContext context, Exception error)
        {
            return Task.CompletedTask;
        }

        protected override HttpStatusCode GetStatusCodeByError(Exception error)
        {
            return error switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
