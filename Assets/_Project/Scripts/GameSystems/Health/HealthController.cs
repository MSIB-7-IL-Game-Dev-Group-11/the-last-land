using TheLastLand._Project.Scripts.GameSystems.Health.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Health
{
    public class HealthController : IHealthController
    {
        private readonly HealthData _data;
        private float _health;

        public HealthController(HealthData data)
        {
            _data = data;
            _health = _data.Max;
        }

        public float Health
        {
            get => _health;
            private set => _health = Mathf.Clamp(value, _data.Min, _data.Max);
        }

        public void TakeDamage(float value)
        {
            Health -= value;
        }

        public void RegenerateHealth(float value)
        {
            if (!(Health < _data.Max)) return;
            Health += _data.RegenerationRate;
            Health = Mathf.Clamp(Health, _data.Min, _data.Max);
        }
    }
}