using ScaffoldR.Core.Entities;

namespace ScaffoldR.EntityFramework.Entities
{
    internal sealed class EntityWriterAdapter<TEntity> : IEntityWriter<TEntity>  where TEntity : Entity
    {
        private readonly EntityFrameworkRepository<TEntity> _repository;

        public EntityWriterAdapter(EntityFrameworkRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Get(object primaryKey)
        {
            return _repository.Get(primaryKey);
        }

        public void Save(TEntity entity)
        {
            _repository.Save(entity);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }
    }
}
