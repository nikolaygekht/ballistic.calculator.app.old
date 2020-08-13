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

using System.Timers;

namespace BallisticCalculator.Utils
{
    class StoppableTimer
    {
        private Timer _timer = new Timer();
        private volatile bool _stopRequested = false;

        public delegate void ElapsedEventHandler();
        public event ElapsedEventHandler ElapsedEvent;
        
        public StoppableTimer(TimeSpan interval)
        {
            _timer.Elapsed += InternalElapsedEventHandler;
            _timer.AutoReset = false;
            _timer.Interval = interval.TotalMilliseconds;
            _timer.Enabled = false;
        }

        private void InternalElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            if (_stopRequested == false)
            {
                if (ElapsedEvent != null)
                {
                    ElapsedEvent();
                }
            }
        }

        public void Start()
        {
            _stopRequested = false;
            _timer.Start();
        }

        public void Stop()
        {
            
            _stopRequested = true;
            _timer.Stop();
        }
    }
}