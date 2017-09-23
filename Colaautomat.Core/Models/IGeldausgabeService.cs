using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IGeldausgabeService
    {
        Task<bool> GeldRueckgabe(IGeldspeicherService geldspeicher);
    }
}