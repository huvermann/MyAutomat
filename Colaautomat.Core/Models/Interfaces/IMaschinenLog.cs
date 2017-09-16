using System.Collections.ObjectModel;

namespace Colaautomat.Model
{
    public interface IMaschinenLog
    {
        ObservableCollection<string> LogEntries { get; set; }

        void AddLogEntry(string entry);
    }
}