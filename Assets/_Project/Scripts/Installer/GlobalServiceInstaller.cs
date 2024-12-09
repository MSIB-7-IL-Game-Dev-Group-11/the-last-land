using System.Collections.Generic;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.Installer.Common;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Installer
{
    public class GlobalServiceInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private List<Object> services;

        public void InstallServices()
        {
            foreach (var service in services)
            {
                ServiceLocator.Global.RegisterServiceIfNotExists(service);
            }
        }

        private void Awake()
        {
            InstallServices();
        }
    }
}