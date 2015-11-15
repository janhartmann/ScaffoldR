using System.Linq;
using ScaffoldR.Core.Entities;

namespace ScaffoldR.EntityFramework.Entities
{
    internal sealed class EntityReaderAdapter<TEntity> : IEntityReader<TEntity> where TEntity : Entity
    {
        private readonly EntityFrameworkRepository<TEntity> _repository;

        public EntityReaderAdapter(EntityFrameworkRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }
    }
}
