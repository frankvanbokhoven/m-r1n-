using System;
using System.ServiceModel;

namespace TestMTOM
{
    /// <summary>
    /// We need to provide more info (parameters) with the upload than only the stream.
    /// Normally, .net only allows one parameter when this parameter is of type Stream
    /// The workaround for this is to add the data via a messagecontract class
    /// see: https://social.msdn.microsoft.com/Forums/vstudio/en-US/ef4cfee3-4d96-44a5-9fa1-ebafdff9bee3/wcf-stream-with-more-than-one-parameter?forum=wcf
    /// </summary>
    [MessageContract]
    public class SaveFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;
        [MessageHeader(MustUnderstand = true)]
        public long Length;
        [MessageHeader(MustUnderstand = true)]
        public string MimeType;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}