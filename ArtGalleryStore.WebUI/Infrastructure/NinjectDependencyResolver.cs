using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Concrete;
using System.Configuration;
using ArtGalleryStore.WebUI.Infrastructure.Abstract;
using ArtGalleryStore.WebUI.Infrastructure.Concrete;


namespace ArtGalleryStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IArtGalleryRepository>().To<EFArtGalleryRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();

        }
    }
}