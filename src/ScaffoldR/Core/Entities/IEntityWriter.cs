namespace ScaffoldR.Core.Entities
{
    /// <summary>
    /// Informs an underlying  data store to accept sets of writeable entity instances.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityWriter<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Inform an underlying data store to return a single writable entity instance.
        /// </summary>
        /// <param name="primaryKey">Primary key value of the entity instance that the underlying data store should return.</param>
        /// <returns>A single writable entity instance whose primary key matches the argument value(, if one exists in the underlying data store. Otherwise, null.</returns>
        TEntity Get(object primaryKey);

        /// <summary>
        /// Inform the underlying data store that a new or existing entity instance's should be saved to a set of entity instances.
        /// </summary>
        /// <param name="entity">Entity instance that should be saved to the TEntity set by the underlying data store.</param>
        void Save(TEntity entity);

        /// <summary>
        /// Inform the underlying data store that an existing entity instance should be permanently removed from its set of entity instances.
        /// </summary>
        /// <param name="entity">Entity instance that should be permanently removed from the TEntity set by the underlying data store.</param>
        void Delete(TEntity entity);
    }
}
