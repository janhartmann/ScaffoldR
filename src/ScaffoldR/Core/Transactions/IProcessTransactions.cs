namespace ScaffoldR.Core.Transactions
{
    /// <summary>
    /// Executes the transaction, e.g. synchronizes data state changes with an underlying data store.
    /// </summary>
    public interface IProcessTransactions
    {
        /// <summary>
        /// Executes the transaction
        /// </summary>
        void Execute();
    }
}
