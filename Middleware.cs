using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
	public class QueryStringMiddleWare
	{
		private RequestDelegate next;
		public QueryStringMiddleWare() { }

		public QueryStringMiddleWare(RequestDelegate request)
		{
			next = request;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Method == HttpMethods.Get
				&& context.Request.Query["custom"] == "true")
			{
				await context.Response.WriteAsync("Class-based middleware \n");
			}
			if (next != null)
				await next(context);
		}
	}
	public class LocationMiddleWare
	{
		private RequestDelegate next;
		private MassageOptions options;
        public LocationMiddleWare(RequestDelegate req,IOptions<MassageOptions> opt)
        {
			next = req;
			options = opt.Value;
        }

		public async Task Invoke(HttpContext context)
        {
			if (context.Request.Path == "/locations")
				await context.Response.WriteAsync($"{options.CityName},{options.CountryName}");
			else
				await next(context);
        }
	}
}
