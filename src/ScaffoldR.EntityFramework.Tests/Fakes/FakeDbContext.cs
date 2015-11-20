using System.Data.Entity;

namespace ScaffoldR.EntityFramework.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        protected FakeDbContext() : base(@"Server=(local)\SQL2008R2SP2;Database=master;User ID=sa;Password=Password12!")
        {
        }

        public virtual DbSet<FakeCustomer> FakeCustomers { get; set; }
    }
}
