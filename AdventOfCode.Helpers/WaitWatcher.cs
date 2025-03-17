namespace AdventOfCode.Helpers
{
    public class WaitWatcher
    {
        System.Timers.Timer timer;
        public System.Timers.Timer Timer { get => timer; }

        int iMaxWaitTime;
        bool bWaitComplete;

        public bool WaitComplete { get { return bWaitComplete; } }
        public int WaitTimeMs { get { return iMaxWaitTime; } }
        public int WaitTimeS { get { return iMaxWaitTime / 1000; } }

        public WaitWatcher(int _iMaxWaitTime = 10000) // Default wait time is 10s
        {
            iMaxWaitTime = _iMaxWaitTime;
            timer = new System.Timers.Timer(iMaxWaitTime);
            timer.AutoReset = false; // Single shot timer
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bWaitComplete = true;
        }

        public void Start()
        {
            bWaitComplete = false;
            timer.Start();
        }

        public void Stop()
        {
            bWaitComplete = false;
            timer.Stop();
        }

        public void SetMaxWaitTime(int _iMaxWaitTime)
        {
            iMaxWaitTime = _iMaxWaitTime;
            timer.Interval = iMaxWaitTime;
        }
    }
}
