using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Correspondence.Tasks;
using System;
using System.IO;
using System.Threading;

/**
/ For use with http://graphviz.org/
digraph "FacetedWorlds.Ledger.Model"
{
    rankdir=BT
    Share -> Identity [color="red"]
    Share -> Company
    ShareRevoke -> Share
    Company__name -> Company [color="red"]
    Company__name -> Company__name [label="  *"]
    Year -> Company [color="red"]
    Account -> Company [color="red"]
    Account__name -> Account
    Account__name -> Account__name [label="  *"]
    AccountDelete -> Account
    Book -> Account
    Book -> Year
    Entry -> Book
    Entry -> Book
    Entry__id -> Entry
    Entry__id -> Entry__id [label="  *"]
    Entry__description -> Entry
    Entry__description -> Entry__description [label="  *"]
    EntryVoid -> Entry
}
**/

namespace FacetedWorlds.Ledger.Model
{
    public partial class Identity : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Identity newFact = new Identity(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Identity fact = (Identity)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Identity.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Identity.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Identity", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Identity GetUnloadedInstance()
        {
            return new Identity((FactMemento)null) { IsLoaded = false };
        }

        public static Identity GetNullInstance()
        {
            return new Identity((FactMemento)null) { IsNull = true };
        }

        public Identity Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Identity fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Identity)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries
        private static Query _cacheQueryActiveShares;

