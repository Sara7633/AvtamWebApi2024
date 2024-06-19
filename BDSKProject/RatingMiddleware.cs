using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Service;
using System.IO;
using System.Threading.Tasks;

namespace BDSKProject
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating rating = new Rating()
            {
                Host = httpContext.Request.Host.Host,
                Path = httpContext.Request.Path,
                Method = httpContext.Request.Method,
                RecordDate = DateTime.UtcNow, // Update with appropriate date/time
                Referer = httpContext.Request.Headers["Referer"],
                UserAgent = httpContext.Request.Headers.UserAgent
            };
            ratingService.AddRating(rating);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RoutingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
