using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace ChessClock
{
    public class PlayerTimer
    {
        public TimeSpan Time { get; private set; }
        private TimeSpan timeElapsed;
        private Stopwatch stopwatch;
        private int additionalTime;

        public PlayerTimer(int minutes, int addTime)
        {
            Time = new TimeSpan(0, minutes, 0);
            timeElapsed = new TimeSpan(0, 0, 0);
            stopwatch = new Stopwatch();
            additionalTime = addTime;
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void AddTime()
        {
            Time = Time.Add(TimeSpan.FromSeconds(additionalTime));
        }

        public void CountTime()
        {
            Time = Time.Subtract(stopwatch.Elapsed - timeElapsed);
            timeElapsed = stopwatch.Elapsed;
        }

    }
}
