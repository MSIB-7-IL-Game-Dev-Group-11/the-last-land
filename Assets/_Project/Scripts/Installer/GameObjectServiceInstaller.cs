using System.Collections.Generic;
using TheLastLand._Project.Scripts.Installer.Common;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Installer
{
    public class GameObjectServiceInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private List<Object> services;

        public void InstallServices()
        {
            foreach (var service in services)
            {
                ServiceLocator.For(this).Register(service);
            }
        }

        private void Awake()
        {
            InstallServices();
        }
    }
}