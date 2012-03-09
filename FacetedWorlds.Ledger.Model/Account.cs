
namespace FacetedWorlds.Ledger.Model
{
    public partial class Account
    {
        public void Delete()
        {
            Community.AddFact(new AccountDelete(this));
        }

        public Book GetBook(int calendarYear)
        {
            Year year = Community.AddFact(new Year(Company, calendarYear));
            return Community.AddFact(new Book(this, year));
        }
    }
}
