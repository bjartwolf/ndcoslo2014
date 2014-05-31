using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace PipeNet
{
    internal class GzipPipeStream : Stream
    {
        private readonly GZipStream _zipStream;
        private readonly BackingBuffer _backingStream;
        private bool _completed;
        private readonly object _myLock;

        public GzipPipeStream()
        {
            _myLock = new object();
            _backingStream = new BackingBuffer();
            _zipStream = new GZipStream(_backingStream, CompressionMode.Decompress);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

		public override async Task<int> ReadAsync (byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
		    if (!_completed)
		    {
	            while (_backingStream.Length < count * 2)
                {
                    // We must wait for some more data or until we are complete before we actually read
                    // Probably better with some sort of notifcation on Write...
                    await Task.Delay(100);
                }	        
		    }
            return await Task<int>.Factory.FromAsync (BeginRead, EndRead, buffer, offset, count, null);
		}

        public override int Read(byte[] buffer, int offset, int count)
        {
            // OMFG SHARED WRITABLE STATE. THROW LOCKS AT IT!
            // Joe Albahari says that this stuff should be designed properly peer reviewed but I wasn't
            // sure how to design it properly no-one wanted to review it 
            lock (_myLock)
            {
                return _zipStream.Read(buffer, offset, count);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            lock (_myLock )
            {
                _backingStream.Write(buffer, offset, count);
            }
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position { get; set; }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public void Completed()
        {
            _completed = true;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }


        protected override void Dispose(bool disposing)
        {
            _zipStream.Dispose();
            _backingStream.Dispose(); 
            base.Dispose(disposing);
        }
    }
}