using System;

namespace TheLastLand._Project.Scripts.Utils
{
    public abstract class Timer
    {
        protected float InitialTime;
        public float Time { get; protected set; }
        public bool IsRunning { get; protected set; }

        public float Progress => Time / InitialTime;

        public Action OnStart = delegate { };
        public Action OnStop = delegate { };

        protected Timer(float initialTime)
        {
            InitialTime = initialTime;
            IsRunning = false;
        }

        public void Start()
        {
            Time = InitialTime;
            if (IsRunning) return;
            
            IsRunning = true;
            OnStart.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            OnStop.Invoke();
        }

        public void Resume() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }
}