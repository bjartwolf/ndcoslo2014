#r "System.Net.Http"
using System.Net.Http;

var client = new HttpClient();
var request = client.GetStreamAsync("http://localhost:8090");
request.Result.CopyTo(Console.OpenStandardOutput());
