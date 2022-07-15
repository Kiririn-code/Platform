using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platform.Services;

namespace Platform
{
	public class WeatherMiddleware
	{
		private RequestDelegate next;
		//private IResponseFormatter _formatter;

        public WeatherMiddleware(RequestDelegate request)
        {
			next = request;
			//_formatter = formatter;
        }

		public async Task Invoke(HttpContext context,IResponseFormatter formatter
			, IResponseFormatter formatter1, IResponseFormatter formatter2)
        {
			if(context.Request.Path == "/middleware/class")
            {
				await formatter.Format(context, string.Empty);
				await formatter1.Format(context, string.Empty);
				await formatter2.Format(context, string.Empty);
			}
			else
            {
				await next(context);
            }
        }
	}
}

