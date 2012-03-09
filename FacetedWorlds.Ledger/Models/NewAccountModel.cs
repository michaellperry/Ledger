using UpdateControls.Fields;

namespace FacetedWorlds.Ledger.Models
{
    public class NewAccountModel
    {
        private Independent<string> _name = new Independent<string>();
        private Independent<AccountType> _type = new Independent<AccountType>();

        public string Name
        {
            get { return _name; }
            set { _name.Value = value; }
        }

        public AccountType Type
        {
            get { return _type; }
            set { _type.Value = value; }
        }
    }
}
