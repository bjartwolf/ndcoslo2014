#r "System.Net.Http"
using System.Net.Http;

var request = new HttpClient().GetStreamAsync("http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml");

request.Result.CopyTo(Console.OpenStandardOutput());

