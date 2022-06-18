namespace MyMapObjects
{
    public class moMultiPolygon : moGeometry
    {
        #region 字段


        #endregion

        #region 构造函数

        public moMultiPolygon()
        {
            Parts = new moParts();
        }

        public moMultiPolygon(moPoints points)
        {
            Parts = new moParts();
            Parts.Add(points);
        }

        public moMultiPolygon(moParts parts)
        {
            Parts = parts;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置部件集合
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
        /// 获取最小绑定矩形
        /// </summary>
        /// <returns></returns>
        public moRectangle GetEnvelope()
        {
            moRectangle sRect = new moRectangle(MinX,
                MaxX, MinY, MaxY);
            return sRect;
        }

        /// <summary>
        /// 更新范围
        /// </summary>
        public void UpdateExtent()
        {
            CalExtent();
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moMultiPolygon Clone()
        {
            moMultiPolygon sMultiPolygon = new moMultiPolygon
            {
                Parts = Parts.Clone(),
                MinX = MinX,
                MaxX = MaxX,
                MinY = MinY,
                MaxY = MaxY
            };
            return sMultiPolygon;
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
