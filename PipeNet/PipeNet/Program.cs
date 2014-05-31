using System;
using System.IO;
using System.Net.Http;

namespace PipeNet
{
    class Program
    {
        static void Main()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            var request = client.GetStreamAsync("http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml.gz");
            request.Result.PipeAsync(new GzipPipeStream()).PipeAsync(Console.OpenStandardOutput());

            //var request2 = client.GetStreamAsync("http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml.gz");
            //var outfile = File.Create(@"c:\ndcoslo\test.out");
            //request2.Result.PipeAsync(new GzipPipeStream()).CopyToAsync(outfile);

            Console.ReadLine();
        }
    }
}