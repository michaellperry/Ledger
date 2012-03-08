using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;
using FacetedWorlds.Ledger.Models;

namespace FacetedWorlds.Ledger.ViewModels.Main
{
    public class CompanyViewModel
    {
        private readonly Share _share;
        private readonly NavigationModel _navigationModel;

        public CompanyViewModel(Share share, NavigationModel navigationModel)
        {
            _share = share;
            _navigationModel = navigationModel;
        }

        public string Name
        {
            get { return _share.Company.Name; }
            set { _share.Company.Name = value; }
        }

        public int Year
        {
            get { return _navigationModel.SelectedYear; }
            set { _navigationModel.SelectedYear = value; }
        }
    }
}
