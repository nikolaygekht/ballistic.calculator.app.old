using System;
using System.Collections.Generic;
using System.Text;

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    public class ConnectivityHttpServer : HttpServer
    {
        TraceInfoCollection mCollection;

        public ConnectivityHttpServer(TraceInfoCollection collection, int port)
            : base(port)
        {
            mCollection = collection;
        }

        public override void handleGETRequest(HttpProcessor p)
        {
            if (p.http_url == "/traces")
            {
                p.writeSuccess();
                string s = TraceInfoController.Serialization.ArrayToXml(mCollection);
                p.outputStream.Write("{0}", s);
                RaiseInfo("request handled");
            }
            else
            {
                RaiseError("unknown request");
                p.writeFailure();
            }
        }

        public override void handlePOSTRequest(HttpProcessor p, System.IO.StreamReader inputData)
        {
            RaiseError("unknown request");
        }
    }
}
