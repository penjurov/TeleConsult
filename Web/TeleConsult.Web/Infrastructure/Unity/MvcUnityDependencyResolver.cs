﻿namespace TeleConsult.Web.Infrastructure.Unity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Async;

    using Microsoft.Practices.Unity;
    using Models;

    public class MvcUnityDependencyResolver : IDependencyResolver, IDisposable
    {
        public readonly IUnityContainer Unity;

        public MvcUnityDependencyResolver(IUnityContainer unity)
        {
            this.Unity = unity;
        }

        public TModel LoadModel<TModel>() where TModel : IModel
        {
            return this.Unity.Resolve<TModel>();
        }

        public TModel LoadModel<TModel, TData>() where TModel : IModel<TData>
        {
            return this.Unity.Resolve<TModel>();
        }

        public TModel BuildUp<TModel>(TModel obj)
        {
            return this.Unity.BuildUp<TModel>(obj);
        }

        public void Dispose()
        {
            this.Unity.Registrations.Where(r => r.LifetimeManager is IDisposable)
                .Select(r => r.LifetimeManager).OfType<IDisposable>().ToList().ForEach(m => m.Dispose());
        }

        public object GetService(Type serviceType)
        {
            // Disable resolving for native MVC interfaces
            if (serviceType == typeof(IControllerFactory))
            {
                return null;
            }

            if (serviceType == typeof(IControllerActivator))
            {
                return null;
            }

            if (serviceType == typeof(IViewPageActivator))
            {
                return null;
            }

            if (serviceType == typeof(IActionInvoker))
            {
                return null;
            }

            if (serviceType == typeof(IAsyncActionInvoker))
            {
                return null;
            }

            if (serviceType == typeof(ModelMetadataProvider))
            {
                return null;
            }

            if (serviceType == typeof(IFilterProvider))
            {
                return null;
            }

            try
            {
                return this.Unity.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to resolve type: {0}", serviceType.FullName));

                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.Unity.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }
    }
}