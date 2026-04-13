using AY.SmartEngine.Domain.Entities;
using System.Linq.Expressions;

namespace AY.SmartEngine.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        #region 新增操作

        /// <summary>
        /// 写入数据并返回新写入的实体（包含自增Id信息）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量写入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion 新增操作

        #region 删除操作

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量物理删除 (高效：直接生成 SQL DELETE)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 逻辑删除 (软删除)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        #endregion 删除操作

        #region 修改操作

        /// <summary>
        /// 全量更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /////领域层不引入EF组件，这里不专门指定为EF组件中的UpdateSettersBuilder
        ///// <summary>
        ///// 批量更新某些字段 (适配 EF Core 9/10 语法)(高效：直接生成 SQL UPDATE)
        ///// </summary>
        ///// <param name="predicate">过滤条件</param>
        ///// <param name="setPropertyCalls">更新字段的动作</param>
        ///// <example>
        ///// await repo.UpdateAsync(x => x.Id == 1, s => s.SetProperty(x => x.Name, "NewName"));
        ///// </example>
        //Task<int> UpdateAsync(
        //    Expression<Func<TEntity, bool>> predicate,
        //    Action<UpdateSettersBuilder<TEntity>> setPropertyCalls, // 改为 Action
        //    CancellationToken cancellationToken = default);

        #endregion 修改操作

        #region 查询操作

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> GetFirstAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取单条数据 (支持条件)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 基础条件查询 (List)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 条件查询并支持分页与排序
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(List<TEntity> Items, int Total)> GetPagedListAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool isDescending = true,
            CancellationToken cancellationToken = default);

        #endregion 查询操作

        #region 工具类方法

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default);

        #endregion 工具类方法
    }
}