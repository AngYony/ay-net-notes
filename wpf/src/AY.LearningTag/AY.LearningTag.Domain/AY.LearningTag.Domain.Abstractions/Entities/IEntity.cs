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
    /// <typeparam name="TPrimaryKey">唯一标识的类型</typeparam>
    public interface IEntity<TPrimaryKey> : IEntity where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
