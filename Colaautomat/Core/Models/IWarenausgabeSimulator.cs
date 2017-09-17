using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IWarenausgabeSimulator
    {
        ObservableCollection<string> WarenausgabeFach { get; set; }
    }
}