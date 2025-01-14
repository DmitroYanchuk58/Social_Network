using DAL.DatabaseContextNamespace;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CrudRepository<T> where T : Entity<T>
    {
        private readonly DatabaseContext _context;

        public CrudRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Create(T newEntity)
        {
            if (newEntity == null || newEntity.IsEmpty())
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            _context.Set<T>().Add(newEntity);
            _context.SaveChanges();
        }

        public void Update(Guid idUpdatedEntity, T updatedEntity)
        {
            if (updatedEntity == null || updatedEntity.IsEmpty())
            {
                throw new ArgumentNullException(nameof(updatedEntity));
            }

            var originalEntity = _context.Set<T>().Where(entity => entity.Id == idUpdatedEntity).First();
            originalEntity.CopyTo(updatedEntity);
            _context.SaveChanges();
        }

        public void Delete(Guid idDeleteEntity)
        {
            var deleteEntity = _context.Set<T>().Where(entity => entity.Id == idDeleteEntity).First();

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
