using System.IO;

namespace PipeNet
{
    class BackingBuffer: Stream 
    {
        private long _lastReadPosition;
        private long _lastWritePosition;
        private MemoryStream _memoryStream;

        public BackingBuffer()
        {
            _memoryStream = new MemoryStream();
        }

        public override void Flush()
        {
            throw new System.NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            _memoryStream.Position = _lastReadPosition;
            int bytesRead = _memoryStream.Read(buffer, offset, count);
            _lastReadPosition = _memoryStream.Position;
            return bytesRead;               
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _memoryStream.Position = _lastWritePosition;
            _memoryStream.Write(buffer, offset, count);
            _lastWritePosition = _memoryStream.Position;
            if (_lastReadPosition > 10000000)
            {
                var newStream = new MemoryStream();
                _memoryStream.Position = _lastReadPosition;
                _memoryStream.CopyTo(newStream);
                _lastWritePosition -= _lastReadPosition; 
                _lastReadPosition = 0; 

                _memoryStream = newStream;
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return _lastWritePosition - _lastReadPosition; }
        }

        public override long Position { get; set; }
    }
}
