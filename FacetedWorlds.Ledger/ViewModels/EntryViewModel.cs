using System;
using FacetedWorlds.Ledger.Model;
using FacetedWorlds.Ledger.Models;
using System.Windows.Input;
using UpdateControls.XAML;

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

        public string Date
        {
            get { return _entry.EntryDate.ToString("M/d"); }
        }

        public string Id
        {
            get { return _entry.Id; }
        }

        public string Description
        {
            get { return _entry.Description; }
        }

        public AccountHeaderViewModel OtherAccount
        {
            get { return new AccountHeaderViewModel(_entry.Credit == _book ? _entry.Debit.Account : _entry.Credit.Account); }
        }

        public string Increase
        {
            get { return IsIncrease ? _entry.Amount.ToString("0.00") : String.Empty; }
        }

        public string Decrease
        {
            get { return !IsIncrease ? _entry.Amount.ToString("0.00") : String.Empty; }
        }

        public bool IsIncrease
        {
            get
            {
                AccountType accountType = (AccountType)_book.Account.Type;
                if (accountType == AccountType.Asset || accountType == AccountType.Expense)
                    return _entry.Debit == _book;
                else
                    return _entry.Credit == _book;
            }
        }

        public ICommand Void
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _entry.Void();
                    });
            }
        }
    }
}
