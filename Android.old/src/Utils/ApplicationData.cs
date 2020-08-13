using System;
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Connectivity;
using System.Threading;
using Android.Content;
using MathEx.ExternalBallistic.Units;
using System.IO;
using System.Text;

namespace BallisticCalculator.Utils
{
	public class ApplicationData
	{
		private ApplicationData ()
        {
            SingleShotDistance = new Distance(0, Distance.DefaultUnit);
        }

		private static ApplicationData _instance = null;

		public static ApplicationData Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ApplicationData();
				return _instance;
			}

		}

        public void LoadTracesFromFile()
        {
            try
            {
                string path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "gehtsoft", "BallisticCalculator", "traces.xml");
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string tracesXML = sr.ReadToEnd();
                            TraceInfo[] traces = TraceInfoController.Serialization.XmlToArray(tracesXML);
                            _traceInfoCollection.Clear();
                            foreach (TraceInfo trace in traces)
                                _traceInfoCollection.Add(trace);
                        }
                    }
                }
            }
            catch
            {
            }
                 
        }

        public void SaveTracesToFile()
        {
            try
            {
                string path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "gehtsoft", "BallisticCalculator", "traces.xml");
                if (File.Exists(path))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(path);
                }
                string dir = System.IO.Path.GetDirectoryName(path);
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        string tracesXML = TraceInfoController.Serialization.ArrayToXml(_traceInfoCollection);
                        sw.WriteLine(tracesXML);
                    }
                }
            }
            catch
            {
            }
        }

        private TraceInfoCollection _traceInfoCollection = new TraceInfoCollection();
        private bool _needCalculateBallisticInfo = false;
        private BallisticInfoCollection _ballisticInfoCollection = null;
        private ShotInfoDataProvider _shotInfoDataProvider = new ShotInfoDataProvider();

        public enum EPrefferedUnits
        {
            Imperial = 0,
            Metric = 1
        }

        public EPrefferedUnits PrefferedUnits { get; set; }

		public TraceInfoCollection TraceInfoCollection
        {
            get
            {
                return _traceInfoCollection;
            }
        }

        public BallisticInfoCollection BallisticInfoCollection
        {
            get
            {
                if (_needCalculateBallisticInfo)
                    return null;
                return _ballisticInfoCollection;
            }
        }

        public bool calculateBallisticInfo(Action onCalculateFinished)
        {
            if (_shotInfoDataProvider.TraceInfo == null)
                return false;

            new Thread(() =>
            {
                if (_needCalculateBallisticInfo)
                {
            
                    _ballisticInfoCollection = ShotInfoController.Calculation.calculateShot(
                        _shotInfoDataProvider.ShotInfo);
                    _needCalculateBallisticInfo = false;
                    onCalculateFinished();
            
                }
                else
                {
                    onCalculateFinished();
                }
            }).Start();
            return true;
        }

        public bool calculateSingleShot(Distance range, Distance rangeStep, Velocity windSpeed, Angle windAngle, Action<BallisticInfo> onCalculateFinished)
        {
            if (_shotInfoDataProvider.TraceInfo == null)
                return false;

            new Thread(() =>
            {
                ShotInfo si = _shotInfoDataProvider.ShotInfo;
                si.Wind.Speed = windSpeed;
                si.Wind.Direction = windAngle;
                si.Step = rangeStep;
                SingleShotDistance = range;
                _needCalculateBallisticInfo = true;

                if (SingleShotDistance.Get(Distance.DefaultUnit) > 0)
                {

                    _ballisticInfoCollection = ShotInfoController.Calculation.calculateShot(si);
                    _needCalculateBallisticInfo = false;

                    double step = si.Step.Get(DefaultUnits.Range);
                    foreach (BallisticInfo info in _ballisticInfoCollection)
                    {
                        if (Math.Abs(info.Range.Get(DefaultUnits.Range) - range.Get(DefaultUnits.Range)) < step)
                        {
                            onCalculateFinished(info);
                            break;
                        }
                    }
                }
            }).Start();
            return true;
        }

		public TraceInfo SelectedTraceInfo
        {
            get
            {
                return _shotInfoDataProvider.TraceInfo;
            }
            
            set
            {
                if (_shotInfoDataProvider.TraceInfo != value)
                {
                    _needCalculateBallisticInfo = true;
                    _shotInfoDataProvider.TraceInfo = value;
                }
            }
        }

        public AtmosphereInfo AtmosphereInfo
        {
            set
            {
                if (_shotInfoDataProvider.AtmosphereInfo != value)
                {
                    _needCalculateBallisticInfo = true;
                    _shotInfoDataProvider.AtmosphereInfo = value;
                }
            }
            get
            {
                return _shotInfoDataProvider.AtmosphereInfo;
            }
        }

        public WindInfo WindInfo
        {
            set
            {
                if (_shotInfoDataProvider.WindInfo != value)
                {
                    _needCalculateBallisticInfo = true;
                    _shotInfoDataProvider.WindInfo = value;
                }
            }
            get
            {
                return _shotInfoDataProvider.WindInfo;
            }
        }

        public Distance SingleShotDistance { get; private set; }

        public Distance RangeStep
        {
            get
            {
                return _shotInfoDataProvider.ShotInfo.Step;
            }
        }

        public Distance RangeMax
        {
            get
            {
                return _shotInfoDataProvider.ShotInfo.MaxDistance;
            }
        }
	}
}

