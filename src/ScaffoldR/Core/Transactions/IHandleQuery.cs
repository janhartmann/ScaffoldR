namespace ScaffoldR.Core.Transactions
{
    /// <summary>
    /// Handles the <typeparamref name="TQuery"/> with the return type of <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TQuery">The query.</typeparam>
    /// <typeparam name="TResult">The return type of the <typeparamref name="TQuery"/>.</typeparam>
    public interface IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles the <typeparamref name="TQuery"/>. 
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Returns the <typeparamref name="TResult"/>.</returns>
        TResult Handle(TQuery query);
    }
}