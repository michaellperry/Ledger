
namespace FacetedWorlds.Ledger.Model
{
    public partial class Company
    {
        public void NewAccount(string name, int accountType)
        {
            Account account = Community.AddFact(new Account(this, accountType));
            account.Name = name;
        }
    }
}
