using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Identity", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query QueryActiveShares = new Query()
            .JoinSuccessors(Share.RoleIdentity, Condition.WhereIsEmpty(Share.QueryIsActive)
            )
            ;

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
            _activeShares = new Result<Share>(this, QueryActiveShares);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public IEnumerable<Share> ActiveShares
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Share", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleIdentity = new Role(new RoleMemento(
			_correspondenceFactType,
			"identity",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Identity", 1),
			true));
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Company", 1),
			false));

        // Queries
        public static Query QueryIsActive = new Query()
            .JoinSuccessors(ShareRevoke.RoleShare)
            ;

        // Predicates
        public static Condition IsActive = Condition.WhereIsEmpty(QueryIsActive);

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
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, identity);
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
        }

        // Hydration constructor
        private Share(FactMemento memento)
        {
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, memento);
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Identity Identity
        {
            get { return _identity.Fact; }
        }
        public Company Company
        {
            get { return _company.Fact; }
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

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ShareRevoke fact = (ShareRevoke)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.ShareRevoke", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleShare = new Role(new RoleMemento(
			_correspondenceFactType,
			"share",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Share", 1),
			false));

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
            _share = new PredecessorObj<Share>(this, RoleShare, share);
        }

        // Hydration constructor
        private ShareRevoke(FactMemento memento)
        {
            InitializeResults();
            _share = new PredecessorObj<Share>(this, RoleShare, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Share Share
        {
            get { return _share.Fact; }
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Company", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query QueryName = new Query()
            .JoinSuccessors(Company__name.RoleCompany, Condition.WhereIsEmpty(Company__name.QueryIsCurrent)
            )
            ;
        public static Query QueryAccounts = new Query()
            .JoinSuccessors(Account.RoleCompany, Condition.WhereIsEmpty(Account.QueryIsDeleted)
            )
            ;

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
            _name = new Result<Company__name>(this, QueryName);
            _accounts = new Result<Account>(this, QueryAccounts);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public IEnumerable<Account> Accounts
        {
            get { return _accounts; }
        }

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_name.Count() != 1 || !object.Equals(_name.Single().Value, value.Value))
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Company__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Company", 1),
			true));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Company__name", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Company__name.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

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
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _prior = new PredecessorList<Company__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Company__name(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
            _prior = new PredecessorList<Company__name>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }
        public IEnumerable<Company__name> Prior
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Year", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Company", 1),
			true));

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
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _calendarYear = calendarYear;
        }

        // Hydration constructor
        private Year(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Account", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Company", 1),
			true));

        // Queries
        public static Query QueryName = new Query()
            .JoinSuccessors(Account__name.RoleAccount, Condition.WhereIsEmpty(Account__name.QueryIsCurrent)
            )
            ;
        public static Query QueryIsDeleted = new Query()
            .JoinSuccessors(AccountDelete.RoleAccount)
            ;

        // Predicates
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(QueryIsDeleted);

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
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _type = type;
        }

        // Hydration constructor
        private Account(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Account__name>(this, QueryName);
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }

        public int Type
        {
            get { return _type; }
        }

        // Query result access

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_name.Count() != 1 || !object.Equals(_name.Single().Value, value.Value))
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Account__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleAccount = new Role(new RoleMemento(
			_correspondenceFactType,
			"account",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Account", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Account__name", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Account__name.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

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
            _account = new PredecessorObj<Account>(this, RoleAccount, account);
            _prior = new PredecessorList<Account__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Account__name(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, RoleAccount, memento);
            _prior = new PredecessorList<Account__name>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Account Account
        {
            get { return _account.Fact; }
        }
        public IEnumerable<Account__name> Prior
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

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				AccountDelete fact = (AccountDelete)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.AccountDelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleAccount = new Role(new RoleMemento(
			_correspondenceFactType,
			"account",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Account", 1),
			false));

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
            _account = new PredecessorObj<Account>(this, RoleAccount, account);
        }

        // Hydration constructor
        private AccountDelete(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, RoleAccount, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Account Account
        {
            get { return _account.Fact; }
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

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Book fact = (Book)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Book", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleAccount = new Role(new RoleMemento(
			_correspondenceFactType,
			"account",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Account", 1),
			false));
        public static Role RoleYear = new Role(new RoleMemento(
			_correspondenceFactType,
			"year",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Year", 1),
			false));

        // Queries
        public static Query QueryCredits = new Query()
            .JoinSuccessors(Entry.RoleCredit, Condition.WhereIsEmpty(Entry.QueryIsVoided)
            )
            ;
        public static Query QueryDebits = new Query()
            .JoinSuccessors(Entry.RoleDebit, Condition.WhereIsEmpty(Entry.QueryIsVoided)
            )
            ;

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
            _account = new PredecessorObj<Account>(this, RoleAccount, account);
            _year = new PredecessorObj<Year>(this, RoleYear, year);
        }

        // Hydration constructor
        private Book(FactMemento memento)
        {
            InitializeResults();
            _account = new PredecessorObj<Account>(this, RoleAccount, memento);
            _year = new PredecessorObj<Year>(this, RoleYear, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _credits = new Result<Entry>(this, QueryCredits);
            _debits = new Result<Entry>(this, QueryDebits);
        }

        // Predecessor access
        public Account Account
        {
            get { return _account.Fact; }
        }
        public Year Year
        {
            get { return _year.Fact; }
        }

        // Field access

        // Query result access
        public IEnumerable<Entry> Credits
        {
            get { return _credits; }
        }
        public IEnumerable<Entry> Debits
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
						newFact._amount = (float)_fieldSerializerByType[typeof(float)].ReadData(output);
						newFact._created = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Entry fact = (Entry)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._entryDate);
				_fieldSerializerByType[typeof(float)].WriteData(output, fact._amount);
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._created);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCredit = new Role(new RoleMemento(
			_correspondenceFactType,
			"credit",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Book", 1),
			false));
        public static Role RoleDebit = new Role(new RoleMemento(
			_correspondenceFactType,
			"debit",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Book", 1),
			false));

        // Queries
        public static Query QueryId = new Query()
            .JoinSuccessors(Entry__id.RoleEntry, Condition.WhereIsEmpty(Entry__id.QueryIsCurrent)
            )
            ;
        public static Query QueryDescription = new Query()
            .JoinSuccessors(Entry__description.RoleEntry, Condition.WhereIsEmpty(Entry__description.QueryIsCurrent)
            )
            ;
        public static Query QueryIsVoided = new Query()
            .JoinSuccessors(EntryVoid.RoleEntry)
            ;

        // Predicates
        public static Condition IsVoided = Condition.WhereIsNotEmpty(QueryIsVoided);

        // Predecessors
        private PredecessorObj<Book> _credit;
        private PredecessorObj<Book> _debit;

        // Fields
        private DateTime _entryDate;
        private float _amount;
        private DateTime _created;

        // Results
        private Result<Entry__id> _id;
        private Result<Entry__description> _description;

        // Business constructor
        public Entry(
            Book credit
            ,Book debit
            ,DateTime entryDate
            ,float amount
            ,DateTime created
            )
        {
            InitializeResults();
            _credit = new PredecessorObj<Book>(this, RoleCredit, credit);
            _debit = new PredecessorObj<Book>(this, RoleDebit, debit);
            _entryDate = entryDate;
            _amount = amount;
            _created = created;
        }

        // Hydration constructor
        private Entry(FactMemento memento)
        {
            InitializeResults();
            _credit = new PredecessorObj<Book>(this, RoleCredit, memento);
            _debit = new PredecessorObj<Book>(this, RoleDebit, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _id = new Result<Entry__id>(this, QueryId);
            _description = new Result<Entry__description>(this, QueryDescription);
        }

        // Predecessor access
        public Book Credit
        {
            get { return _credit.Fact; }
        }
        public Book Debit
        {
            get { return _debit.Fact; }
        }

        // Field access
        public DateTime EntryDate
        {
            get { return _entryDate; }
        }
        public float Amount
        {
            get { return _amount; }
        }
        public DateTime Created
        {
            get { return _created; }
        }

        // Query result access

        // Mutable property access
        public Disputable<string> Id
        {
            get { return _id.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_id.Count() != 1 || !object.Equals(_id.Single().Value, value.Value))
				{
					Community.AddFact(new Entry__id(this, _id, value.Value));
				}
			}
        }
        public Disputable<string> Description
        {
            get { return _description.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_description.Count() != 1 || !object.Equals(_description.Single().Value, value.Value))
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry__id", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleEntry = new Role(new RoleMemento(
			_correspondenceFactType,
			"entry",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Entry", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Entry__id", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Entry__id.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

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
            _entry = new PredecessorObj<Entry>(this, RoleEntry, entry);
            _prior = new PredecessorList<Entry__id>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Entry__id(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, RoleEntry, memento);
            _prior = new PredecessorList<Entry__id>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return _entry.Fact; }
        }
        public IEnumerable<Entry__id> Prior
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
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.Entry__description", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleEntry = new Role(new RoleMemento(
			_correspondenceFactType,
			"entry",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Entry", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Entry__description", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Entry__description.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

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
            _entry = new PredecessorObj<Entry>(this, RoleEntry, entry);
            _prior = new PredecessorList<Entry__description>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Entry__description(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, RoleEntry, memento);
            _prior = new PredecessorList<Entry__description>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return _entry.Fact; }
        }
        public IEnumerable<Entry__description> Prior
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
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				EntryVoid fact = (EntryVoid)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._voided);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.Ledger.Model.EntryVoid", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleEntry = new Role(new RoleMemento(
			_correspondenceFactType,
			"entry",
			new CorrespondenceFactType("FacetedWorlds.Ledger.Model.Entry", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Entry> _entry;

        // Fields
        private DateTime _voided;

        // Results

        // Business constructor
        public EntryVoid(
            Entry entry
            ,DateTime voided
            )
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, RoleEntry, entry);
            _voided = voided;
        }

        // Hydration constructor
        private EntryVoid(FactMemento memento)
        {
            InitializeResults();
            _entry = new PredecessorObj<Entry>(this, RoleEntry, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Entry Entry
        {
            get { return _entry.Fact; }
        }

        // Field access
        public DateTime Voided
        {
            get { return _voided; }
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
				Identity.QueryActiveShares.QueryDefinition);
			community.AddType(
				Share._correspondenceFactType,
				new Share.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Share._correspondenceFactType }));
			community.AddQuery(
				Share._correspondenceFactType,
				Share.QueryIsActive.QueryDefinition);
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
				Company.QueryName.QueryDefinition);
			community.AddQuery(
				Company._correspondenceFactType,
				Company.QueryAccounts.QueryDefinition);
			community.AddType(
				Company__name._correspondenceFactType,
				new Company__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company__name._correspondenceFactType }));
			community.AddQuery(
				Company__name._correspondenceFactType,
				Company__name.QueryIsCurrent.QueryDefinition);
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
				Account.QueryName.QueryDefinition);
			community.AddQuery(
				Account._correspondenceFactType,
				Account.QueryIsDeleted.QueryDefinition);
			community.AddType(
				Account__name._correspondenceFactType,
				new Account__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Account__name._correspondenceFactType }));
			community.AddQuery(
				Account__name._correspondenceFactType,
				Account__name.QueryIsCurrent.QueryDefinition);
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
				Book.QueryCredits.QueryDefinition);
			community.AddQuery(
				Book._correspondenceFactType,
				Book.QueryDebits.QueryDefinition);
			community.AddType(
				Entry._correspondenceFactType,
				new Entry.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry._correspondenceFactType }));
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.QueryId.QueryDefinition);
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.QueryDescription.QueryDefinition);
			community.AddQuery(
				Entry._correspondenceFactType,
				Entry.QueryIsVoided.QueryDefinition);
			community.AddType(
				Entry__id._correspondenceFactType,
				new Entry__id.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry__id._correspondenceFactType }));
			community.AddQuery(
				Entry__id._correspondenceFactType,
				Entry__id.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Entry__description._correspondenceFactType,
				new Entry__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Entry__description._correspondenceFactType }));
			community.AddQuery(
				Entry__description._correspondenceFactType,
				Entry__description.QueryIsCurrent.QueryDefinition);
			community.AddType(
				EntryVoid._correspondenceFactType,
				new EntryVoid.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { EntryVoid._correspondenceFactType }));
		}
	}
}
