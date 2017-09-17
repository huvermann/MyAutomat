using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IWarenausgabeModel
    {
        bool ProduktAusgabe(IProduct product, IMaschinenLog log);
    }
}