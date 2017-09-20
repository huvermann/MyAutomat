using Colaautomat.Core.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using UglySodaMachineSimulator.Views;

namespace UglySodaMachineSimulator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _navigationService;


        public MainWindowViewModel(IRegionManager navigationService)
        {
            _navigationService = navigationService;
            _navigationService.RegisterViewWithRegion("Shell", typeof(AutomatenView));
        }
        
    }
}
