using System.Drawing;

namespace MyMapObjects
{
    //用户绘图工具
    public class moUserDrawingTool
    {
        #region 字段
        #endregion

        #region 构造函数

        internal moUserDrawingTool(Graphics graphics, moRectangle extent, double mapScale, double dpm, double mpu)
        {
            MyGraphics = graphics;
            Extent = extent;
            MapScale = mapScale;
            this.dpm = dpm;
            this.mpu = mpu;
        }

        #endregion

        #region 属性

        internal Graphics MyGraphics { get; set; }

        internal moRectangle Extent { get; set; }

        internal double MapScale { get; } = 10000;

        internal double dpm { get; } = 96 / 0.0254;

        internal double mpu { get; } = 1.0;

        #endregion

        #region 方法


        /// <summary>
        /// 以指定符号绘制指定点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="symbol"></param>
        public void DrawPoint(moPoint point, moSymbol symbol)
        {
            moMapDrawingTools.DrawPoint(MyGraphics, Extent, MapScale, dpm, mpu, point, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定点集合
        /// </summary>
        /// <param name="points"></param>
        /// <param name="symbol"></param>
        public void DrawPoints(moPoints points, moSymbol symbol)
        {
            moMapDrawingTools.DrawPoints(MyGraphics, Extent, MapScale, dpm, mpu, points, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="symbol"></param>
        public void DrawRectangle(moRectangle rectangle, moSymbol symbol)
        {
            moMapDrawingTools.DrawRectangle(MyGraphics, Extent, MapScale, dpm, mpu, rectangle, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定线段
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="symbol"></param>
        public void DrawLine(moPoint point1, moPoint point2, moSymbol symbol)
        {
            moMapDrawingTools.DrawLine(MyGraphics, Extent, MapScale, dpm, mpu, point1, point2, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定折线
        /// </summary>
        /// <param name="points"></param>
        /// <param name="symbol"></param>
        public void DrawPolyline(moPoints points, moSymbol symbol)
        {
            moMapDrawingTools.DrawPolyline(MyGraphics, Extent, MapScale, dpm, mpu, points, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定多边形
        /// </summary>
        /// <param name="points"></param>
        /// <param name="symbol"></param>
        public void DrawPolygon(moPoints points, moSymbol symbol)
        {
            moMapDrawingTools.DrawPolygon(MyGraphics, Extent, MapScale, dpm, mpu, points, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定复合折线
        /// </summary>
        /// <param name="multiPolyline"></param>
        /// <param name="symbol"></param>
        public void DrawMultiPolyline(moMultiPolyline multiPolyline, moSymbol symbol)
        {
            moMapDrawingTools.DrawMultiPolyline(MyGraphics, Extent, MapScale, dpm, mpu, multiPolyline, symbol);
        }

        /// <summary>
        /// 以指定符号绘制指定复合多边形
        /// </summary>
        /// <param name="multiPolygon"></param>
        /// <param name="symbol"></param>
        public void DrawMultiPolygon(moMultiPolygon multiPolygon, moSymbol symbol)
        {
            moMapDrawingTools.DrawMultiPolygon(MyGraphics, Extent, MapScale, dpm, mpu, multiPolygon, symbol);
        }

        #endregion
    }
}
