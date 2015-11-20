using System;
using System.Data.Entity;

namespace ScaffoldR.EntityFramework.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        /// <summary>
        /// This field is for supporting AppVeyor CI configuration
        /// </summary>
        private static readonly string DefaultConnectionString = 
            Environment.GetEnvironmentVariable("DefaultConnectionString") ??
            @"Data Source=.\SQLEXPRESS;Initial Catalog=ScaffoldR_IntegrationTests;Integrated Security=True;MultipleActiveResultSets=True;";
       
        public virtual DbSet<FakeCustomer> FakeCustomers { get; set; }

        public FakeDbContext() : base(DefaultConnectionString)
        {
            Database.SetInitializer(new FakeDbContextInitializer());
        }
    }
}
