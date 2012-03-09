using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.Ledger.Models;
using UpdateControls.Correspondence;
using UpdateControls.XAML;
using FacetedWorlds.Ledger.Model;

namespace FacetedWorlds.Ledger.ViewModels
{
    public class MainViewModel
    {
        private Community _community;
        private NavigationModel _navigationModel;
        private NewEntryModel _newEntry;
        private SynchronizationService _synhronizationService;

        public MainViewModel(Community community, NavigationModel navigationModel, NewEntryModel newEntry, SynchronizationService synhronizationService)
        {
            _community = community;
            _navigationModel = navigationModel;
            _newEntry = newEntry;
            _synhronizationService = synhronizationService;
        }

        public IEnumerable<CompanyHeaderViewModel> Companies
        {
            get
            {
                return
                    from share in _synhronizationService.Identity.ActiveShares
                    orderby share.Company.Name.Value
                    select new CompanyHeaderViewModel(share);
            }
        }

        public CompanyHeaderViewModel SelectedCompany
        {
            get
            {
                return _navigationModel.SelectedShare == null
                    ? null
                    : new CompanyHeaderViewModel(_navigationModel.SelectedShare);
            }
            set
            {
            	_navigationModel.SelectedShare = value == null
                    ? null
                    : value.Share;
            }
        }

        public CompanyViewModel CompanyDetail
        {
            get
            {
                return _navigationModel.SelectedShare == null
                    ? null
                    : new CompanyViewModel(_navigationModel.SelectedShare, _navigationModel, _newEntry);
            }
        }

        public ICommand NewCompany
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        Company company = _community.AddFact(new Company());
                        Share share = _community.AddFact(new Share(_synhronizationService.Identity, company));
                        _navigationModel.SelectedShare = share;
                    });
            }
        }

        public ICommand DeleteCompany
        {
            get
            {
                return MakeCommand
                    .When(() => _navigationModel.SelectedShare != null)
                    .Do(delegate
                    {
                        _navigationModel.SelectedShare.Revoke();
                        _navigationModel.SelectedShare = null;
                    });
            }
        }
    }
}
