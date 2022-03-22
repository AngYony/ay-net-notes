namespace IocDemo.Container
{
    /// <summary>
    /// 服务生命周期
    /// </summary>
    public enum MyServiceLife
    {
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient,
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,
        /// <summary>
        /// 作用域单例
        /// </summary>
        Scoped
    }
}