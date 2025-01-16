using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Interfaces
{
    public interface IEntityHelper<T> where T : Entity
    {
        public T CopyTo(T entityWithChanges, T entityForChange);

        public bool IsEmpty(T entity);
    }
}
