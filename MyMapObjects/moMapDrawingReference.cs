namespace MyMapObjects
{
    //为地图显示、缩放、漫游定义的类，
    //实现地图坐标与屏幕坐标的转换
    internal class moMapDrawingReference
    {
        #region 字段

        private const double mcMaxMapScale = 10000000000;    //地图显示比例尺倒数的最大值,100亿
        private const double mcMinMapScale = 10;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="offsetX">绘图区域左上点在地图坐标系中的X坐标</param>
        /// <param name="offsetY">绘图区域左上点在地图坐标系中的Y坐标</param>
        /// <param name="mapScale">地图比例尺的倒数</param>
        /// <param name="dpm">每米代表的象素数</param>
        /// <param name="mpu">1个地图坐标单位代表的米数</param>
        internal moMapDrawingReference(double offsetX, double offsetY, double mapScale, double dpm, double mpu)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            MapScale = mapScale;
            this.dpm = dpm;
            this.mpu = mpu;
        }

        #endregion

        #region 属性

        internal double OffsetX { get; set; }

        internal double OffsetY { get; set; }

        internal double MapScale { get; set; } = 10000;

        internal double dpm { get; set; } = 96 / 0.0254;

        internal double mpu { get; set; } = 1.0;

        #endregion

        #region 方法

        //设置视图
        internal void SetView(double offsetX, double offsetY, double mapScale)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            MapScale = mapScale;
        }

        //以指定中心和指定系数进行缩放
        internal void ZoomByCenter(moPoint center, double ratio)
        {
            double sMapScale = MapScale / ratio;      //新的比例尺

            if (sMapScale > mcMaxMapScale)
            {
                sMapScale = mcMaxMapScale;
            }
            else if (sMapScale < mcMinMapScale)
            {
                sMapScale = mcMinMapScale;
            }

            double sRatio = MapScale / sMapScale;      //实际的缩放系数
            double sOffsetX = OffsetX + ((1 - (1 / sRatio)) * (center.X - OffsetX));
            double sOffsetY = OffsetY + ((1 - (1 / sRatio)) * (center.Y - OffsetY));
            OffsetX = sOffsetX;
            OffsetY = sOffsetY;
            MapScale = sMapScale;
        }

        //将指定范围缩放至指定大小的屏幕窗口
        internal void ZoomExtentToWindow(moRectangle rect, double windowWidth, double windowHeight)
        {
            double sRectWidth = rect.Width, sRectHeight = rect.Height;
            //计算宽高比例
            double sMapRatio = sRectWidth / sRectHeight;            //地图范围的宽高比
            double sWindowRatio = windowWidth / windowHeight;       //窗口的宽高比
            //计算缩放后比例尺
            double sMapScale;
            if (sMapRatio <= sWindowRatio)
            {
                //按照垂向充满窗体
                sMapScale = sRectHeight * mpu / windowHeight * dpm;
            }
            else
            {
                //按照横向充满窗体
                sMapScale = sRectWidth * mpu / windowWidth * dpm;
            }
            if (sMapScale > mcMaxMapScale)          //100亿
            {
                sMapScale = mcMaxMapScale;          //防止溢出
            }
            else if (sMapScale < mcMinMapScale)
            {
                sMapScale = mcMinMapScale;            //防止溢出
            }
            //计算偏移量
            double sOffsetX, sOffsetY;              //定义新的偏移量
            sOffsetX = ((rect.MinX + rect.MaxX) / 2) - (windowWidth / 2 / dpm * sMapScale / mpu);
            sOffsetY = ((rect.MinY + rect.MaxY) / 2) + (windowHeight / 2 / dpm * sMapScale / mpu);
            //赋值
            OffsetX = sOffsetX;
            OffsetY = sOffsetY;
            MapScale = sMapScale;
        }

        //将地图平移指定量
        internal void PanDelta(double deltaX, double deltaY)
        {
            OffsetX -= deltaX;
            OffsetY -= deltaY;
        }

        //将屏幕坐标转换为地图坐标
        internal moPoint ToMapPoint(double x, double y)
        {
            double sX = (x / dpm / mpu * MapScale) + OffsetX;
            double sY = OffsetY - (y / dpm / mpu * MapScale);
            moPoint sPoint = new moPoint(sX, sY);
            return sPoint;
        }

        //将地图坐标转换为屏幕坐标
        internal moPoint FromMapPoint(double x, double y)
        {
            double sX = (x - OffsetX) / MapScale * dpm * mpu;
            double sY = (OffsetY - y) / MapScale * dpm * mpu;
            moPoint sPoint = new moPoint(sX, sY);
            return sPoint;
        }

        //将屏幕距离转换为地图距离
        internal double ToMapDistance(double dis)
        {
            double sDis = dis * MapScale / mpu / dpm;
            return sDis;
        }

        //将地图距离转换为屏幕距离
        internal double FromMapDistance(double dis)
        {
            double sDis = dis / MapScale * dpm * mpu;
            return sDis;
        }
        #endregion
    }
}
