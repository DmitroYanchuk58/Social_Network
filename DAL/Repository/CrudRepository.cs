using DAL.DatabaseContextNamespace;
using DAL.Entities;
using DAL.Helpers.Interfaces;

namespace DAL.Repository
{
    public class CrudRepository<T> where T : Entity
    {
        private readonly DatabaseContext _context;
        private readonly IEntityHelper<T> _entityHelper;

        public CrudRepository(DatabaseContext context, IEntityHelper<T> helper)
        {
            this._context = context;
            this._entityHelper = helper;
        }

        public void Create(T newEntity)
        {
            if (newEntity == null || _entityHelper.IsEmpty(newEntity))
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            _context.Set<T>().Add(newEntity);
            _context.SaveChanges();
        }

        public void Update(Guid idUpdatedEntity, T updatedEntity)
        {
            if (updatedEntity == null || _entityHelper.IsEmpty(updatedEntity))
            {
                throw new ArgumentNullException(nameof(updatedEntity));
            }

            T originalEntity;

            try
            {
                originalEntity = _context.Set<T>().Where(entity => entity.Id == idUpdatedEntity).First();
            }
            catch 
            {
                throw new ArgumentException("Id was not found", nameof(idUpdatedEntity));
            }

            _entityHelper.CopyTo(updatedEntity, originalEntity);

            _context.SaveChanges();
        }

        public void Delete(Guid idDeleteEntity)
        {
            T deleteEntity;

            try
            {
                deleteEntity = _context.Set<T>().Where(entity => entity.Id == idDeleteEntity).First();
            }
            catch
            {
                throw new ArgumentException("Id was not found", nameof(idDeleteEntity));
            }

            _context.Set<T>().Remove(deleteEntity);

            _context.SaveChanges();
        }

        public T Get(Guid idEntity)
        {
            var findUser = _context.Set<T>().Where(entity => entity.Id == idEntity).First();

            return findUser;
        }

        public List<T> GetAll()
        {
            List<T> entities = _context.Set<T>().ToList();
            return entities;
        }
    }
}
