using System;

namespace FacetedWorlds.Ledger.Model
{
    public partial class Book
    {
        public void AddCredit(DateTime entryDate, string id, string description, Account debit, float amount)
        {
            Entry entry = Community.AddFact(new Entry(this, debit.GetBook(Year.CalendarYear), entryDate, amount, DateTime.Now));
            if (!String.IsNullOrEmpty(id))
                entry.Id = id;
            if (!String.IsNullOrEmpty(description))
                entry.Description = description;
        }

        public void AddDebit(DateTime entryDate, string id, string description, Account credit, float amount)
        {
            Entry entry = Community.AddFact(new Entry(credit.GetBook(Year.CalendarYear), this, entryDate, amount, DateTime.Now));
            if (!String.IsNullOrEmpty(id))
                entry.Id = id;
            if (!String.IsNullOrEmpty(description))
                entry.Description = description;
        }
    }
}
