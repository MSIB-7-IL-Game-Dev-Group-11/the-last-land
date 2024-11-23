namespace TheLastLand._Project.Scripts.Utils
{
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

        public void Reset() => Time = InitialTime;
        
        public void Reset(float value)
        {
            InitialTime = value;
            Reset();
        }
    }
}