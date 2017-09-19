using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IWarenausgabeService
    {
        bool ProduktAusgabe(IProduct product);
    }
}