using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public class TimeResponceFormatter : IResponseFormatter
    {
        private ITimeStamper timeStamper;

        public TimeResponceFormatter(ITimeStamper stamper)
        {
            timeStamper = stamper;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{timeStamper.TimeStamp} - {content}");
        }
    }
}

