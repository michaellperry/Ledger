using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.Ledger.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.Ledger.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            var navigationModel = new NavigationModel();
            var newEntry = new NewEntryModel();
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
            {
                _synchronizationService.Initialize();
                navigationModel.SelectedShare = _synchronizationService.Identity.ActiveShares
                    .OrderBy(share => share.Company.Name.Value)
                    .FirstOrDefault();
                _main = new MainViewModel(_synchronizationService.Community, navigationModel, newEntry, _synchronizationService);
            }
        }

        public object Main
        {
            get { return _main == null ? null : ForView.Wrap(_main); }
        }
    }
}
