using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaffoldR.EntityFramework.Tests.Fakes
{
    public class FakeDbContextInitializer : DropCreateDatabaseAlways<FakeDbContext>
    {
        protected override void Seed(FakeDbContext context)
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

            foreach (var customer in customers)
            {
                context.FakeCustomers.Add(customer);
            }
               
            base.Seed(context);
        }
    }
}
