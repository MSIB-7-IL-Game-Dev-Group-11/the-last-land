using System;

namespace TheLastLand._Project.Scripts.Utils
{
    /// <summary>
    /// Abstract class representing a timer.
    /// </summary>
    public abstract class Timer
    {
        /// <summary>
        /// The initial time set for the timer.
        /// </summary>
        protected float InitialTime;

        /// <summary>
        /// Gets or sets the current time of the timer.
        /// </summary>
        public float Time { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the timer is running.
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets the progress of the timer as a fraction of the initial time.
        /// </summary>
        public float Progress => Time / InitialTime;

        /// <summary>
        /// Event triggered when the timer starts.
        /// </summary>
        public Action OnStart = delegate { };

        /// <summary>
        /// Event triggered when the timer stops.
        /// </summary>
        public Action OnStop = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class with the specified initial time.
        /// </summary>
        /// <param name="initialTime">The initial time set for the timer.</param>
        protected Timer(float initialTime)
        {
            InitialTime = initialTime;
            IsRunning = false;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            Time = InitialTime;
            if (IsRunning) return;

            IsRunning = true;
            OnStart.Invoke();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            OnStop.Invoke();
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume() => IsRunning = true;

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause() => IsRunning = false;

        /// <summary>
        /// Abstract method to update the timer by a specified delta time.
        /// </summary>
        /// <param name="deltaTime">The time to add to the timer.</param>
        public abstract void Tick(float deltaTime);
    }
}