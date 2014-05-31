using System.IO;

namespace PipeNet
{
    static class GzipPipeExtensions
    {
        public static Stream PipeAsync(this Stream str, GzipPipeStream destinationStream)
        {
            var tsk = str.CopyToAsync(destinationStream);
            tsk.ContinueWith(_ => destinationStream.Completed());
            return destinationStream;
        }   
    }
}
