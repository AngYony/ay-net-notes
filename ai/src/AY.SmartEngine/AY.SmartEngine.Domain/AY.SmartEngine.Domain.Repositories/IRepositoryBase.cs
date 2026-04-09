using AY.SmartEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();

    }
}
