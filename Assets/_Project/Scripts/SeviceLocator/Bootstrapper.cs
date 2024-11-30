using TheLastLand._Project.Scripts.Extensions;
using UnityEngine;

namespace TheLastLand._Project.Scripts.SeviceLocator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        private ServiceLocator _container;
        internal ServiceLocator Container =>
            _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());

        private bool _hasBeenBootstrapped;

        private void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }
}