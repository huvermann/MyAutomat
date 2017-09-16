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
            Container.RegisterInstance<IMaschinenLog>(new MaschinenLog());
            Container.RegisterInstance<IWarenausgabeModel>(new WarenausgabeModel());
            Container.RegisterInstance<IGeldausgabeModel>(new GeldausgabeModel());
            Container.RegisterInstance<IGeldspeicherModel>(new GeldspeicherModel());
            Container.RegisterInstance<IProductStorage>(new ProductStorage());
            Container.RegisterInstance<IOrderService>(new OrderService());
            Container.RegisterType<IAutomatInputManager, AutomatInputManager>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<JoystickInputExtension>(Container.Resolve<JoystickInputExtension>());
            base.ConfigureContainer();

        } 
    }
}