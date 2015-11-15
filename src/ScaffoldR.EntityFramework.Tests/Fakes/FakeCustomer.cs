using System.ComponentModel.DataAnnotations;
using ScaffoldR.Core.Entities;

namespace ScaffoldR.EntityFramework.Tests.Fakes
{
    public class FakeCustomer : EntityWithId<int>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
} 