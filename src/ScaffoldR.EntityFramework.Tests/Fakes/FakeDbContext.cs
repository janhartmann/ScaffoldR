using System.Data.Entity;

namespace ScaffoldR.EntityFramework.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        public virtual DbSet<FakeCustomer> FakeCustomers { get; set; }
    }
}
