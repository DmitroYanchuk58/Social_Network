using EntityDB = DAL.Entities.Entity;
using EntityDTO = BAL.DTOs.IEntity;

namespace BAL.Helpers.Interfaces
{
<<<<<<< HEAD:BAL/Helpers/Interfaces/IConverter.cs
    public interface IConverter<out T, in S> 
=======
    public interface IConverterFromDtoToDb<T, S> 
>>>>>>> 95af41002ed13c6ab7d44c8f00595d4f730654fd:BAL/Helpers/Interfaces/IConverterFromDtoToDb.cs
        where T : EntityDB
        where S : EntityDTO
    {
        public T Convert(S entity);
    }
}
