﻿using System.Collections.ObjectModel;

namespace Colaautomat.Models
{
    public interface IGeldausgabeModel
    {
        ObservableCollection<string> GeldausgabeInfo { get; set; }

        void GeldRueckgabe(IGeldspeicherModel geldspeicher, IMaschinenLog log);
    }
}