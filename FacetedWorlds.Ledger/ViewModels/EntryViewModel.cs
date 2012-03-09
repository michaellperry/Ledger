using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.ViewModels
{
    public class EntryViewModel
    {
        private readonly Entry _entry;
        private readonly Book _book;
        
        public EntryViewModel(Entry entry, Book book)
        {
            _entry = entry;
            _book = book;
        }
    }
}
