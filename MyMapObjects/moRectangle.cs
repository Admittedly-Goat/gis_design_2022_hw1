namespace MyMapObjects
{
    public class moRectangle : moShape
    {
        #region 字段

        #endregion

        #region 构造函数

        public moRectangle(double minX, double maxX,
            double minY, double maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取最小X坐标
        /// </summary>
        public double MinX { get; }

        /// <summary>
        /// 获取最大X坐标
        /// </summary>
        public double MaxX { get; }

        /// <summary>
        /// 获取最小Y坐标
        /// </summary>
        public double MinY { get; }

        /// <summary>
        /// 获取最大Y坐标
        /// </summary>
        public double MaxY { get; }

        /// <summary>
        /// 获取宽度
        /// </summary>
        public double Width => MaxX - MinX;

        /// <summary>
        /// 获取高度
        /// </summary>
        public double Height => MaxY - MinY;

        /// <summary>
        /// 指示是否为空矩形
        /// </summary>
        public bool IsEmpty => MaxX <= MinX || MaxY <= MinY;

        #endregion
    }
}
