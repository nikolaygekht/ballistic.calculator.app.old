using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    static public class TraceInfoReader
    {
        static public TraceInfoCollection GetTraces(string server)
        {
            TraceInfoCollection collection = null;
            WebRequest request = WebRequest.Create("http://" + server + "/traces");
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    string responseFromServer = reader.ReadToEnd();
                    collection = new TraceInfoCollection(TraceInfoController.Serialization.XmlToArray(responseFromServer));
                    reader.Close();
                }
                dataStream.Close();
            }
            return collection;
        }
        
    }
}
