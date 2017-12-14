using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StubSIM2UNET
{
    public class PcapPlayer
    {

        public string Destination { get; set; }
        public string FileName { get; set; }
        public int Count { get; set; }
        public List<String> Lines { get; set; }

        public PcapPlayer(string _destination, string _filename)
        {
            Destination = _destination;
            FileName = _filename;
            Lines = new List<string>();
            Lines.Clear();
            Count = 0;
               /// count the number of lines in the pcap file
                using (StreamReader r = new StreamReader(FileName))
                {
                    string record;
                    while ((record = r.ReadLine()) != null)
                    {

                    Lines.Add(Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(record)));
                        Count++;
                    }
                r.Close();
                }
        }

        /// <summary>
        /// Reads the PCAP file and creates HTTPWebRequest and gets the response from server
        /// </summary>
        /// <returns>a string representing the results coming back from server</returns>
        public string Play()
        {
            string result = string.Empty;

            try
            {
                string url = Destination;

                if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                    url = "http://" + url;

    

                HttpWebRequest request;

                using (FileStream reader = new FileStream(FileName, FileMode.Open))
                {
                    string line = ReadLine(reader);
                    string[] components = line.Split('\\'); //   (' ');

                    url += components[1];

                    request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Method = components[0];

                    if (components.Length > 2)
                    {
                        switch (components[2].ToLower())
                        {
                            case "http/1.0":
                                request.ProtocolVersion = HttpVersion.Version10;
                                break;
                            case "http/1.1":
                                request.ProtocolVersion = HttpVersion.Version11;
                                break;
                        }
                    }

                    while (reader.Position < reader.Length)
                    {
                        line = ReadLine(reader);

                        if (string.IsNullOrEmpty(line))
                            break;
                        else
                            SetHeader(line, request);
                    }

                    byte[] buffer = new byte[4096];
                    int bytesread = 0;
                    Stream Body = request.GetRequestStream();

                    while (reader.Position < reader.Length)
                    {
                        bytesread = reader.Read(buffer, 0, buffer.Length);
                        if (bytesread > 0)
                            Body.Write(buffer, 0, bytesread);
                    }

                    Body.Close();
                    reader.Close();
                }

                if (request != null)
                {
                    // Get the response
                    using (HttpWebResponse wRes = (HttpWebResponse)request.GetResponse())
                    {
                        result = GetResponse(wRes);
                        wRes.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result = GetExceptionMessage(ex);
            }

            return result;
        }

        /// <summary>
        /// Read from file until reaching a line break
        /// </summary>
        /// <param name="fstream">FileStream to read from</param>
        /// <returns>the line of text that was just read</returns>
        private string ReadLine(FileStream fstream)
        {
            long pos1 = fstream.Position;

            byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);

            while (fstream.Position < fstream.Length)
            {
                if (fstream.ReadByte() == newline[0] && fstream.ReadByte() == newline[1])
                    break;
            }

            long pos2 = fstream.Position;
            fstream.Position = pos1;

            byte[] buffer = new byte[pos2 - pos1 - 2];
            fstream.Read(buffer, 0, buffer.Length);

            fstream.Position = pos2;

            return Encoding.ASCII.GetString(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Some known headers need to be parsed and assigned to Request properties
        /// </summary>
        /// <param name="line">Header Line to be processed</param>
        /// <param name="request">HttpWebRequest object which is being configured</param>
        private void SetHeader(string line, HttpWebRequest request)
        {
            string[] header = line.Split(':');
            string value = header[1].Trim();
            switch (header[0].ToLower())
            {
                case "content-type":
                    request.ContentType = value;
                    break;
                case "accept":
                    request.Accept = value;
                    break;
                case "expect":
                    if (value.Equals("100-continue", StringComparison.OrdinalIgnoreCase))
                        request.ServicePoint.Expect100Continue = true;
                    break;
                case "host":
                    request.Host = value;
                    break;
                case "user-agent":
                    request.UserAgent = value;
                    break;
                case "connection":
                    switch (value.ToLower())
                    {
                        case "keep-alive":
                            request.KeepAlive = true;
                            break;
                        case "close":
                            request.KeepAlive = false;
                            break;
                        default:
                            request.Connection = value;
                            break;
                    }
                    break;
                case "content-length":
                    request.ContentLength = long.Parse(value);
                    break;
                default:
                    request.Headers.Add(line);
                    break;
            }
        }

        /// <summary>
        /// Try to present most complete error message
        /// </summary>
        /// <param name="ex">Exception which was captured</param>
        /// <returns>string containing the error message</returns>
        private string GetExceptionMessage(Exception ex)
        {
            string result = string.Empty;
            if (ex is WebException)
            {
                WebResponse errResp = ((WebException)ex).Response;
                result = GetResponse((HttpWebResponse)errResp);
            }
            if (string.IsNullOrEmpty(result))
            {
                result = GetErrorStackTrace(ex);
            }
            return result;
        }

        /// <summary>
        /// Builds output based on the response and includes headers
        /// </summary>
        /// <param name="wRes">HttpWebResponse</param>
        /// <returns>string representing server response</returns>
        protected string GetResponse(HttpWebResponse wRes)
        {
            StringBuilder result = new StringBuilder();

            if (wRes != null)
            {
                result.Append("HTTP/").Append(wRes.ProtocolVersion).Append(" ").Append((int)wRes.StatusCode).Append(" ").Append(wRes.StatusDescription).AppendLine();

                foreach (string h in wRes.Headers.AllKeys)
                {
                    result.Append(h).Append(": ").Append(wRes.Headers[h]).AppendLine();
                }
                result.AppendLine();

                Stream ResponseStream = wRes.GetResponseStream();
                using (StreamReader stReader = new StreamReader(ResponseStream))
                {
                    result.Append(stReader.ReadToEnd());
                    stReader.Close();
                }
                ResponseStream.Close();
                wRes.Close();
            }

            return result.ToString();
        }

        public static string GetErrorStackTrace(Exception ex)
        {
            if (ex == null)
                return "";
            return ex.Message + "\r\n" + ex.StackTrace + "\r\n" + GetErrorStackTrace(ex.InnerException);
        }

    }
}
