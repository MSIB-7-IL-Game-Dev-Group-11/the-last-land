using UnityEngine;

namespace TheLastLand._Project.Scripts.Utils
{
    public static class ServiceInfo
    {
        public static void LogServiceRetrieval(string className, string serviceName, bool success)
        {
            Debug.Log(
                success
                    ? $"{className} successfully retrieved {serviceName} service."
                    : $"{className} failed to retrieve {serviceName} service."
            );
        }
    }
}