using System;

namespace FacetedWorlds.Ledger.Model
{
    public partial class Entry
    {
        public void Void()
        {
            Community.AddFact(new EntryVoid(this, DateTime.Now, 0));
        }
    }
}
