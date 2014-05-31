using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace FileStreamWebApi.Controllers
{
    public class FastController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetResult()
        {
            var fs = new FileStream(Path.Combine(HttpRuntime.AppDomainAppPath, "medline13n0701.xml.gz"), FileMode.Open, FileAccess.Read);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(fs)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            response.Content.Headers.ContentEncoding.Add("gzip");
            return response;
        }
    }
}