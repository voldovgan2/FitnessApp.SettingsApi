﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FitnessApp.Common.Middleware;
using Microsoft.AspNetCore.Http;
using Minio.Exceptions;

namespace FitnessApp.SettingsApi.Middleware;

public class ErrorHandlerMiddleware(RequestDelegate next) : AbstractErrorHandlerMiddleware(next)
{
    protected override Task HandleGlobalError(HttpContext context, Exception error)
    {
        return Task.CompletedTask;
    }

    protected override HttpStatusCode GetStatusCodeByError(Exception error)
    {
        return error switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,
            ForbiddenException => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
