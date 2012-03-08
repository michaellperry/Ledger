using System;
using UpdateControls.Fields;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.Models
{
    public class NavigationModel
    {
        private Independent<Share> _selectedShare = new Independent<Share>();

        public Share SelectedShare
        {
            get { return _selectedShare; }
            set { _selectedShare.Value = value; }
        }
    }
}
