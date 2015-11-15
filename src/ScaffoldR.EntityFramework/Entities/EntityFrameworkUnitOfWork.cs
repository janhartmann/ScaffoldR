using System;
using System.Data.Entity;
using ScaffoldR.Core.Transactions;

namespace ScaffoldR.EntityFramework.Entities
{
    internal sealed class EntityFrameworkUnitOfWork : IProcessTransactions
    {
        private readonly Func<DbContext> _contextProvider;

        public EntityFrameworkUnitOfWork(Func<DbContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void Execute()
        {
            _contextProvider().SaveChanges();
        }
    }
}
