using DAL.DatabaseContextNamespace;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CRUD_Repository<T> where T : Entity
    {
        private readonly DatabaseContext _context;

        public CRUD_Repository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Create(T newEntity)
        {
            _context.Set<T>().Add(newEntity);
            _context.SaveChanges();
        }

    }
}
