using System;
using System.IO;
using System.Text;

//http://www.codeproject.com/aspnet/RemovingWhiteSpacesAspNet.asp

namespace PixelDragons.Commons.IO
{
    public class FindReplaceStream : Stream
    {
        private readonly Stream stream;
        private readonly StreamWriter writer;
        private readonly string find;
        private readonly string replace;
        private readonly Encoding encoding;

        public FindReplaceStream(Stream stream, Encoding encoding, string find, string replace)
        {
            this.stream = stream;
            this.encoding = encoding;
            writer = new StreamWriter(this.stream, this.encoding);
            this.find = find;
            this.replace = replace;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            MemoryStream ms = new MemoryStream(buffer, offset, count, false);
            StreamReader sr = new StreamReader(ms, encoding);

            string html = sr.ReadToEnd();
            html = html.Replace(find, replace);

            writer.Write(html);
            writer.Flush();
        }

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
            stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}