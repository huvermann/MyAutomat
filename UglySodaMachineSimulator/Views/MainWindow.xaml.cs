using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Colaautomat.ViewModels;
using Prism.Events;
using Colaautomat.Core.Messages;

namespace Colaautomat.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEventAggregator _eventAggregator;

        public MainWindow(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            _eventAggregator.GetEvent<CloseApplicationMessage>().Publish();
            base.OnClosed(e);
        }
    }
}
