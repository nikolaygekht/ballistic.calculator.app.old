using System.Net.Sockets;
using System.Text;
using System.Threading;
using Android.OS;
using Thread = System.Threading.Thread;

namespace Gehtsoft.BallisticCalculator.Utils
{

    interface IServiceFinderDelegate
    {
        void serviceFound(string serviceName, string serviceAddress, string servicePort);
    }

    class ServiceFinder
    {
        private string _serviceName;
        private string _serviceAddress;
        private string _servicePort;
        private Thread _thread;
        private UdpClient _udpClient;
        private volatile bool _isStopRequested;

        private const int _udpPort = 15873;

        public IServiceFinderDelegate Delegate;

        public ServiceFinder(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void start()
        {
            _thread = new Thread(() =>
            {
                try
                {
                    _udpClient = new UdpClient(_udpPort);
                    string message;

                    while (!_isStopRequested)
                    {
                        message = "";
                        var result = _udpClient.ReceiveAsync();
                        if (!_isStopRequested)
                            message = Encoding.ASCII.GetString(result.Result.Buffer);
                        if (!_isStopRequested)
                            callDelegate(message);
                        
                    }
                }
                catch (Java.Lang.Exception)
                {
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                }
                finally
                {
                    try
                    {
                        _udpClient.Close();
                    }
                    catch (System.Exception)
                    {
                    }
                }
            });

            _thread.Start();

        }

        public void stop()
        {
            _isStopRequested = true;
            _thread.Interrupt();
        }

        public void callDelegate(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            string[] parties = message.Split(';');

            if (parties == null)
                return;

            if (parties.Length != 3)
                return;

            foreach (var part in parties)
                if (string.IsNullOrEmpty(part))
                    return;

            if (parties[0] == _serviceName && Delegate != null)
            {
                new Handler(Looper.MainLooper).Post(() => {
                    Delegate.serviceFound(parties[0], parties[1], parties[2]);
                });
            }

        }

    }
}