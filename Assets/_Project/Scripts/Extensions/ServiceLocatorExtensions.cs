using System.IO;
using System.Runtime.CompilerServices;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.Utils;

namespace TheLastLand._Project.Scripts.Extensions
{
    public static class ServiceLocatorExtensions
    {
        /// <summary>
        /// Extension method for registering a service in the ServiceLocator if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The type of the service to register.</typeparam>
        /// <param name="serviceLocator">The ServiceLocator instance.</param>
        /// <param name="service">The service instance to register.</param>
        /// <returns>The ServiceLocator instance.</returns>
        public static ServiceLocator RegisterServiceIfNotExists<T>(
            this ServiceLocator serviceLocator, T service) where T : class
        {
            if (!serviceLocator.TryGet(out T _))
            {
                serviceLocator.Register(service);
            }

            return serviceLocator;
        }
        
        public static ServiceLocator TryGetWithStatus<T>(
            this ServiceLocator serviceLocator, 
            out T service, 
            [CallerFilePath] string callerName = "") where T : class
        {
            bool success = serviceLocator.TryGet(out service);
            ServiceInfo.LogServiceRetrieval(GetClassName(callerName), typeof(T).Name, success);
            return serviceLocator;
        }   

        public static T GetWithStatus<T>(
            this ServiceLocator serviceLocator, 
            [CallerFilePath] string callerName = "") where T : class
        {
            var service = serviceLocator.Get<T>();
            bool success = service != null;
            ServiceInfo.LogServiceRetrieval(GetClassName(callerName), typeof(T).Name, success);
            return service;
        }
        
        private static string GetClassName(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }
    }
}