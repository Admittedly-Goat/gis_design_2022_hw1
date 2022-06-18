using System.Collections.Generic;

namespace MyMapObjects
{
    public class moPoints : moGeometry
    {
        #region 字段

        private readonly List<moPoint> _Points;  //点集合

        #endregion

        #region 构造函数

        public moPoints()
        {
            _Points = new List<moPoint>();
        }

        public moPoints(moPoint[] points)
        {
            _Points = new List<moPoint>();
            _Points.AddRange(points);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取点数目
        /// </summary>
        public int Count => _Points.Count;

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
        /// 获取指定索引号的点
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns></returns>
        public moPoint GetItem(int index)
        {
            return _Points[index];
        }

        /// <summary>
        /// 在末尾增加一个点
        /// </summary>
        /// <param name="point"></param>
        public void Add(moPoint point)
        {
            _Points.Add(point);
        }

        /// <summary>
        /// 将指定数组中的元素添加到末尾
        /// </summary>
        /// <param name="points"></param>
        public void AddRange(moPoint[] points)
        {
            _Points.AddRange(points);
        }

        /// <summary>
        /// 将指定数组中的元素插入到指定索引号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="points"></param>
        public void InsertRange(int index,
            moPoint[] points)
        {
            _Points.InsertRange(index, points);
        }

        /// <summary>
        /// 将指定元素插入到指定索引号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="point"></param>
        public void Insert(int index, moPoint point)
        {
            _Points.Insert(index, point);
        }

        /// <summary>
        /// 删除指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _Points.RemoveAt(index);
        }

        /// <summary>
        /// 将所有元素复制到一个新的数组
        /// </summary>
        /// <returns></returns>
        public moPoint[] ToArray()
        {
            return _Points.ToArray();
        }

        /// <summary>
        /// 删除所有元素
        /// </summary>
        public void Clear()
        {
            _Points.Clear();
        }

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
        public moPoints Clone()
        {
            moPoints sPoints = new moPoints();
            int sPointCount = _Points.Count;
            for (int i = 0; i <= sPointCount - 1; i++)
            {
                moPoint sPoint = new moPoint(_Points[i].X, _Points[i].Y);
                sPoints.Add(sPoint);
            }
            sPoints.MinX = MinX;
            sPoints.MaxX = MaxX;
            sPoints.MinY = MinY;
            sPoints.MaxY = MaxY;
            return sPoints;
        }

        #endregion

        #region 私有函数

        //计算范围
        private void CalExtent()
        {
            double sMinX = double.MaxValue;
            double sMaxX = double.MinValue;
            double sMinY = double.MaxValue;
            double sMaxY = double.MinValue;
            int sPointCount = _Points.Count;
            for (int i = 0; i <= sPointCount - 1; i++)
            {
                if (_Points[i].X < sMinX)
                {
                    sMinX = _Points[i].X;
                }

                if (_Points[i].X > sMaxX)
                {
                    sMaxX = _Points[i].X;
                }

                if (_Points[i].Y < sMinY)
                {
                    sMinY = _Points[i].Y;
                }

                if (_Points[i].Y > sMaxY)
                {
                    sMaxY = _Points[i].Y;
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
