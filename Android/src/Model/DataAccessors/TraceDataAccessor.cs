using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic.Serialization.Windows;

using Environment = Android.OS.Environment;

namespace Gehtsoft.BallisticCalculator.DataAccessors
{
    // Read traces from various distanation points (e.g. a sd card, a phome memory, an internet/intranet network e.t.c.) 
    // Current implementation support reading from internal memory in android-Phone only.
    class TraceDataAccessor : ITraceDataAccessor
    {
        private const string FILE_NAME = "traces.xml";
        private const string COMPANY_NAME = "gehtsoft";
        private const string APPLICATION_NAME = "BallisticCalculator";
        private string _fullPath;

        public TraceDataAccessor()
        {
            _fullPath = Path.Combine(
               Environment.ExternalStorageDirectory.Path,
               COMPANY_NAME,
               APPLICATION_NAME,
               FILE_NAME
               );
        }


        public TraceInfoCollection Read()
        {
            if (!File.Exists(_fullPath))
                return null;

            string xml = string.Empty;

            try
            {
                using (FileStream fs = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader sr = new StreamReader(fs))
                    xml = sr.ReadToEnd();
            }
            catch
            {
                return null;
            }

            TraceInfo[] traces = TraceInfoController.Serialization.XmlToArray(xml);
            TraceInfoCollection result = new TraceInfoCollection();

            if (traces == null)
                return null;

            foreach (var trace in traces)
                result.Add(trace);

            return result.Count > 0 ? result : null;
        }

        public bool Write(TraceInfoCollection traceInfoCollection)
        {
            try
            {
                if (File.Exists(_fullPath))
                {
                    // Small fix: application can crash when a file hase been deleted
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(_fullPath);
                }

                string workingDir = System.IO.Path.GetDirectoryName(_fullPath);
                if (Directory.Exists(workingDir) == false)
                    Directory.CreateDirectory(workingDir);

                using (FileStream fs = new FileStream(_fullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        string tracesXML = TraceInfoController.Serialization.ArrayToXml(traceInfoCollection);
                        sw.WriteLine(tracesXML);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}