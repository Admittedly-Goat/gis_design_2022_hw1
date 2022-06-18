namespace MyMapObjects
{
    /// <summary>
    /// 注记渲染类型
    /// </summary>
    public class moLabelRenderer
    {
        #region 字段


        #endregion

        #region 属性

        /// <summary>
        /// 指示是否为图层配置注记
        /// </summary>
        public bool LabelFeatures { get; set; } = false;

        /// <summary>
        /// 获取或设置注记符号
        /// </summary>
        public moTextSymbol TextSymbol { get; set; } = new moTextSymbol();

        /// <summary>
        /// 获取或设置绑定字段
        /// </summary>
        public string Field { get; set; } = "";

        #endregion
    }
}
