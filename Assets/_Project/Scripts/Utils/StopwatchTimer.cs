namespace TheLastLand._Project.Scripts.Utils
{
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