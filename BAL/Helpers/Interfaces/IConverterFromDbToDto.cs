﻿using EntityDB = DAL.Entities.Entity;
using EntityDTO = BAL.DTOs.IEntity;

namespace BAL.Helpers.Interfaces
{
    public interface IConverterFromDbToDto<in T, out S>
        where T : EntityDB
        where S : EntityDTO
    {
        public S Convert(T entity);
    }
}
