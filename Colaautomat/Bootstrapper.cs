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
            Container.RegisterType<IWarenausgabeModel, WarenausgabeSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeldausgabeModel, GeldausgabeSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeldspeicherModel, GeldspeicherService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IProductStorage, ProductStorageSimulator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOrderService, OrderService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAutomatInputManager, AutomatInputManager>(new ContainerControlledLifetimeManager());
            //Container.RegisterInstance<JoystickInputExtension>(Container.Resolve<JoystickInputExtension>());
            base.ConfigureContainer();

        } 
    }
}