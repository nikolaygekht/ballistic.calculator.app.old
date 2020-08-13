using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.Connectivity;
using System;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class BallisticDataProvider
    {
        public static BallisticDataProvider Instance { get; private set; }

        static BallisticDataProvider()
        {
            Instance = new BallisticDataProvider();
            Instance.MeasurementSystem = MeasurementSystem.Imperial;
            Instance.AtmosphereData.AtmosphereInfo = DefaultValues.CreateAtmosphereInfo();
            Instance.AtmosphereData.WindInfo = DefaultValues.CreateWindInfo();
        }

        public AtmosphereDataProvider AtmosphereData { get; private set; }
        public BulletDataProvider BulletData { get; private set; }
        public ZeroDataProvider ZeroData { get; private set; }
        public TraceDataProvider TraceData { get; private set; }
        public ShotDataProvider ShotData { get; private set; }
        public BallisticInfoCollection BallisticInfo { get; set; }

        public MeasurementSystem MeasurementSystem { get; set; }

        private BallisticDataProvider()
        {
            BulletData = new BulletDataProvider();
            AtmosphereData = new AtmosphereDataProvider();
            ZeroData = new ZeroDataProvider();
            TraceData = new TraceDataProvider();
            ShotData = new ShotDataProvider();
        }

        public void Init()
        {
        }

        public void SaveTraces()
        {
            TraceData.Save();
        }

        public void LoadTraces()
        {
            TraceData.Load();
        }

        public void SetSelectedTraceByName(string name)
        {
            TraceData.SetSelectedTraceByName(name);
        }

        public TraceInfo GetTraceByIndex(int index)
        {
            if (index < 0)
                return null;

            return TraceData.TraceInfoCollection[index];
        }

        public void AddNewTraceInfoToCollection(TraceInfo traceInfo)
        {
            if (traceInfo == null)
                return;

            string traceName = traceInfo.TraceName;
            int index = TraceData.TraceInfoCollection.Find(traceName);

            if (index < 0)
            {
                TraceData.TraceInfoCollection.Add(traceInfo);
            }
            else
            {
                TraceData.TraceInfoCollection.RemoveAt(index);
                TraceData.TraceInfoCollection.Add(traceInfo);
            }
        }

        public string GetSelectedTraceNameOrDefault(string defaultName)
        {
            string result = defaultName;
            if (TraceData.SelectedTraceInfo != null)
                result = TraceData.SelectedTraceInfo.TraceName;

            return result;
        }

        public string GetSelectedTraceName()
        {
            return GetSelectedTraceNameOrDefault("");
        } 
    }
}