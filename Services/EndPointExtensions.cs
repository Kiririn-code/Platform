using Platform.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
	public static class EndPointExtensions
	{
        #region Map_Weather
        /**
        public static void MapWeather(this IEndpointRouteBuilder app, string path)
        {
			IResponseFormatter formatter = app.ServiceProvider.GetService<IResponseFormatter>();
			app.MapGet(path, context => 
				 Platform.WeatherEndpoint.Endpoint(context, formatter));
        }
        **/
        #endregion
        public static void MapEndpoint<T>(this IEndpointRouteBuilder app,string path,string methodName = "Endpoint")
        {
            MethodInfo info = typeof(T).GetMethod(methodName);
            if(info == null || info.ReturnType != typeof(Task)){
                throw new System.Exception("Method cannot be used");
            }
            T endpointInstance = ActivatorUtilities.CreateInstance<T>(app.ServiceProvider);
            ParameterInfo[] methodParams = info.GetParameters();
            app.MapGet(path, (context) =>
            {
                T endpointUtilites = ActivatorUtilities.CreateInstance<T>(context.RequestServices);
                return (Task)info.Invoke(endpointInstance, methodParams.Select(p =>
                 p.ParameterType == typeof(HttpContext) ? context :
                context.RequestServices.GetService(p.ParameterType)).ToArray());
            });
        }
    }
}

