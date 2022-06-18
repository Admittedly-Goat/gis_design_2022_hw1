namespace MyMapObjects
{
    public class moFeature
    {
        #region 字段


        #endregion

        #region 构造函数

        public moFeature(moGeometryTypeConstant shapeType,
            moGeometry geometry, moAttributes attributes)
        {
            ShapeType = shapeType;
            Geometry = geometry;
            Attributes = attributes;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置几何类型
        /// </summary>
        public moGeometryTypeConstant ShapeType { get; set; } = moGeometryTypeConstant.MultiPolygon;

        /// <summary>
        /// 获取或设置几何图形
        /// </summary>
        public moGeometry Geometry { get; set; }

        /// <summary>
        /// 获取或设置属性集合
        /// </summary>
        public moAttributes Attributes { get; set; }

        /// <summary>
        /// 获取或设置要素符号
        /// </summary>
        internal moSymbol Symbol { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 获取要素的最小绑定矩形
        /// </summary>
        /// <returns></returns>
        public moRectangle GetEnvelope()
        {
            moRectangle sRect;
            if (ShapeType == moGeometryTypeConstant.Point)
            {

                if (Geometry.GetType() == typeof(moPoint))
                {
                    moPoint sPoint = (moPoint)Geometry;
                    sRect = new moRectangle(sPoint.X, sPoint.X,
                    sPoint.Y, sPoint.Y);
                }
                else
                {
                    moPoints sPoints = (moPoints)Geometry;
                    moPoint sPoint = sPoints.GetItem(0);
                    sRect = new moRectangle(sPoint.X, sPoint.X,
                    sPoint.Y, sPoint.Y);
                }

            }
            else if (ShapeType ==
                moGeometryTypeConstant.MultiPolyline)
            {
                moMultiPolyline sMultiPolyline
                    = (moMultiPolyline)Geometry;
                sRect = sMultiPolyline.GetEnvelope();
            }
            else
            {
                moMultiPolygon sMultiPolygon
                    = (moMultiPolygon)Geometry;
                sRect = sMultiPolygon.GetEnvelope();
            }
            return sRect;
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moFeature Clone()
        {
            moGeometryTypeConstant sShapeType = ShapeType;
            moGeometry sGeometry = null;
            moAttributes sAttributes = Attributes.Clone();
            if (ShapeType == moGeometryTypeConstant.Point)
            {
                moPoint sPoint = (moPoint)Geometry;
                sGeometry = sPoint.Clone();
            }
            else if (ShapeType == moGeometryTypeConstant.MultiPolyline)
            {
                moMultiPolyline sMultiPolyline = (moMultiPolyline)Geometry;
                sGeometry = sMultiPolyline.Clone();
            }
            else if (ShapeType == moGeometryTypeConstant.MultiPolygon)
            {
                moMultiPolygon sMultiPolygon = (moMultiPolygon)Geometry;
                sGeometry = sMultiPolygon.Clone();
            }
            moFeature sFeature = new moFeature(sShapeType, sGeometry, sAttributes);
            return sFeature;
        }

        #endregion

        #region 私有函数
        #endregion
    }
}
