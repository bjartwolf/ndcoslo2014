#r "System.Net.Http"
using System.Net.Http;

var client = new HttpClient();
var request = client.GetStringAsync("http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml");
Console.WriteLine(request.Result);
