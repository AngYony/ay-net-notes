using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Entities
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ILogicallyDeletable
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 乐观并发接口
    /// </summary>
    public interface IOptimisticConcurrencySupported
    {
        /// <summary>
        /// 行版本，乐观并发锁
        /// </summary>
        [ConcurrencyCheck]
        string ConcurrencyStamp { get; set; }
    }
}
