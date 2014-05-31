#r "System.Net.Http"
using System.Net.Http;

var client = new HttpClient();
var request = client.GetStringAsync("http://localhost:8090/");
Console.WriteLine(request.Result);
