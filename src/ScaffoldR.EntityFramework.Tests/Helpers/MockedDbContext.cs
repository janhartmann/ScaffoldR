using System;
using System.Collections.Generic;
using System.Data.Entity;
using Moq;

namespace ScaffoldR.EntityFramework.Tests.Helpers
{
    public class MockedDbContext<TContext> : Mock<TContext>, IDisposable where TContext : DbContext
    {
        private Dictionary<string, object> _tables;

        public Dictionary<string, object> Tables => _tables ?? (_tables = new Dictionary<string, object>());

        public void Dispose()
        {
            Object.Dispose();
        }
    }
}
