using System;
using System.IO;
using System.Text;

//http://www.codeproject.com/aspnet/RemovingWhiteSpacesAspNet.asp
namespace PixelDragons.Commons.IO
{
    public class FindReplaceStream : Stream
    {
        private Stream _stream;
        private StreamWriter _writer;
        private string _find;
        private string _replace;
        private Encoding _encoding;

        public FindReplaceStream(Stream stream, Encoding encoding, string find, string replace)
        {
            _stream = stream;
            _encoding = encoding;
            _writer = new StreamWriter(_stream, _encoding);
            _find = find;
            _replace = replace;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            MemoryStream ms = new MemoryStream(buffer, offset, count, false);
            StreamReader sr = new StreamReader(ms, _encoding);

            string html = sr.ReadToEnd();
            html = html.Replace(_find, _replace);

            _writer.Write(html);
            _writer.Flush();            
        }

        #region Overrides
        //This stream is write-only
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        { 
            get { return false; } 
        }

        public override bool CanSeek
        { 
            get { return false; } 
        }

        public override bool CanWrite
        { 
            get { return true; } 
        }

        public override long Length
        { 
            get { throw new NotSupportedException(); } 
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
