using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Entities
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity { }

    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="TKey">唯一标识的类型</typeparam>
    public interface IEntity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        TKey Id { get; set; }
    }
}
