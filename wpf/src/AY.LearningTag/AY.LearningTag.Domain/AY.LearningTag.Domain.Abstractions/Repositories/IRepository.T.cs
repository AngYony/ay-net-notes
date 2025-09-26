using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    /// <summary>
    /// 可读写仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> : IVariableRepository<TEntity>, IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
    }

    /// <summary>
    /// 可读写仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体的唯一标识类型</typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IVariableRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
