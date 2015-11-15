using System.Linq;

namespace ScaffoldR.Core.Entities
{
    /// <summary>
    /// Inform an underlying data store to return a set of read-only entity instances.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to return read-only entity instances of.</typeparam>
    public interface IEntityReader<out TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Inform an underlying data store to return a set of read-only entity instances.
        /// </summary>
        /// <returns>IQueryable for set of read-only TEntity instances from an underlying data store.</returns>
        IQueryable<TEntity> Query();
    }
}
