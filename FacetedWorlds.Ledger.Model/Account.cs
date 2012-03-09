
namespace FacetedWorlds.Ledger.Model
{
    public partial class Account
    {
        public void Delete()
        {
            Community.AddFact(new AccountDelete(this));
        }
    }
}
