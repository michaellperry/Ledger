using System;
using System.Text.RegularExpressions;
using FacetedWorlds.Ledger.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;

namespace FacetedWorlds.Ledger
{
    public class SynchronizationService
    {
        private const string ThisIdentity = "FacetedWorlds.Ledger.Model.Identity.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Identity _identity;

        public void Initialize()
        {
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .Register<CorrespondenceModel>()
                ;

            _identity = _community.LoadFact<Identity>(ThisIdentity);
            if (_identity == null)
            {
                string randomId = Punctuation.Replace(Guid.NewGuid().ToString(), String.Empty).ToLower();
                _identity = _community.AddFact(new Identity(randomId));
                _community.SetFact(ThisIdentity, _identity);
            }
        }

        public Community Community
        {
            get { return _community; }
        }

        public Identity Identity
        {
            get { return _identity; }
        }
    }
}
