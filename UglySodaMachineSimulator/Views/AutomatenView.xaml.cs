using Colaautomat.Core.Messages;
using Prism.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Colaautomat.Core.Models;

namespace UglySodaMachineSimulator.Views
{
    /// <summary>
    /// Interaction logic for AutomatenView.xaml
    /// </summary>
    public partial class AutomatenView : UserControl
    {
        public AutomatenView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<ProductDeliveredMessage>().Subscribe(ProductsChanged);
        }

        private void ProductsChanged(IProduct obj)
        {
            Warenbestandsliste.Items.Refresh();
        }
    }
}
