using System;
using System.Data.Entity;
using System.Linq;
using ScaffoldR.Core.Entities;

namespace ScaffoldR.EntityFramework.Entities
{
    internal sealed class EntityFrameworkRepository<TEntity> : IEntityWriter<TEntity>, IEntityReader<TEntity> where TEntity : Entity
    {
        private readonly Func<DbContext> _contextProvider;

        public EntityFrameworkRepository(Func<DbContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void Save(TEntity entity)
        {
            var context = _contextProvider();
            var entry = context.Entry(entity);

            // If it is not tracked by the context, add it to the context
            if (entry.State == EntityState.Detached)
            {
                // This also sets the entity state to added.
                context.Set<TEntity>().Add(entity);
            }
            else
            {
                // Tells the context that the entity should be updated during saving changes
                entry.State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity)
        {
            var context = _contextProvider();
            var entry = context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                // This also sets the entity state to Deleted.
                context.Set<TEntity>().Remove(entity);
            }  
        }

        public IQueryable<TEntity> Query()
        {
            return _contextProvider().Set<TEntity>().AsNoTracking();
        }

        public TEntity Get(object primaryKey)
        {
            var context = _contextProvider();
            var entity = context.Set<TEntity>().Find(primaryKey);
            if (entity == null) return null;

            // We found the entity, set the state to unchanged.
            context.Entry(entity).State = EntityState.Unchanged;

            return entity;
        }
    }
}
