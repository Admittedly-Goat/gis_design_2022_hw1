namespace MyMapObjects
{
    public class moMultiPolyline : moGeometry
    {

        #region 字段


        #endregion

        #region 构造函数

        public moMultiPolyline()
        {
            Parts = new moParts();
        }

        public moMultiPolyline(moPoints points)
        {
            Parts = new moParts();
            Parts.Add(points);
        }

        public moMultiPolyline(moParts parts)
        {
            Parts = parts;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置部分集合
        /// </summary>
        public moParts Parts { get; set; }

        /// <summary>
        /// 获取最小X坐标
        /// </summary>
        public double MinX { get; private set; } = double.MaxValue;

        /// <summary>
        /// 获取最大X坐标
        /// </summary>
        public double MaxX { get; private set; } = double.MinValue;

        /// <summary>
        /// 获取最小Y坐标
        /// </summary>
        public double MinY { get; private set; } = double.MaxValue;

        /// <summary>
        /// 获取最大Y坐标
        /// </summary>
        public double MaxY { get; private set; } = double.MinValue;

        #endregion

        #region 方法

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        /// <returns></returns>
        public moRectangle GetEnvelope()
        {
            moRectangle sRectangle = new moRectangle(MinX, MaxX, MinY, MaxY);
            return sRectangle;
        }

        /// <summary>
        /// 重新计算坐标范围
        /// </summary>
        public void UpdateExtent()
        {
            CalExtent();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public moMultiPolyline Clone()
        {
            moMultiPolyline sMultiPolyline = new moMultiPolyline
            {
                Parts = Parts.Clone(),
                MinX = MinX,
                MaxX = MaxX,
                MinY = MinY,
                MaxY = MaxY
            };
            return sMultiPolyline;
        }

        #endregion

        #region 私有函数

        //计算坐标范围
        private void CalExtent()
        {
            double sMinX = double.MaxValue, sMaxX = double.MinValue;
            double sMinY = double.MaxValue, sMaxY = double.MinValue;
            int sPartCount = Parts.Count;
            for (int i = 0; i <= sPartCount - 1; i++)
            {
                Parts.GetItem(i).UpdateExtent();
                if (Parts.GetItem(i).MinX < sMinX)
                {
                    sMinX = Parts.GetItem(i).MinX;
                }

                if (Parts.GetItem(i).MaxX > sMaxX)
                {
                    sMaxX = Parts.GetItem(i).MaxX;
                }

                if (Parts.GetItem(i).MinY < sMinY)
                {
                    sMinY = Parts.GetItem(i).MinY;
                }

                if (Parts.GetItem(i).MaxY > sMaxY)
                {
                    sMaxY = Parts.GetItem(i).MaxY;
                }
            }
            MinX = sMinX;
            MaxX = sMaxX;
            MinY = sMinY;
            MaxY = sMaxY;
        }

        #endregion
    }
}
