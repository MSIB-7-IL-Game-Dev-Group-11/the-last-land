using System;

namespace TheLastLand._Project.Scripts.Utils
{
    public abstract class Timer
    {
        protected float initialTime;
        public float Time { get; protected set; }
        public bool IsRunning { get; protected set; }

        public float Progress => Time / initialTime;

        public Action OnStart = delegate { };
        public Action OnStop = delegate { };

        protected Timer(float initialTime)
        {
            this.initialTime = initialTime;
            IsRunning = false;
        }

        public void Start()
        {
            Time = initialTime;
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

    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning && !IsFinished)
            {
                Time -= deltaTime;
            }

            if (IsRunning && IsFinished)
            {
                Stop();
            }
        }

        public bool IsFinished => Time <= 0;

        public void Reset() => Time = initialTime;

        public void Reset(float value)
        {
            initialTime = value;
            Reset();
        }
    }

    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(float value) : base(value)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                Time += deltaTime;
            }
        }

        public void Reset() => Time = 0;
    }
}