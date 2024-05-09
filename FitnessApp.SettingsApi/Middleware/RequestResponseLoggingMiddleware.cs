using FitnessApp.Common.Middleware;
using Microsoft.AspNetCore.Http;

namespace FitnessApp.SettingsApi.Middleware
{
    public class RequestResponseLoggingMiddleware(RequestDelegate next) : AbstractRequestResponseLoggingMiddleware(next)
    {
        protected override string ObfuscateBodyText(RequestDirection requestDirection, string bodyText, string path)
        {
            return bodyText;
        }
    }
}
