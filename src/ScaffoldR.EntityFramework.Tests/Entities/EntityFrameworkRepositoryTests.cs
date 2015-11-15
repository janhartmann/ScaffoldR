using System.Collections.Generic;
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
            var customers = new List<FakeCustomer>
            {
                new FakeCustomer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                 new FakeCustomer
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Smith"
                }
            };

            var dbSet = EntityFrameworkMockHelper.MockDbSet(customers);
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.Set<FakeCustomer>().AsNoTracking()).Returns(dbSet);
            context.Object.FakeCustomers.AddRange(customers);

            var repository = new EntityFrameworkRepository<FakeCustomer>(() => context.Object);
            var customersWithNameJohn = repository.Query().Where(x => x.FirstName == "John").ToList();

            Assert.NotNull(customersWithNameJohn);
            Assert.Equal(2, customersWithNameJohn.Count);
        }

        [Fact]
        public void Get_CanGetWritableEntity_WhenEntityExists()
        {
            var customers = new List<FakeCustomer>
            {
                new FakeCustomer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                 new FakeCustomer
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Smith"
                }
            };

            var dbSet = EntityFrameworkMockHelper.MockDbSet(customers);
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.Set<FakeCustomer>()).Returns(dbSet);
            context.Object.FakeCustomers.AddRange(customers);

            var repository = new EntityFrameworkRepository<FakeCustomer>(() => context.Object);
            var customer = repository.Get(1);

            Assert.NotNull(customer);
            Assert.Equal(1, customer.Id);
        }

        [Fact]
        public void Save_CanCreateEntity_WhenEntityIsNotTrackedByContext()
        {
            var customers = new List<FakeCustomer>();
            var customer = new FakeCustomer
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var dbSet = EntityFrameworkMockHelper.MockDbSet(customers);
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.Set<FakeCustomer>()).Returns(dbSet);

            var repository = new EntityFrameworkRepository<FakeCustomer>(() => context.Object);
            repository.Save(customer);

            Assert.Equal(1, customers.Count);
        }

        [Fact]
        public void Save_CanUpdateEntity_WhenEntityIsTrackedByContext()
        {
            var customers = new List<FakeCustomer>
            {
                new FakeCustomer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };

            var dbSet = EntityFrameworkMockHelper.MockDbSet(customers);
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.Set<FakeCustomer>()).Returns(dbSet);
            context.Object.FakeCustomers.AddRange(customers);

            Assert.Equal("John", context.Object.FakeCustomers.First().FirstName);

            var repository = new EntityFrameworkRepository<FakeCustomer>(() => context.Object);
            var customer = repository.Get(1);
            customer.FirstName = "Artina";

            repository.Save(customer);

            Assert.Equal(1, customers.Count);
            Assert.Equal("Artina", customers.First().FirstName);
            Assert.Equal(1, customers.First().Id);
        }

        [Fact]
        public void Delete_CanMarkEntityForDeletion_WhenEntityExists()
        {
            var customers = new List<FakeCustomer>
            {
                new FakeCustomer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };

            var dbSet = EntityFrameworkMockHelper.MockDbSet(customers);
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.Set<FakeCustomer>()).Returns(dbSet);
            context.Object.FakeCustomers.AddRange(customers);

            var repository = new EntityFrameworkRepository<FakeCustomer>(() => context.Object);
            var customer = repository.Get(1);
            Assert.NotNull(customer);

            repository.Delete(customer);

            Assert.Equal(0, customers.Count);
        }
    }
}
