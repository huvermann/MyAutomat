using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IMaschinenLog
    {
        ObservableCollection<string> LogEntries { get; set; }

        void AddLogEntry(string entry);
    }
}