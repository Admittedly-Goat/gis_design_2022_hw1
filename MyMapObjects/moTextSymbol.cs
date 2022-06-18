using System.Drawing;

namespace MyMapObjects
{
    /// <summary>
    /// 文本符号类型
    /// </summary>
    public class moTextSymbol
    {
        #region 字段
        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置字体
        /// </summary>
        public Font Font { get; set; } = new Font("微软雅黑", 8);

        /// <summary>
        /// 获取或设置字体颜色
        /// </summary>
        public Color FontColor { get; set; } = Color.Black;

        /// <summary>
        /// 获取或设置布局
        /// </summary>
        public moTextSymbolAlignmentConstant Alignment { get; set; } = moTextSymbolAlignmentConstant.CenterCenter;

        /// <summary>
        /// 获取或设置X方向偏移量，向右为正
        /// </summary>
        public double OffsetX { get; set; }

        /// <summary>
        /// 获取或设置Y方向偏移量，向上为正
        /// </summary>
        public double OffsetY { get; set; }

        /// <summary>
        /// 指示是否描边
        /// </summary>
        public bool UseMask { get; set; } = false;

        /// <summary>
        /// 获取或设置描边颜色
        /// </summary>
        public Color MaskColor { get; set; } = Color.White;

        /// <summary>
        /// 获取或设置描边宽度
        /// </summary>
        public double MaskWidth { get; set; } = 0.5;

        #endregion

        #region 方法

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public moTextSymbol Clone()
        {
            moTextSymbol sTextSymbol = new moTextSymbol
            {
                Font = (Font)Font.Clone(),
                FontColor = FontColor,
                Alignment = Alignment,
                OffsetX = OffsetX,
                OffsetY = OffsetY,
                UseMask = UseMask,
                MaskColor = MaskColor,
                MaskWidth = MaskWidth
            };
            return sTextSymbol;
        }
        #endregion
    }
}
