using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platform.Services;

namespace Platform
{
    public class WeatherEndpoint
	{
		public async Task Endpoint(HttpContext context,IResponseFormatter formatter)
        {
			await formatter.Format(context, "Endpoint class: It is cloudy in milan");
        }
	}
}

