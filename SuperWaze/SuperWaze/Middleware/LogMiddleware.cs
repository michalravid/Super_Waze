using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;

namespace SuperWaze.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // רישום פרטי הבקשה
            Log.Information("Request: {Method} {Path}", context.Request.Method, context.Request.Path);

            // שליחת הבקשה ל-middleware הבא
            await _next(context);

            // רישום פרטי התגובה
            Log.Information("Response: {StatusCode}", context.Response.StatusCode);
        }
    }
}

