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

        private readonly Main.MainViewModel _main;

        public ViewModelLocator()
        {
            NavigationModel navigationModel = new NavigationModel();
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
            {
                _synchronizationService.Initialize();
                navigationModel.SelectedShare = _synchronizationService.Identity.ActiveShares
                    .OrderBy(share => share.Company.Name.Value)
                    .FirstOrDefault();
                _main = new Main.MainViewModel(_synchronizationService.Community, navigationModel, _synchronizationService);
            }
        }

        public object Main
        {
            get { return _main == null ? null : ForView.Wrap(_main); }
        }
    }
}
