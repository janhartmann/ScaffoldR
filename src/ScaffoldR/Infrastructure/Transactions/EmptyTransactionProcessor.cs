using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Infrastructure.Transactions
{
    internal sealed class EmptyTransactionProcessor : IProcessTransactions
    {
        public void Execute()
        {
            // I do nothing, this interface is implemented in sub packages.
        }
    }
}