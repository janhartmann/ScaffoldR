using FluentValidation.Results;
using ScaffoldR.Core.Commands;
using ScaffoldR.Core.Queries;
using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Core.Validation
{
    /// <summary>
    /// Processes command and query validation.
    /// </summary>
    public interface IProcessValidation
    {
        /// <summary>
        /// Validates if a command is valid for execution.
        /// </summary>
        /// <param name="command">The command which should be validated.</param>
        /// <returns>The <see cref="ValidationResult"/> of the command.</returns>
        ValidationResult Validate(ICommand command);

        /// <summary>
        /// Validates if a query is valid for execution.
        /// </summary>
        /// <param name="query">The query which should be validated.</param>
        /// <returns>The <see cref="ValidationResult"/> of the query</returns>
        ValidationResult Validate<TResult>(IQuery<TResult> query);
    }
}
