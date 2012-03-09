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

        public IEnumerable<AccountHeaderViewModel> Accounts
        {
            get
            {
                return
                    from account in _share.Company.Accounts
                    orderby account.Type, account.Name.Value
                    select new AccountHeaderViewModel(account);
            }
        }

        public AccountHeaderViewModel SelectedAccount
        {
            get
            {
                return _navigationModel.SelectedAccount == null
                    ? null
                    : new AccountHeaderViewModel(_navigationModel.SelectedAccount);
            }
        }

        public void NewAccount(NewAccountModel newAccount)
        {
            _share.Company.NewAccount(newAccount.Name, (int)newAccount.AccountType);
        }

        public void DeleteAccount()
        {
            _navigationModel.SelectedAccount.Delete();
        }
    }
}
