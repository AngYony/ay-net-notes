using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    /// <summary>
    /// 普通可读写仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> :
        IVariableRepository<TEntity>,
        IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
    }


    /// <summary>
    /// 根据主键支持可读写操作的仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的唯一标识类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> :
        IRepository<TEntity>,
        IVariableRepository<TEntity, TPrimaryKey>,
        IReadOnlyRepository<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}
