using UpdateControls.Fields;

namespace FacetedWorlds.Ledger.Models
{
    public class NewAccountModel
    {
        private Independent<string> _name = new Independent<string>();
        private Independent<AccountType> _accountType = new Independent<AccountType>();

        public string Name
        {
            get { return _name; }
            set { _name.Value = value; }
        }

        public AccountType AccountType
        {
            get { return _accountType; }
            set { _accountType.Value = value; }
        }
    }
}
