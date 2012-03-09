using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;
using FacetedWorlds.Ledger.Models;

namespace FacetedWorlds.Ledger.ViewModels
{
    public class BookViewModel
    {
        private readonly Book _book;

        private NewEntryModel _newEntry = new NewEntryModel();

        public BookViewModel(Book book)
        {
            _book = book;
        }

        public IEnumerable<EntryViewModel> Entries
        {
            get
            {
                return
                    from entry in _book.Credits.Union(_book.Debits)
                    orderby entry.EntryDate, entry.Created
                    select new EntryViewModel(entry, _book);
            }
        }

        public DateTime Date
        {
            get { return _newEntry.Date; }
            set { _newEntry.Date = value; }
        }

        public string Id
        {
            get { return _newEntry.Id; }
            set { _newEntry.Id = value; }
        }

        public string Description
        {
            get { return _newEntry.Description; }
            set { _newEntry.Description = value; }
        }

        public string AccountName
        {
            get { return _newEntry.Account == null ? null : _newEntry.Account.Name.Value; }
            set
            {
            	_newEntry.Account = _book.Account.Company.Accounts
                    .FirstOrDefault(account => account.Name.Value == value && account != _book.Account);
            }
        }

        public IEnumerable<string> AccountNameOptions
        {
            get
            {
                return
                    from account in _book.Account.Company.Accounts
                    where account != _book.Account
                    orderby account.Name.Value
                    select account.Name.Value;
            }
        }

        public AccountType? AccountType
        {
            get
            {
                if (_newEntry.Account == null)
                    return null;
                else
                    return (AccountType)_newEntry.Account.Type;
            }
        }

        public float Increase
        {
            get { return _newEntry.Increase; }
            set { _newEntry.Increase = value; }
        }

        public float Decrease
        {
            get { return _newEntry.Decrease; }
            set { _newEntry.Decrease = value; }
        }
    }
}
