using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Hulk
{
    using AppFunc = Func<IDictionary<string,object>, Task>;
    public class AllCaps
    {
        private readonly AppFunc _next;

        public AllCaps(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            // Just so we don't have to do
            //((Stream)env["owin.ResponseBody"])
            // (IDictionary<string, string[]>)env["owin.RequestHeaders"];
            var ctx = new OwinContext(env); 
            var accepts = ctx.Request.Accept;
            if (accepts != null && accepts == accepts.ToUpper())
            {
                // Whenever someone thinks they write to the response....
                ctx.Response.Body = new CapsStream(ctx.Response.Body);
            }
            await _next(env);
        }
    }
}