
namespace FacetedWorlds.Ledger.Model
{
    public partial class Share
    {
        public void Revoke()
        {
            Community.AddFact(new ShareRevoke(this));
        }
    }
}
