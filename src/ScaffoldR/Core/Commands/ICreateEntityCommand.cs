using ScaffoldR.Core.Entities;

namespace ScaffoldR.Core.Commands
{
    /// <summary>
    /// Defines the command as an entity creation command. 
    /// </summary>
    /// <typeparam name="TEntity">The entity type which should be created.</typeparam>
    public interface ICreateEntityCommand<TEntity> : ICommand where TEntity : Entity
    {
        /// <summary>
        /// The created entity instance.
        /// </summary>
        TEntity CreatedEntity { get; set; }
    }
}
