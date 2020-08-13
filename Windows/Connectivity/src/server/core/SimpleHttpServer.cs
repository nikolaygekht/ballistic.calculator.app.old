using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//https://github.com/jeske/SimpleHttpServer

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    public enum HttpServerEventType
    {
        Info,
        Warning,
        Error
    }


    public class HttpServerEventArgs : EventArgs
    {
        private DateTime mTime;
        private string mMessage;
        private HttpServerEventType mEventType;

        public DateTime Time
        {
            get
            {
                return mTime;
            }
        }

        public string Message
        {
            get
            {
                return mMessage;
            }
        }

        public HttpServerEventType Severity
        {
            get
            {
                return mEventType;
            }
        }
        
        public HttpServerEventArgs(HttpServerEventType eventType, string message) : base()
        {
            mTime = DateTime.Now;
            mMessage = message;
            mEventType = eventType;
        }

        static public HttpServerEventArgs Info(string message)
        {
            return new HttpServerEventArgs(HttpServerEventType.Info, message);
        }

        static public HttpServerEventArgs Warning(string message)
        {
            return new HttpServerEventArgs(HttpServerEventType.Warning, message);
        }

        static public HttpServerEventArgs Error(string message)
        {
            return new HttpServerEventArgs(HttpServerEventType.Error, message);
        }
    }

    public delegate void HttpServerEventDelegate(object sender, HttpServerEventArgs e);

    public class HttpProcessor
    {
        public TcpClient socket;
        public HttpServer srv;

        private Stream inputStream;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();


        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient s, HttpServer srv)
        {
            this.socket = s;
            this.srv = srv;
        }

        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }

        public void process()
        {
            try
            {
                string remote = ((IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString();
                srv.RaiseInfo("client at " + remote + " connected");
            }
            catch (Exception e)
            {
                srv.RaiseError(e.ToString());
            }

            inputStream = new BufferedStream(socket.GetStream());
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try
            {
                parseRequest();
                readHeaders();
                if (http_method.Equals("GET"))
                {
                    handleGETRequest();
                }
                else if (http_method.Equals("POST"))
                {
                    handlePOSTRequest();
                }
            }
            catch (Exception e)
            {
                srv.RaiseError(e.ToString());
                writeFailure();
            }
            outputStream.Flush();
            // bs.Flush(); // flush any remaining output
            inputStream = null; outputStream = null; // bs = null;            
            socket.Close();
        }

        public void parseRequest()
        {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];
        }

        public void readHeaders()
        {
            String line;
            while ((line = streamReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                httpHeaders[name] = value;
            }
        }

        public void handleGETRequest()
        {
            srv.handleGETRequest(this);
        }

        private const int BUF_SIZE = 4096;

        public void handlePOSTRequest()
        {
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length"))
            {
                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                if (content_len > MAX_POST_SIZE)
                {
                    throw new Exception(
                        String.Format("POST Content-Length({0}) too big for this simple server",
                          content_len));
                }
                byte[] buf = new byte[BUF_SIZE];
                int to_read = content_len;
                while (to_read > 0)
                {
                    int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                    if (numread == 0)
                    {
                        if (to_read == 0)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("client disconnected during post");
                        }
                    }
                    to_read -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            srv.handlePOSTRequest(this, new StreamReader(ms));
        }

        public void writeSuccess(string content_type = "text/html")
        {
            // this is the successful HTTP response line
            outputStream.WriteLine("HTTP/1.0 200 OK");
            // these are the HTTP headers...          
            outputStream.WriteLine("Content-Type: " + content_type);
            outputStream.WriteLine("Connection: close");
            // ..add your own headers here if you like
            outputStream.WriteLine(""); // this terminates the HTTP headers.. everything after this is HTTP body..
        }

        public void writeFailure()
        {
            // this is an http 404 failure response
            outputStream.WriteLine("HTTP/1.0 404 File not found");
            // these are the HTTP headers
            outputStream.WriteLine("Connection: close");
            // ..add your own headers here
            outputStream.WriteLine(""); // this terminates the HTTP headers.
        }
    }

    public abstract class HttpServer
    {
        IPAddress addr;
        protected int port;
        private TcpListener listener;
        private bool is_active = true;
        private Thread mThread;
        private string name;
        private bool mStopRequested;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public HttpServer(int port)
        {
            this.addr = GetLocalIPAddress();
            this.port = port;
            name = addr.ToString() + ":" + port;

        }

        public void Start()
        {
            mThread = new Thread(this.Listen);
            mThread.Start();
        }

        internal static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void Listen()
        {
            mStopRequested = false;
            RaiseInfo("Server started");
            try
            {
                listener = new TcpListener(addr, port);
                listener.Start();
                while (is_active)
                {
                    TcpClient s = listener.AcceptTcpClient();
                    HttpProcessor processor = new HttpProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                if (!mStopRequested)
                    RaiseError(e.ToString());
            }
            finally
            {
                is_active = false;
                listener = null;
                RaiseInfo("Server stopped");
                mStopRequested = false;
            }
        }

        public bool IsRunning
        {
            get
            {
                return is_active;
            }
        }

        public void Terminate()
        {
            mStopRequested = true;
            is_active = false;
            if (listener != null)
                listener.Stop();
            mThread.Abort();
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);

        public event HttpServerEventDelegate OnEvent;

        public void RaiseInfo(string message)
        {
            if (OnEvent != null)
                OnEvent(this, HttpServerEventArgs.Info(message));
        }

        public void RaiseWarning(string message)
        {
            if (OnEvent != null)
                OnEvent(this, HttpServerEventArgs.Warning(message));
        }

        public void RaiseError(string message)
        {
            if (OnEvent != null)
                OnEvent(this, HttpServerEventArgs.Error(message));
        }
    }
}