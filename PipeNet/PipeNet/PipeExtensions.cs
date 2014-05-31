using System.IO;

namespace PipeNet
{
    static class PipeExtensions
    {
        public static Stream PipeAsync(this Stream str, Stream destinationStream)
        {
            str.CopyToAsync(destinationStream);
            return destinationStream;
        }   
    }
}
