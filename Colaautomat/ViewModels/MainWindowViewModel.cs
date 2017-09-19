using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colaautomat.Core.Models;
using Microsoft.Practices.ServiceLocation;

namespace Colaautomat.ViewModels
{
    public class MainWindowViewModel : SimulatorViewModel
    {
        public MainWindowViewModel(IServiceLocator serviceLocator, IAutomatInputManager inputManager, IGeldspeicherService geldspeicher, IProductStorageService productstorage, IMaschinenLog maschinenLog, IGeldausgabeSimulator geldausgabe, IWarenausgabeSimulator warenausgabe, IOrderService orderService) : base(serviceLocator, inputManager, geldspeicher, productstorage, maschinenLog, geldausgabe, warenausgabe, orderService)
        {
        }
    }
}
