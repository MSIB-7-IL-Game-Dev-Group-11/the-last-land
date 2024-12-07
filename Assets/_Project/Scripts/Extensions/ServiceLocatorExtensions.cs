using TheLastLand._Project.Scripts.SeviceLocator;

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
    }
}