        public static Query GetQueryActiveShares()
		{
            if (_cacheQueryActiveShares == null)
            {
			    _cacheQueryActiveShares = new Query()
    				.JoinSuccessors(Share.GetRoleIdentity(), Condition.WhereIsEmpty(Share.GetQueryIsActive())
				)
                ;
            }
            return _cacheQueryActiveShares;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Share> _activeShares;

        // Business constructor
        public Identity(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Identity(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _activeShares = new Result<Share>(this, GetQueryActiveShares(), Share.GetUnloadedInstance, Share.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Share> ActiveShares
        {
            get { return _activeShares; }
        }

        // Mutable property access

    }
    
    public partial class Share : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Share newFact = new Share(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Share fact = (Share)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Share.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Share.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Share", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Share GetUnloadedInstance()
        {
            return new Share((FactMemento)null) { IsLoaded = false };
        }

        public static Share GetNullInstance()
        {
            return new Share((FactMemento)null) { IsNull = true };
        }

        public Share Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Share fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Share)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleIdentity;
        public static Role GetRoleIdentity()
        {
            if (_cacheRoleIdentity == null)
            {
                _cacheRoleIdentity = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "identity",
			        Identity._correspondenceFactType,
			        true));
            }
            return _cacheRoleIdentity;
        }
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        false));
            }
            return _cacheRoleCompany;
        }

        // Queries
        private static Query _cacheQueryIsActive;

        public static Query GetQueryIsActive()
		{
            if (_cacheQueryIsActive == null)
            {
			    _cacheQueryIsActive = new Query()
		    		.JoinSuccessors(ShareRevoke.GetRoleShare())
                ;
            }
            return _cacheQueryIsActive;
		}

        // Predicates
        public static Condition IsActive = Condition.WhereIsEmpty(GetQueryIsActive());

        // Predecessors
        private PredecessorObj<Identity> _identity;
        private PredecessorObj<Company> _company;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public Share(
            Identity identity
            ,Company company
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, GetRoleIdentity(), identity);
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
        }

        // Hydration constructor
        private Share(FactMemento memento)
        {
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, GetRoleIdentity(), memento, Identity.GetUnloadedInstance, Identity.GetNullInstance);
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Identity Identity
        {
            get { return IsNull ? Identity.GetNullInstance() : _identity.Fact; }
        }
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class ShareRevoke : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ShareRevoke newFact = new ShareRevoke(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ShareRevoke fact = (ShareRevoke)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ShareRevoke.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ShareRevoke.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.ShareRevoke", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ShareRevoke GetUnloadedInstance()
        {
            return new ShareRevoke((FactMemento)null) { IsLoaded = false };
        }

        public static ShareRevoke GetNullInstance()
        {
            return new ShareRevoke((FactMemento)null) { IsNull = true };
        }

        public ShareRevoke Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ShareRevoke fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ShareRevoke)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleShare;
        public static Role GetRoleShare()
        {
            if (_cacheRoleShare == null)
            {
                _cacheRoleShare = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "share",
			        Share._correspondenceFactType,
			        false));
            }
            return _cacheRoleShare;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Share> _share;

        // Fields

        // Results

        // Business constructor
        public ShareRevoke(
            Share share
            )
        {
            InitializeResults();
            _share = new PredecessorObj<Share>(this, GetRoleShare(), share);
        }

        // Hydration constructor
        private ShareRevoke(FactMemento memento)
        {
            InitializeResults();
            _share = new PredecessorObj<Share>(this, GetRoleShare(), memento, Share.GetUnloadedInstance, Share.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Share Share
        {
            get { return IsNull ? Share.GetNullInstance() : _share.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Company : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Company newFact = new Company(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Company fact = (Company)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Company.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Company.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Company", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Company GetUnloadedInstance()
        {
            return new Company((FactMemento)null) { IsLoaded = false };
        }

        public static Company GetNullInstance()
        {
            return new Company((FactMemento)null) { IsNull = true };
        }

        public Company Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Company fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Company)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Company__name.GetRoleCompany(), Condition.WhereIsEmpty(Company__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}
        private static Query _cacheQueryAccounts;

        public static Query GetQueryAccounts()
		{
            if (_cacheQueryAccounts == null)
            {
			    _cacheQueryAccounts = new Query()
    				.JoinSuccessors(Account.GetRoleCompany(), Condition.WhereIsEmpty(Account.GetQueryIsDeleted())
				)
                ;
            }
            return _cacheQueryAccounts;
		}

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Company__name> _name;
        private Result<Account> _accounts;

        // Business constructor
        public Company(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Company(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Company__name>(this, GetQueryName(), Company__name.GetUnloadedInstance, Company__name.GetNullInstance);
            _accounts = new Result<Account>(this, GetQueryAccounts(), Account.GetUnloadedInstance, Account.GetNullInstance);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Account> Accounts
        {
            get { return _accounts; }
        }

        // Mutable property access
        public TransientDisputable<Company__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Company__name(this, _name, value.Value));
				}
			}
        }

    }
    
    public partial class Company__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Company__name newFact = new Company__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Company__name fact = (Company__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Company__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Company__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Company__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Company__name GetUnloadedInstance()
        {
            return new Company__name((FactMemento)null) { IsLoaded = false };
        }

        public static Company__name GetNullInstance()
        {
            return new Company__name((FactMemento)null) { IsNull = true };
        }

        public Company__name Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Company__name fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Company__name)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        true));
            }
            return _cacheRoleCompany;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Company__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Company__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Company> _company;
        private PredecessorList<Company__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Company__name(
            Company company
            ,IEnumerable<Company__name> prior
            ,string value
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
            _prior = new PredecessorList<Company__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Company__name(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
            _prior = new PredecessorList<Company__name>(this, GetRolePrior(), memento, Company__name.GetUnloadedInstance, Company__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }
        public PredecessorList<Company__name> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Year : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Year newFact = new Year(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._calendarYear = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Year fact = (Year)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._calendarYear);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Year.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Year.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Year", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Year GetUnloadedInstance()
        {
            return new Year((FactMemento)null) { IsLoaded = false };
        }

        public static Year GetNullInstance()
        {
            return new Year((FactMemento)null) { IsNull = true };
        }

        public Year Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Year fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Year)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        true));
            }
            return _cacheRoleCompany;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        private int _calendarYear;

        // Results

        // Business constructor
        public Year(
            Company company
            ,int calendarYear
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
            _calendarYear = calendarYear;
        }

        // Hydration constructor
        private Year(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }

        // Field access
        public int CalendarYear
        {
            get { return _calendarYear; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Account : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Account newFact = new Account(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
						newFact._type = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Account fact = (Account)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._type);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Account.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Account.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Account", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Account GetUnloadedInstance()
        {
            return new Account((FactMemento)null) { IsLoaded = false };
        }

        public static Account GetNullInstance()
        {
            return new Account((FactMemento)null) { IsNull = true };
        }

        public Account Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Account fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Account)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        true));
            }
            return _cacheRoleCompany;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Account__name.GetRoleAccount(), Condition.WhereIsEmpty(Account__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}
        private static Query _cacheQueryIsDeleted;

        public static Query GetQueryIsDeleted()
		{
            if (_cacheQueryIsDeleted == null)
            {
			    _cacheQueryIsDeleted = new Query()
		    		.JoinSuccessors(AccountDelete.GetRoleAccount())
                ;
            }
            return _cacheQueryIsDeleted;
		}

        // Predicates
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(GetQueryIsDeleted());

        // Predecessors
        private PredecessorObj<Company> _company;

        // Unique
        private Guid _unique;

        // Fields
        private int _type;

        // Results
        private Result<Account__name> _name;

        // Business constructor
        public Account(
            Company company
            ,int type
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
            _type = type;
        }

        // Hydration constructor
        private Account(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Account__name>(this, GetQueryName(), Account__name.GetUnloadedInstance, Account__name.GetNullInstance);
        }

        // Predecessor access
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }

        public int Type
        {
            get { return _type; }
        }

        // Query result access

        // Mutable property access
        public TransientDisputable<Account__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Account__name(this, _name, value.Value));
				}
			}
        }

    }
    
    public partial class Account__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Account__name newFact = new Account__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Account__name fact = (Account__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Account__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Account__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Account__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Account__name GetUnloadedInstance()
        {
            return new Account__name((FactMemento)null) { IsLoaded = false };
        }

        public static Account__name GetNullInstance()
        {
            return new Account__name((FactMemento)null) { IsNull = true };
        }

        public Account__name Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Account__name fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Account__name)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleAccount;
        public static Role GetRoleAccount()
        {
            if (_cacheRoleAccount == null)
            {
                _cacheRoleAccount = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "account",
			        Account._correspondenceFactType,
			        false));
            }
            return _cacheRoleAccount;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Account__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Account__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Account> _account;
        private PredecessorList<Account__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Account__name(
            Account account
            ,IEnumerable<Account__name> prior
            ,string value
            )
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), account);
            _prior = new PredecessorList<Account__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Account__name(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), memento, Account.GetUnloadedInstance, Account.GetNullInstance);
            _prior = new PredecessorList<Account__name>(this, GetRolePrior(), memento, Account__name.GetUnloadedInstance, Account__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Account Account
        {
            get { return IsNull ? Account.GetNullInstance() : _account.Fact; }
        }
        public PredecessorList<Account__name> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class AccountDelete : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				AccountDelete newFact = new AccountDelete(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				AccountDelete fact = (AccountDelete)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return AccountDelete.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return AccountDelete.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.AccountDelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static AccountDelete GetUnloadedInstance()
        {
            return new AccountDelete((FactMemento)null) { IsLoaded = false };
        }

        public static AccountDelete GetNullInstance()
        {
            return new AccountDelete((FactMemento)null) { IsNull = true };
        }

        public AccountDelete Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                AccountDelete fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (AccountDelete)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleAccount;
        public static Role GetRoleAccount()
        {
            if (_cacheRoleAccount == null)
            {
                _cacheRoleAccount = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "account",
			        Account._correspondenceFactType,
			        false));
            }
            return _cacheRoleAccount;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Account> _account;

        // Fields

        // Results

        // Business constructor
        public AccountDelete(
            Account account
            )
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), account);
        }

        // Hydration constructor
        private AccountDelete(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), memento, Account.GetUnloadedInstance, Account.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Account Account
        {
            get { return IsNull ? Account.GetNullInstance() : _account.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Book : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Book newFact = new Book(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Book fact = (Book)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Book.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Book.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Book", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Book GetUnloadedInstance()
        {
            return new Book((FactMemento)null) { IsLoaded = false };
        }

        public static Book GetNullInstance()
        {
            return new Book((FactMemento)null) { IsNull = true };
        }

        public Book Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Book fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Book)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleAccount;
        public static Role GetRoleAccount()
        {
            if (_cacheRoleAccount == null)
            {
                _cacheRoleAccount = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "account",
			        Account._correspondenceFactType,
			        false));
            }
            return _cacheRoleAccount;
        }
        private static Role _cacheRoleYear;
        public static Role GetRoleYear()
        {
            if (_cacheRoleYear == null)
            {
                _cacheRoleYear = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "year",
			        Year._correspondenceFactType,
			        false));
            }
            return _cacheRoleYear;
        }

        // Queries
        private static Query _cacheQueryCredits;

        public static Query GetQueryCredits()
		{
            if (_cacheQueryCredits == null)
            {
			    _cacheQueryCredits = new Query()
    				.JoinSuccessors(Entry.GetRoleCredit(), Condition.WhereIsEmpty(Entry.GetQueryIsVoided())
				)
                ;
            }
            return _cacheQueryCredits;
		}
        private static Query _cacheQueryDebits;

        public static Query GetQueryDebits()
		{
            if (_cacheQueryDebits == null)
            {
			    _cacheQueryDebits = new Query()
    				.JoinSuccessors(Entry.GetRoleDebit(), Condition.WhereIsEmpty(Entry.GetQueryIsVoided())
				)
                ;
            }
            return _cacheQueryDebits;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Account> _account;
        private PredecessorObj<Year> _year;

        // Fields

        // Results
        private Result<Entry> _credits;
        private Result<Entry> _debits;

        // Business constructor
        public Book(
            Account account
            ,Year year
            )
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), account);
            _year = new PredecessorObj<Year>(this, GetRoleYear(), year);
        }

        // Hydration constructor
        private Book(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, GetRoleAccount(), memento, Account.GetUnloadedInstance, Account.GetNullInstance);
            _year = new PredecessorObj<Year>(this, GetRoleYear(), memento, Year.GetUnloadedInstance, Year.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _credits = new Result<Entry>(this, GetQueryCredits(), Entry.GetUnloadedInstance, Entry.GetNullInstance);
            _debits = new Result<Entry>(this, GetQueryDebits(), Entry.GetUnloadedInstance, Entry.GetNullInstance);
        }

        // Predecessor access
        public Account Account
        {
            get { return IsNull ? Account.GetNullInstance() : _account.Fact; }
        }
        public Year Year
        {
            get { return IsNull ? Year.GetNullInstance() : _year.Fact; }
        }

        // Field access

        // Query result access
        public Result<Entry> Credits
        {
            get { return _credits; }
        }
        public Result<Entry> Debits
        {
            get { return _debits; }
        }

        // Mutable property access

    }
    
    public partial class Entry : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Entry newFact = new Entry(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._entryDate = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._entryDatePadding = (byte)_fieldSerializerByType[typeof(byte)].ReadData(output);
						newFact._amount = (float)_fieldSerializerByType[typeof(float)].ReadData(output);
						newFact._created = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._createdPadding = (byte)_fieldSerializerByType[typeof(byte)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Entry fact = (Entry)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._entryDate);
				_fieldSerializerByType[typeof(byte)].WriteData(output, fact._entryDatePadding);
				_fieldSerializerByType[typeof(float)].WriteData(output, fact._amount);
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._created);
				_fieldSerializerByType[typeof(byte)].WriteData(output, fact._createdPadding);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Entry.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Entry.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Entry GetUnloadedInstance()
        {
            return new Entry((FactMemento)null) { IsLoaded = false };
        }

        public static Entry GetNullInstance()
        {
            return new Entry((FactMemento)null) { IsNull = true };
        }

        public Entry Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Entry fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Entry)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleCredit;
        public static Role GetRoleCredit()
        {
            if (_cacheRoleCredit == null)
            {
                _cacheRoleCredit = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "credit",
			        Book._correspondenceFactType,
			        false));
            }
            return _cacheRoleCredit;
        }
        private static Role _cacheRoleDebit;
        public static Role GetRoleDebit()
        {
            if (_cacheRoleDebit == null)
            {
                _cacheRoleDebit = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "debit",
			        Book._correspondenceFactType,
			        false));
            }
            return _cacheRoleDebit;
        }

        // Queries
        private static Query _cacheQueryId;

        public static Query GetQueryId()
		{
            if (_cacheQueryId == null)
            {
			    _cacheQueryId = new Query()
    				.JoinSuccessors(Entry__id.GetRoleEntry(), Condition.WhereIsEmpty(Entry__id.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryId;
		}
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(Entry__description.GetRoleEntry(), Condition.WhereIsEmpty(Entry__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryIsVoided;

        public static Query GetQueryIsVoided()
		{
            if (_cacheQueryIsVoided == null)
            {
			    _cacheQueryIsVoided = new Query()
		    		.JoinSuccessors(EntryVoid.GetRoleEntry())
                ;
            }
            return _cacheQueryIsVoided;
		}

        // Predicates
        public static Condition IsVoided = Condition.WhereIsNotEmpty(GetQueryIsVoided());

        // Predecessors
        private PredecessorObj<Book> _credit;
        private PredecessorObj<Book> _debit;

        // Fields
        private DateTime _entryDate;
        private byte _entryDatePadding;
        private float _amount;
        private DateTime _created;
        private byte _createdPadding;

        // Results
        private Result<Entry__id> _id;
        private Result<Entry__description> _description;

        // Business constructor
        public Entry(
            Book credit
            ,Book debit
            ,DateTime entryDate
            ,byte entryDatePadding
            ,float amount
            ,DateTime created
            ,byte createdPadding
            )
        {
            InitializeResults();
            _credit = new PredecessorObj<Book>(this, GetRoleCredit(), credit);
            _debit = new PredecessorObj<Book>(this, GetRoleDebit(), debit);
            _entryDate = entryDate;
            _entryDatePadding = entryDatePadding;
            _amount = amount;
            _created = created;
            _createdPadding = createdPadding;
        }

        // Hydration constructor
        private Entry(FactMemento memento)
        {
            InitializeResults();
            _credit = new PredecessorObj<Book>(this, GetRoleCredit(), memento, Book.GetUnloadedInstance, Book.GetNullInstance);
            _debit = new PredecessorObj<Book>(this, GetRoleDebit(), memento, Book.GetUnloadedInstance, Book.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _id = new Result<Entry__id>(this, GetQueryId(), Entry__id.GetUnloadedInstance, Entry__id.GetNullInstance);
            _description = new Result<Entry__description>(this, GetQueryDescription(), Entry__description.GetUnloadedInstance, Entry__description.GetNullInstance);
        }

        // Predecessor access
        public Book Credit
        {
            get { return IsNull ? Book.GetNullInstance() : _credit.Fact; }
        }
        public Book Debit
        {
            get { return IsNull ? Book.GetNullInstance() : _debit.Fact; }
        }

        // Field access
        public DateTime EntryDate
        {
            get { return _entryDate; }
        }
        public byte EntryDatePadding
        {
            get { return _entryDatePadding; }
        }
        public float Amount
        {
            get { return _amount; }
        }
        public DateTime Created
        {
            get { return _created; }
        }
        public byte CreatedPadding
        {
            get { return _createdPadding; }
        }

        // Query result access

        // Mutable property access
        public TransientDisputable<Entry__id, string> Id
        {
            get { return _id.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _id.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Entry__id(this, _id, value.Value));
				}
			}
        }
        public TransientDisputable<Entry__description, string> Description
        {
            get { return _description.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _description.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Entry__description(this, _description, value.Value));
				}
			}
        }

    }
    
    public partial class Entry__id : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Entry__id newFact = new Entry__id(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Entry__id fact = (Entry__id)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Entry__id.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Entry__id.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry__id", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Entry__id GetUnloadedInstance()
        {
            return new Entry__id((FactMemento)null) { IsLoaded = false };
        }

        public static Entry__id GetNullInstance()
        {
            return new Entry__id((FactMemento)null) { IsNull = true };
        }

        public Entry__id Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Entry__id fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Entry__id)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleEntry;
        public static Role GetRoleEntry()
        {
            if (_cacheRoleEntry == null)
            {
                _cacheRoleEntry = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "entry",
			        Entry._correspondenceFactType,
			        false));
            }
            return _cacheRoleEntry;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Entry__id._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Entry__id.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Entry> _entry;
        private PredecessorList<Entry__id> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Entry__id(
            Entry entry
            ,IEnumerable<Entry__id> prior
            ,string value
            )
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), entry);
            _prior = new PredecessorList<Entry__id>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Entry__id(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), memento, Entry.GetUnloadedInstance, Entry.GetNullInstance);
            _prior = new PredecessorList<Entry__id>(this, GetRolePrior(), memento, Entry__id.GetUnloadedInstance, Entry__id.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return IsNull ? Entry.GetNullInstance() : _entry.Fact; }
        }
        public PredecessorList<Entry__id> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Entry__description : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Entry__description newFact = new Entry__description(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Entry__description fact = (Entry__description)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Entry__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Entry__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry__description", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Entry__description GetUnloadedInstance()
        {
            return new Entry__description((FactMemento)null) { IsLoaded = false };
        }

        public static Entry__description GetNullInstance()
        {
            return new Entry__description((FactMemento)null) { IsNull = true };
        }

        public Entry__description Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Entry__description fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Entry__description)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleEntry;
        public static Role GetRoleEntry()
        {
            if (_cacheRoleEntry == null)
            {
                _cacheRoleEntry = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "entry",
			        Entry._correspondenceFactType,
			        false));
            }
            return _cacheRoleEntry;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Entry__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Entry__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Entry> _entry;
        private PredecessorList<Entry__description> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Entry__description(
            Entry entry
            ,IEnumerable<Entry__description> prior
            ,string value
            )
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), entry);
            _prior = new PredecessorList<Entry__description>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Entry__description(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), memento, Entry.GetUnloadedInstance, Entry.GetNullInstance);
            _prior = new PredecessorList<Entry__description>(this, GetRolePrior(), memento, Entry__description.GetUnloadedInstance, Entry__description.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return IsNull ? Entry.GetNullInstance() : _entry.Fact; }
        }
        public PredecessorList<Entry__description> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class EntryVoid : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				EntryVoid newFact = new EntryVoid(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._voided = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._voidedPadding = (byte)_fieldSerializerByType[typeof(byte)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				EntryVoid fact = (EntryVoid)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._voided);
				_fieldSerializerByType[typeof(byte)].WriteData(output, fact._voidedPadding);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return EntryVoid.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return EntryVoid.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.EntryVoid", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static EntryVoid GetUnloadedInstance()
        {
            return new EntryVoid((FactMemento)null) { IsLoaded = false };
        }

        public static EntryVoid GetNullInstance()
        {
            return new EntryVoid((FactMemento)null) { IsNull = true };
        }

        public EntryVoid Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                EntryVoid fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (EntryVoid)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleEntry;
        public static Role GetRoleEntry()
        {
            if (_cacheRoleEntry == null)
            {
                _cacheRoleEntry = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "entry",
			        Entry._correspondenceFactType,
			        false));
            }
            return _cacheRoleEntry;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Entry> _entry;

        // Fields
        private DateTime _voided;
        private byte _voidedPadding;

        // Results

        // Business constructor
        public EntryVoid(
            Entry entry
            ,DateTime voided
            ,byte voidedPadding
            )
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), entry);
            _voided = voided;
            _voidedPadding = voidedPadding;
        }

        // Hydration constructor
        private EntryVoid(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, GetRoleEntry(), memento, Entry.GetUnloadedInstance, Entry.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return IsNull ? Entry.GetNullInstance() : _entry.Fact; }
        }

        // Field access
        public DateTime Voided
        {
            get { return _voided; }
        }
        public byte VoidedPadding
        {
            get { return _voidedPadding; }
        }

        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Identity._correspondenceFactType,
				new Identity.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Identity._correspondenceFactType }));
			community.AddQuery(
				Identity._correspondenceFactType,
				Identity.GetQueryActiveShares().QueryDefinition);
			community.AddType(
				Share._correspondenceFactType,
				new Share.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Share._correspondenceFactType }));
			community.AddQuery(
				Share._correspondenceFactType,
				Share.GetQueryIsActive().QueryDefinition);
			community.AddType(
				ShareRevoke._correspondenceFactType,
				new ShareRevoke.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ShareRevoke._correspondenceFactType }));
			community.AddType(
				Company._correspondenceFactType,
				new Company.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company._correspondenceFactType }));
			community.AddQuery(
				Company._correspondenceFactType,
				Company.GetQueryName().QueryDefinition);
			community.AddQuery(
				Company._correspondenceFactType,
				Company.GetQueryAccounts().QueryDefinition);
			community.AddType(
				Company__name._correspondenceFactType,
				new Company__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company__name._correspondenceFactType }));
			community.AddQuery(
				Company__name._correspondenceFactType,
				Company__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Year._correspondenceFactType,
				new Year.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Year._correspondenceFactType }));
			community.AddType(
				Account._correspondenceFactType,
				new Account.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Account._correspondenceFactType }));
			community.AddQuery(
				Account._correspondenceFactType,
				Account.GetQueryName().QueryDefinition);
			community.AddQuery(
				Account._correspondenceFactType,
				Account.GetQueryIsDeleted().QueryDefinition);
			community.AddType(
				Account__name._correspondenceFactType,
				new Account__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Account__name._correspondenceFactType }));
			community.AddQuery(
				Account__name._correspondenceFactType,
				Account__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				AccountDelete._correspondenceFactType,
				new AccountDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { AccountDelete._correspondenceFactType }));
			community.AddType(
				Book._correspondenceFactType,
				new Book.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Book._correspondenceFactType }));
			community.AddQuery(
				Book._correspondenceFactType,
				Book.GetQueryCredits().QueryDefinition);
			community.AddQuery(
				Book._correspondenceFactType,
				Book.GetQueryDebits().QueryDefinition);
			community.AddType(
				Entry._correspondenceFactType,
				new Entry.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry._correspondenceFactType }));
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.GetQueryId().QueryDefinition);
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.GetQueryIsVoided().QueryDefinition);
			community.AddType(
				Entry__id._correspondenceFactType,
				new Entry__id.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry__id._correspondenceFactType }));
			community.AddQuery(
				Entry__id._correspondenceFactType,
				Entry__id.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Entry__description._correspondenceFactType,
				new Entry__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry__description._correspondenceFactType }));
			community.AddQuery(
				Entry__description._correspondenceFactType,
				Entry__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				EntryVoid._correspondenceFactType,
				new EntryVoid.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { EntryVoid._correspondenceFactType }));
		}
	}
}
