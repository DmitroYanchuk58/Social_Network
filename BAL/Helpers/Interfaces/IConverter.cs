using EntityDB = DAL.Entities.Entity;
using EntityDTO = BAL.DTOs.Entity;

namespace BAL.Helpers.Interfaces
{
    public interface IConverter<T, S> 
        where T : EntityDB
        where S : EntityDTO
    {
        public T Convert(S entity);
    }
}
