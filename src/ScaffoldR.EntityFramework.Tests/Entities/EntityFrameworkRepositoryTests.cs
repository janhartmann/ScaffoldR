using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ScaffoldR.EntityFramework.Entities;
using ScaffoldR.EntityFramework.Tests.Fakes;
using ScaffoldR.EntityFramework.Tests.Helpers;
using Xunit;

namespace ScaffoldR.EntityFramework.Tests.Entities
{
    public class EntityFrameworkRepositoryTests
    {
        [Fact]
        public void Query_CanGetMultipleReadOnlyEntities_WhichAreNotTrackedByContext()
        {
           using (var context = new FakeDbContext())
            {
                var repository = new EntityFrameworkRepository<FakeCustomer>(() => context);
                var customersWithNameJohn = repository.Query().Where(x => x.FirstName == "John").ToList();

                Assert.NotNull(customersWithNameJohn);
                Assert.Equal(2, customersWithNameJohn.Count);
            }
        }

        [Fact]
        public void Get_CanGetWritableEntity_WhenEntityExists()
        {
            using (var context = new FakeDbContext())
            {
                var repository = new EntityFrameworkRepository<FakeCustomer>(() => context);
                var customer = repository.Get(1);

                Assert.NotNull(customer);
                Assert.Equal(EntityState.Unchanged, context.Entry(customer).State);
            }
        }

        [Fact]
        public void Save_CanCreateEntity_WhenEntityIsNotTrackedByContext()
        {
            using (var context = new FakeDbContext())
            {
                var repository = new EntityFrameworkRepository<FakeCustomer>(() => context);
                var customer = new FakeCustomer
                {
                    FirstName = "John",
                    LastName = "Doe"
                };

                repository.Save(customer);

                Assert.Equal(EntityState.Added, context.Entry(customer).State);
            }
        }

        [Fact]
        public void Save_CanUpdateEntity_WhenEntityIsTrackedByContext()
        {
            using (var context = new FakeDbContext())
            {
                var repository = new EntityFrameworkRepository<FakeCustomer>(() => context);
                var customer = repository.Get(1);

                Assert.Equal("John", customer.FirstName);

                // Change name
                customer.FirstName = "Artina";

                repository.Save(customer);
                var affectedRows = context.SaveChanges();

                Assert.Equal(1, affectedRows);
                customer = repository.Get(1);
                Assert.Equal("Artina", customer.FirstName);
            }
        }

        [Fact]
        public void Delete_CanMarkEntityForDeletion_WhenEntityExists()
        {
            using (var context = new FakeDbContext())
            {
                var repository = new EntityFrameworkRepository<FakeCustomer>(() => context);
                var customer = repository.Get(1);
                Assert.NotNull(customer);

                repository.Delete(customer);

                Assert.Equal(EntityState.Deleted, context.Entry(customer).State);
            }
        }
    }
}
