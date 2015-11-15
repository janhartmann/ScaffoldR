using System;

namespace ScaffoldR.Core.Transactions
{
    /// <summary>
    /// Processes queries.
    /// </summary>
    public interface IProcessQueries
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="TResult">The result of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query result object.</returns>
        /// <exception cref="ArgumentNullException">If the query is null.</exception>
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
