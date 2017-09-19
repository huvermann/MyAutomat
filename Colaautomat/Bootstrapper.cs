using Colaautomat.Core.Models;
using Colaautomat.JoystickExtension;
using Colaautomat.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using System.Windows;

namespace Colaautomat
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
            RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
            Container.RegisterType<IMaschinenLog, MaschinenLogSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IWarenausgabeService, WarenausgabeSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeldausgabeService, GeldausgabeHWSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeldspeicherService, GeldspeicherService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IProductStorageService, ProductStorageSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOrderService, OrderService>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IGeldausgabeSimulator>((GeldausgabeHWSimulator)Container.Resolve<IGeldausgabeService>());
            Container.RegisterInstance<IWarenausgabeSimulator>((WarenausgabeSimulator)Container.Resolve<IWarenausgabeService>());
            Container.RegisterType<IAutomatInputManager, AutomatInputManager>(new ContainerControlledLifetimeManager());
            //Container.RegisterInstance<JoystickInputExtension>(Container.Resolve<JoystickInputExtension>());
            base.ConfigureContainer();

        } 
    }
}