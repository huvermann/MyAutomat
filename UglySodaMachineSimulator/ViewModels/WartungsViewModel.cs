using Colaautomat.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UglySodaMachineSimulator.ViewModels
{
    public class WartungsViewModel : BindableBase
    {
        private IProductStorageSimulator _productStorage;
        private IRegionManager _regionManager;

        public WartungsViewModel(IRegionManager regionManager, IProductStorageSimulator productStorage)
        {
            _regionManager = regionManager;
            _productStorage = productStorage;
            GoBackCommand = new DelegateCommand(NavigateToAutomatenView);
        }

        private void NavigateToAutomatenView()
        {
            _regionManager.RequestNavigate("Shell", "AutomatenView");
        }

        public IProductStorageSimulator Storage
        {
            get { return _productStorage; }
            set { SetProperty(ref _productStorage, value); }
        }

        public ICommand GoBackCommand { get; set; }
    }
}
