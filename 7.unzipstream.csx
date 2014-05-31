#r "System.Net.Http"
#r "System.IO.Compression"
using System.IO.Compression;
using System.Net.Http;

var request = new HttpClient().GetStreamAsync("http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml.gz");

new GZipStream(request.Result, CompressionMode.Decompress).CopyTo(Console.OpenStandardOutput());

