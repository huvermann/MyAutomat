using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class MaschinenLogSimulator : BindableBase, IMaschinenLog
    {
        public MaschinenLogSimulator()
        {
            _logEntries = new ObservableCollection<string>();
        }

        private ObservableCollection<string> _logEntries;

        public ObservableCollection<string> LogEntries
        {
            get { return _logEntries; }
            set { SetProperty(ref _logEntries, value); }
        }

        public void AddLogEntry(string entry)
        {
            LogEntries.Add(entry);
        }
    }
}
