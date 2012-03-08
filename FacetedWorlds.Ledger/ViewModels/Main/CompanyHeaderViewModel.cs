using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.ViewModels.Main
{
    public class CompanyHeaderViewModel
    {
        private readonly Share _share;

        public CompanyHeaderViewModel(Share share)
        {
            _share = share;
        }

        internal Share Share
        {
            get { return _share; }
        }

        public string Name
        {
            get { return _share.Company.Name.Value ?? "<New Company>"; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            CompanyHeaderViewModel that = obj as CompanyHeaderViewModel;
            if (that == null)
                return false;
            return this._share.Equals(that._share);
        }

        public override int GetHashCode()
        {
            return _share.GetHashCode();
        }
    }
}
