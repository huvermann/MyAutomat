using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IGeldausgabeSimulator
    {
        ObservableCollection<string> GeldausgabeInfo { get; set; }
    }
}