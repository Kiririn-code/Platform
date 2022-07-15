using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
	public class TextResponseFormatter : IResponseFormatter
	{
        private static TextResponseFormatter _formatter;

        public static TextResponseFormatter Singleton
        {
            get
            {
                if (_formatter == null)
                    _formatter = new TextResponseFormatter();
                return _formatter;
            }
        }

        private int _counter = 0;

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Response {++_counter}:\n {content}");
        }
    }
}

