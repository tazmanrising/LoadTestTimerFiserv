using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimersAlias = System.Timers;

namespace EldosFileLib
{
    public class StopWatch
    {
        private readonly TimersAlias.Timer _Timer;
        public int RunForMinutes { get; private set; }
        public int CurrentCount { get; private set; }
        public Action ElapsedEvent { get; private set; }
        public bool IsRunning
        {
            get
            {
                return _Timer.Enabled;
            }
        }

        public StopWatch(int runForMinutes, Action elapsedEvent = null)
        {
            _Timer = new TimersAlias.Timer() { AutoReset = false };

            RunForMinutes = runForMinutes;
            ElapsedEvent = elapsedEvent;
            Initialize();

            if (RunForMinutes == 0)
                _Timer.Enabled = false;
        }

        public void Initialize()
        {
            _Timer.Interval = 60 * 1000; //1min
            _Timer.Elapsed += OnTimerElapsed;
            Reset();
        }

        public void Reset()
        {
            CurrentCount = 0;
            _Timer.Enabled = true;
        }

        private void OnTimerElapsed(object source, TimersAlias.ElapsedEventArgs e)
        {
            if (ElapsedEvent != null)
                ElapsedEvent();

            CurrentCount++;

            if (CurrentCount >= RunForMinutes)
                _Timer.Enabled = false;
            else
                _Timer.Enabled = true;
        }
    }
}
