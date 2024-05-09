using FitnessApp.Common.Middleware;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace FitnessApp.SettingsApi.Middleware
{
    public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger logger) : AbstractRequestResponseLoggingMiddleware(next, logger)
    {
        protected override string ObfuscateBodyText(RequestDirection requestDirection, string bodyText, string path)
        {
            return bodyText;
        }
    }
}
