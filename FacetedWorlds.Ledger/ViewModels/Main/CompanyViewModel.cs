using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.ViewModels.Main
{
    public class CompanyViewModel
    {
        private readonly Share _share;

        public CompanyViewModel(Share share)
        {
            _share = share;
        }

        public string Name
        {
            get { return _share.Company.Name; }
            set { _share.Company.Name = value; }
        }
    }
}
