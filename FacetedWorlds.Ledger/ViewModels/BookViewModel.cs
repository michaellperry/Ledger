using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;
using FacetedWorlds.Ledger.Models;
using UpdateControls.Collections;

namespace FacetedWorlds.Ledger.ViewModels
{
    public class BookViewModel
    {
        private readonly Book _book;
        private readonly NewEntryModel _newEntry;

        private DependentList<AccountHeaderViewModel> _otherAccountOptions;

        public BookViewModel(Book book, NewEntryModel newEntry)
        {
            _book = book;
            _newEntry = newEntry;

            _otherAccountOptions = new DependentList<AccountHeaderViewModel>(() =>
                from account in _book.Account.Company.Accounts
                orderby account.Name.Value
                select new AccountHeaderViewModel(account)
            );
        }

        public string Name
        {
            get { return _book.Account.Name.Value; }
        }

        public AccountType Type
        {
            get { return (AccountType)_book.Account.Type; }
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

        public string Date
        {
            get { return _newEntry.Date.ToString("M/d"); }
            set { _newEntry.Date = DateTime.Parse(String.Format("{0}/{1}", value, _book.Year.CalendarYear)); }
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

        public AccountHeaderViewModel OtherAccount
        {
            get { return _otherAccountOptions.FirstOrDefault(vm => vm.Account == _newEntry.Account); }
            set { _newEntry.Account = value == null ? null : value.Account; }
        }

        public string OtherAccountName
        {
            get { return _newEntry.Account == null ? null : _newEntry.Account.Name.Value; }
            set { _newEntry.Account = _book.Account.Company.Accounts.FirstOrDefault(account => account.Name.Value == value); }
        }

        public IEnumerable<AccountHeaderViewModel> OtherAccountOptions
        {
            get { return _otherAccountOptions; }
        }

        public string Increase
        {
            get { return _newEntry.Increase == 0.0 ? String.Empty : _newEntry.Increase.ToString("0.00"); }
            set { _newEntry.Increase = SafeParse(value); }
        }

        public string Decrease
        {
            get { return _newEntry.Decrease == 0.0 ? String.Empty : _newEntry.Decrease.ToString("0.00"); }
            set { _newEntry.Decrease = SafeParse(value); }
        }

        private static float SafeParse(string str)
        {
            float value;
            if (!float.TryParse(str, out value))
                value = 0.0f;
            return value;
        }

        public float Balance
        {
            get
            {
                float netCredits = _book.Credits.Sum(credit => credit.Amount) - _book.Debits.Sum(debit => debit.Amount);
                AccountType accountType = (AccountType)_book.Account.Type;
                return accountType == AccountType.Income || accountType == AccountType.Liability ? netCredits : -netCredits;
            }
        }

        public bool EnterRow()
        {
            if (_newEntry.IsValid(_book))
            {
                _newEntry.AddEntry(_book);
                _newEntry.Clear();
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            BookViewModel that = obj as BookViewModel;
            if (that == null)
                return false;
            return _book.Equals(that._book);
        }

        public override int GetHashCode()
        {
            return _book.GetHashCode();
        }
    }
}
