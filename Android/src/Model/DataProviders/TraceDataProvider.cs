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
using Gehtsoft.BallisticCalculator.Connectivity;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.DataAccessors;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class TraceDataProvider
    {
        public TraceInfoCollection TraceInfoCollection { get; set; }
        public TraceInfo SelectedTraceInfo { get; set; }

        public AmmoInfo AmmoInfo
        {
            get
            {
                return new AmmoInfo(
                    SelectedTraceInfo.DrageTable,
                    SelectedTraceInfo.BallisticCoefficient,
                    SelectedTraceInfo.MuzzleVelocity,
                    SelectedTraceInfo.BulletWeight
                    );
            }
        }

        private ITraceDataAccessor _dataAccessor;

        public TraceDataProvider()
        {
            _dataAccessor = new TraceDataAccessor();
            TraceInfoCollection = new TraceInfoCollection();
        }

        public void SetSelectedTraceByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            if (TraceInfoCollection == null)
                return;

            int index = TraceInfoCollection.Find(name);

            if (index != -1)
                SelectedTraceInfo = TraceInfoCollection.ElementAt(index);
        }

        public void Save()
        {
            _dataAccessor.Write(TraceInfoCollection);
        }

        public void Load()
        {
            TraceInfoCollection tif = _dataAccessor.Read();
            if (tif != null && tif.Count > 0)
            {
                TraceInfoCollection = tif;

                if (SelectedTraceInfo == null)
                    return;

                int index = TraceInfoCollection.Find(SelectedTraceInfo.TraceName);
                if (index != -1)
                    SelectedTraceInfo = TraceInfoCollection[index];
                else
                    SelectedTraceInfo = null;
            }
        }
    }
}



