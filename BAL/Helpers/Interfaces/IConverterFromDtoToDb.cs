using EntityDB = DAL.Entities.Entity;
using EntityDTO = BAL.DTOs.IEntity;

namespace BAL.Helpers.Interfaces
{
    public interface IConverterFromDtoToDb<T, S> 
        where T : EntityDB
        where S : EntityDTO
    {
        public T Convert(S entity);
    }
}
