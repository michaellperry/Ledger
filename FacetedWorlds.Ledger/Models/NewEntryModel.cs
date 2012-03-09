using System;
using FacetedWorlds.Ledger.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.Ledger.Models
{
    public class NewEntryModel
    {
        private Independent<DateTime> _date = new Independent<DateTime>();
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
    }
}
