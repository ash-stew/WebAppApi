using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAppApi.Middlewares
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class StatsMiddleware
	{
		private readonly RequestDelegate _next;

		public StatsMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{
			// handle the request- before executing the controller
			DateTime requestTime = DateTime.Now;
			var result = _next(httpContext); // anything after this executed after the controller
			
			// handle the response
			DateTime responseTime = DateTime.Now;
			TimeSpan processDuration = responseTime - requestTime;
			Console.WriteLine("[StatsMiddleware] Process Duration=" + processDuration.TotalMilliseconds + "ms");
			
			return result;
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	
}
