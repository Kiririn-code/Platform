using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
	public class HtmlResponseFormatter : IResponseFormatter
	{

        public async Task Format(HttpContext context, string content)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($@"
            <!DOCKTYPE html>
            <html lang=""en"">
            <head><title>Response</title></head>
            <body>
            <h2> Formatter Response </h2>
            <span>{content}</span>
            </body>");
        }
        public bool RichOutput => true;
    }
}

