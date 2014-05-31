#r "System.Net.Http"
using System.Net.Http;
using System.Net.Http.Headers;

var client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("APPLICATION/JSON"));
var request = client.GetStreamAsync("http://localhost:8090/");
request.Result.CopyTo(Console.OpenStandardOutput());
