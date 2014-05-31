using System;
using System.IO;
using System.Text;

namespace Hulk
{
    class CapsStream: Stream
    {
        private readonly Stream _stream;
        private readonly Decoder _decoder;
        public CapsStream(Stream stream)
        {
            _stream = stream;
            _decoder = Encoding.UTF8.GetDecoder();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
          int charCount = _decoder.GetCharCount(buffer, offset, count);
          var chars = new Char[charCount];
          _decoder.GetChars(buffer, offset, count, chars, 0);

          var caps = new string(chars).ToUpper();
          var capsBuffer = Encoding.UTF8.GetBytes(caps); 
          _stream.Write(capsBuffer,0,capsBuffer.Length);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Position { get; set; }
    }
}