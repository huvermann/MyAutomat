using System.Collections.ObjectModel;

namespace Colaautomat.Model
{
    public interface IWarenausgabeModel
    {
        ObservableCollection<string> WarenausgabeFach { get; set; }

        bool ProduktAusgabe(Product product, IMaschinenLog log);
    }
}