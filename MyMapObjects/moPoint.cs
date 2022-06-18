namespace MyMapObjects
{
    public class moPoint : moGeometry
    {
        #region 字段


        #endregion

        #region 构造函数

        public moPoint()
        { }

        /// <summary>
        /// 新建一个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public moPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置X坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 获取或设置Y坐标
        /// </summary>
        public double Y { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moPoint Clone()
        {
            moPoint sPoint = new moPoint(X, Y);
            return sPoint;
        }

        #endregion
    }
}
