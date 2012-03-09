using FacetedWorlds.Ledger.Model;
using FacetedWorlds.Ledger.Models;

namespace FacetedWorlds.Ledger.ViewModels.Main
{
    public class AccountHeaderViewModel
    {
        private readonly Account _account;

        public AccountHeaderViewModel(Account account)
        {
            _account = account;
        }

        internal Account Account
        {
            get { return _account; }
        }

        public string Name
        {
            get { return _account.Name.Value ?? "<New Account>"; }
        }

        public AccountType AccountType
        {
            get { return (AccountType)_account.Type; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            AccountHeaderViewModel that = obj as AccountHeaderViewModel;
            if (that == null)
                return false;
            return _account.Equals(that._account);
        }

        public override int GetHashCode()
        {
            return _account.GetHashCode();
        }
    }
}
