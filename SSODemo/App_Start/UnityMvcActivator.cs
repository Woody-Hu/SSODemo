using System.Linq;
using System.Web.Mvc;

using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SSODemo.UnityMvcActivator), nameof(SSODemo.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SSODemo.UnityMvcActivator), nameof(SSODemo.UnityMvcActivator.Shutdown))]

namespace SSODemo
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            //FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            //FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            var useUnityApp = UnityUtility.UnityApplication.GetApplication();

            //×¢²áUnity
            DependencyResolver.SetResolver(new UnityDependencyResolver(useUnityApp.CoreUnityContainer));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            var useUnityApp = UnityUtility.UnityApplication.GetApplication();
            useUnityApp.CoreUnityContainer.Dispose();
            //UnityConfig.Container.Dispose();
        }
    }
}