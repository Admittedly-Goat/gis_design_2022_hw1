namespace MyMapObjects
{
    /// <summary>
    /// 简单渲染
    /// </summary>
    public class moSimpleRenderer : moRenderer
    {
        #region 字段
        #endregion

        #region 构造函数
        public moSimpleRenderer()
        { }
        #endregion

        #region 属性

        /// <summary>
        /// 获取渲染类型
        /// </summary>
        public override moRendererTypeConstant RendererType => moRendererTypeConstant.Simple;

        /// <summary>
        /// 获取或设置符号
        /// </summary>
        public moSymbol Symbol { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public override moRenderer Clone()
        {
            moSimpleRenderer sRenderer = new moSimpleRenderer
            {
                Symbol = Symbol.Clone()
            };
            return sRenderer;
        }

        #endregion

    }
}
