using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IGeldausgabeModel
    {
        void GeldRueckgabe(IGeldspeicherModel geldspeicher, IMaschinenLog log);
    }
}