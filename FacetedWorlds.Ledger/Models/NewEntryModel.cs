using System;
using FacetedWorlds.Ledger.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.Ledger.Models
{
    public class NewEntryModel
    {
        private Independent<DateTime> _date = new Independent<DateTime>(DateTime.Today);
        private Independent<string> _id = new Independent<string>();
        private Independent<string> _description = new Independent<string>();
        private Independent<Account> _account = new Independent<Account>();
        private Independent<float> _increase = new Independent<float>();
        private Independent<float> _decrease = new Independent<float>();

        public DateTime Date
        {
            get { return _date; }
            set { _date.Value = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id.Value = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description.Value = value; }
        }

        public Account Account
        {
            get { return _account; }
            set { _account.Value = value; }
        }

        public float Increase
        {
            get { return _increase; }
            set { _increase.Value = value; }
        }

        public float Decrease
        {
            get { return _decrease; }
            set { _decrease.Value = value; }
        }

        public bool IsValid(Book book)
        {
            return _account.Value != null && _account.Value != book.Account &&
                _date.Value.Year == book.Year.CalendarYear &&
                ((_increase.Value > 0.0 && _decrease.Value == 0.0) ||
                 (_decrease.Value > 0.0 && _increase.Value == 0.0));
        }

        public void AddEntry(Book book)
        {
            AccountType accountType = (AccountType)book.Account.Type;
            if (_increase.Value > 0.0)
            {
                if (accountType == AccountType.Asset || accountType == AccountType.Expense)
                    book.AddDebit(_date, _id, _description, _account, _increase);
                else
                    book.AddCredit(_date, _id, _description, _account, _increase);
            }
            else
            {
                if (accountType == AccountType.Asset || accountType == AccountType.Expense)
                    book.AddCredit(_date, _id, _description, _account, _decrease);
                else
                    book.AddDebit(_date, _id, _description, _account, _decrease);
            }
        }

        public void Clear()
        {
            _id.Value = null;
            _description.Value = null;
            _account.Value = null;
            _increase.Value = 0.0f;
            _decrease.Value = 0.0f;
        }
    }
}
