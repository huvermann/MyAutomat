using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IGeldausgabeService
    {
        void GeldRueckgabe(IGeldspeicherService geldspeicher, IMaschinenLog log);
    }
}