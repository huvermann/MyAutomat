﻿using System.Collections.ObjectModel;

namespace Colaautomat.Models
{
    public interface IMaschinenLog
    {
        ObservableCollection<string> LogEntries { get; set; }

        void AddLogEntry(string entry);
    }
}