using System;
using UpdateControls.Fields;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.Models
{
    public class NavigationModel
    {
        private Independent<Share> _selectedShare = new Independent<Share>();
        private Independent<int> _selectedYear = new Independent<int>(DateTime.Today.Year);

        public Share SelectedShare
        {
            get { return _selectedShare; }
            set { _selectedShare.Value = value; }
        }

        public int SelectedYear
        {
            get { return _selectedYear; }
            set { _selectedYear.Value = value; }
        }
    }
}
