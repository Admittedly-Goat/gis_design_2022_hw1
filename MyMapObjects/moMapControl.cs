using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyMapObjects
{
    public partial class moMapControl : UserControl
    {

        #region 字段

        //（1）设计时属性变量
        //绘制选择图形的颜色
        //绘制闪烁图形的颜色
        //（2）运行时属性变量
        //图层集合
        private readonly moLayers _Layers = new moLayers();
        //坐标系统
        private moProjectionCS _ProjectionCS;
        //（3）模块级变量
        //地图绘制参考对象
        private moMapDrawingReference mMapDrawingReference;
        //缓冲位图，仅含所有要素（含注记）
        private Bitmap mBufferMap1 = new Bitmap(10, 10);
        //缓冲位图，含所有要素（含注记）及跟踪图形
        private Bitmap mBufferMap2 = new Bitmap(10, 10);
        //在屏幕上移动BufferMap2时，先将2绘制到3上，再
        //将3绘制到屏幕上，以避免清除屏幕带来的闪烁
        private Bitmap mBufferMap3 = new Bitmap(10, 10);
        //选择点符号
        private moSimpleMarkerSymbol mSelectedPointSymbol;
        //选择线符号
        private moSimpleLineSymbol mSelectedLineSymbol;
        //选择面符号
        private moSimpleFillSymbol mSelectedFillSymbol;
        //闪烁控制器
        private readonly moShapeFlashControler mFlashControl =
            new moShapeFlashControler();
        //闪烁点符号
        private moSimpleMarkerSymbol mFlashPointSymbol;
        //闪烁线符号
        private moSimpleLineSymbol mFlashLineSymbol;
        //闪烁面符号
        private moSimpleFillSymbol mFlashFillSymbol;

        #endregion

        #region 构造函数

        public moMapControl()
        {
            InitializeComponent();
            //创建默认投影坐标系统
            CreateDefaultProjectionCS();
            //新建地图绘制参考对象
            CreateMapDrawingReference();
            //调整缓冲位图的尺寸
            ResizeBufferMap();
            //初始化选择和闪烁图形的符号
            InitailizeSymbols();
            //加入闪烁控制器事件
            mFlashControl.NeedClearFlashShapes
                += MFlashControl_NeedClearFlashShapes;
            mFlashControl.NeedDrawFlashShapes
                += MFlashControl_NeedDrawFlashShapes;
        }



        #endregion

        #region 属性
        //UI是否已经开始重新读取Layer数据。防止不停的触发LayerChanged事件，造成死循环
        public bool isAlreadyRedrawLayer = false;

        /// <summary>
        /// 获取或设置绘制选择要素的颜色
        /// </summary>
        [Browsable(true), Description("获取或设置绘制选择要素的颜色")]
        public Color SelectionColor { get; set; } = Color.Cyan;

        /// <summary>
        /// 获取或设置闪烁图形的颜色
        /// </summary>
        [Browsable(true), Description("获取或设置闪烁图形的颜色")]
        public Color FlashColor { get; set; } = Color.Green;

        /// <summary>
        /// 获取或设置投影坐标系统
        /// </summary>
        [Browsable(false)]
        public moProjectionCS ProjectionCS
        {
            get => _ProjectionCS;
            set
            {
                _ProjectionCS = value;
                mMapDrawingReference.mpu =
                    _ProjectionCS.ToMeters(1);
            }
        }

        /// <summary>
        /// 获取图层集合
        /// </summary>
        [Browsable(false)]
        public moLayers Layers
        {
            get
            {
                RefreshLayerList();
                return _Layers;
            }
        }

        /// <summary>
        /// 获取地图比例尺倒数
        /// </summary>
        public double MapScale => mMapDrawingReference.MapScale;

        /// <summary>
        /// 获取地图控件左上点的X坐标
        /// </summary>
        public double MapOffsetX => mMapDrawingReference.OffsetX;

        /// <summary>
        /// 获取地图控件左上点Y坐标
        /// </summary>
        public double MapOffsetY => mMapDrawingReference.OffsetY;

        #endregion

        #region 方法

        /// <summary>
        /// 获取地图窗口对应的地图范围（地图坐标）
        /// </summary>
        /// <returns></returns>
        public moRectangle GetExtent()
        {
            //定义变量
            double sMinX = double.MaxValue, sMaxX = double.MinValue;
            double sMinY = double.MaxValue, sMaxY = double.MinValue;
            moRectangle sExtent;
            //如果工作区为空，则返回空矩形
            Rectangle sClientRect = ClientRectangle;
            if (sClientRect.IsEmpty == true)
            {
                sExtent = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
                return sExtent;
            }
            //定义工作区左上点和右下点的屏幕坐标
            moPoint sTopLeftScreenPoint = new moPoint(0, 0);
            moPoint sBottomRightScreenPoint = new moPoint(sClientRect.Width, sClientRect.Height);
            //获取工作区左上点和右下点的地图坐标
            moPoint sTopLeftMapPoint = mMapDrawingReference.ToMapPoint(sTopLeftScreenPoint.X, sTopLeftScreenPoint.Y);
            moPoint sBottomRightMapPoint = mMapDrawingReference.ToMapPoint(sBottomRightScreenPoint.X, sBottomRightScreenPoint.Y);
            //定义范围矩形
            sMinX = sTopLeftMapPoint.X;
            sMaxX = sBottomRightMapPoint.X;
            sMinY = sBottomRightMapPoint.Y;
            sMaxY = sTopLeftMapPoint.Y;
            sExtent = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sExtent;
        }

        /// <summary>
        /// 获取地图范围（地图坐标）
        /// </summary>
        /// <returns></returns>
        public moRectangle GetFullExtent()
        {
            //（1）新建一个空矩形
            double sMinX = double.MaxValue, sMaxX = double.MinValue;
            double sMinY = double.MaxValue, sMaxY = double.MinValue;
            moRectangle sFullExtent;
            //（2）如果图层数量为0，则返回空矩形
            int sLayerCount = _Layers.Count;
            if (sLayerCount == 0)
            {
                sFullExtent = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
                return sFullExtent;
            }
            //（3）计算范围矩形
            for (int i = 0; i <= sLayerCount - 1; i++)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true)
                {
                    moRectangle sExtent = sLayer.Extent;
                    if (sExtent.IsEmpty == false)
                    {
                        if (sExtent.MinX < sMinX)
                        {
                            sMinX = sExtent.MinX;
                        }

                        if (sExtent.MaxX > sMaxX)
                        {
                            sMaxX = sExtent.MaxX;
                        }

                        if (sExtent.MinY < sMinY)
                        {
                            sMinY = sExtent.MinY;
                        }

                        if (sExtent.MaxY > sMaxY)
                        {
                            sMaxY = sExtent.MaxY;
                        }
                    }
                }
            }
            sFullExtent = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sFullExtent;
        }

        /// <summary>
        /// 在窗口内显示地图全部范围
        /// </summary>
        public void FullExtent()
        {
            moRectangle sFullExtent = GetFullExtent();
            if (sFullExtent.IsEmpty == false)
            {
                Rectangle sClientRect = ClientRectangle;
                mMapDrawingReference.ZoomExtentToWindow(sFullExtent,
                    sClientRect.Width, sClientRect.Height);
                UseWaitCursor = true;
                DrawBufferMap1();
                DrawBufferMap2();
                UseWaitCursor = false;
                Refresh();
                //触发事件
                MapScaleChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// 设置地图视图
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="mapScale"></param>
        public void SetView(double offsetX,
            double offsetY, double mapScale)
        {
            mMapDrawingReference.SetView(offsetX, offsetY, mapScale);
        }

        /// <summary>
        /// 以指定中心和指定系数进行缩放
        /// </summary>
        /// <param name="center"></param>
        /// <param name="ratio"></param>
        public void ZoomByCenter(moPoint center, double ratio)
        {
            mMapDrawingReference.ZoomByCenter(center, ratio);
            UseWaitCursor = true;
            DrawBufferMap1();
            DrawBufferMap2();
            UseWaitCursor = false;
            Refresh();
            //触发事件
            MapScaleChanged?.Invoke(this);
        }

        /// <summary>
        /// 在地图窗口内显示指定范围
        /// </summary>
        /// <param name="extent"></param>
        public void ZoomToExtent(moRectangle extent)
        {
            double sWindowWidth = ClientRectangle.Width;
            double sWindowHeight = ClientRectangle.Height;
            mMapDrawingReference.ZoomExtentToWindow(extent, sWindowWidth, sWindowHeight);
            UseWaitCursor = true;
            DrawBufferMap1();
            DrawBufferMap2();
            UseWaitCursor = false;
            Refresh();
            //触发事件
            MapScaleChanged?.Invoke(this);
        }

        /// <summary>
        /// 将地图平移指定量
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void PanDelta(double deltaX, double deltaY)
        {
            mMapDrawingReference.PanDelta(deltaX, deltaY);
            UseWaitCursor = true;
            DrawBufferMap1();
            DrawBufferMap2();
            UseWaitCursor = false;
            Refresh();
        }

        /// <summary>
        /// 将屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public moPoint ToMapPoint(double x, double y)
        {
            moPoint sPoint = mMapDrawingReference.ToMapPoint(x, y);
            return sPoint;
        }

        /// <summary>
        /// 将地图坐标转换为屏幕坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public moPoint FromMapPoint(double x, double y)
        {
            moPoint sPoint = mMapDrawingReference.FromMapPoint(x, y);
            return sPoint;
        }

        /// <summary>
        /// 将屏幕距离转换为地图距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double ToMapDistance(double dis)
        {
            double sDis = mMapDrawingReference.ToMapDistance(dis);
            return sDis;
        }

        /// <summary>
        /// 将地图距离转换为屏幕距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double FromMapDistance(double dis)
        {
            double sDis = mMapDrawingReference.FromMapDistance(dis);
            return sDis;
        }

        /// <summary>
        /// 重新绘制地图
        /// </summary>
        public void RedrawMap()
        {
            UseWaitCursor = true;
            DrawBufferMap1();
            DrawBufferMap2();
            UseWaitCursor = false;
            Refresh();
        }

        /// <summary>
        /// 重新绘制跟踪图形
        /// </summary>
        public void RedrawTrackingShapes()
        {
            UseWaitCursor = true;
            DrawBufferMap2();
            UseWaitCursor = false;
            Refresh();
        }

        /// <summary>
        /// 将地图图像移动到指定位置(屏幕坐标)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PanMapImageTo(float x, float y)
        {
            Graphics g = Graphics.FromImage(mBufferMap3);
            g.Clear(Color.White);
            g.DrawImage(mBufferMap2, x, y);
            g = Graphics.FromHwnd(Handle);
            g.DrawImage(mBufferMap3, 0, 0);
            g.Dispose();
        }

        /// <summary>
        /// 对指定的图形数组，以指定次数和时间间隔进行闪烁显示
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="times"></param>
        /// <param name="interval"></param>
        public void FlashShapes(moShape[] shapes,
            int times, int interval)
        {
            mFlashControl.StartFlash(shapes, times, interval);
        }

        /// <summary>
        /// 获取一个绘图工具
        /// </summary>
        /// <returns></returns>
        public moUserDrawingTool GetDrawingTool()
        {
            Graphics g = Graphics.FromHwnd(Handle);
            moUserDrawingTool sDrawingTool = CreateDrawingTool(g);
            return sDrawingTool;
        }

        /// <summary>
        /// 根据指定选择盒与选择方法执行选择
        /// </summary>
        /// <param name="selectingBox"></param>
        /// <param name="tolerance"></param>
        /// <param name="selectMethod"></param>
        public void SelectByBox(moRectangle selectingBox,
            double tolerance, int selectMethod)
        {
            int sLayerCount = _Layers.Count;
            for (int i = 0; i <= sLayerCount - 1; i++)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true &&
                    sLayer.Selectable == true)
                {
                    moFeatures sFeatures = sLayer.SearchByBox
                        (selectingBox, tolerance);
                    sLayer.ExecuteSelect(sFeatures, 0);
                }
                else
                {
                    sLayer.SelectedFeatures.Clear();
                }
            }
        }

        public void RefreshLayerList()
        {
            if ((isAlreadyRedrawLayer == false) && (LayerChanged != null))
            {
                LayerChanged(this);
            }
        }
        #endregion

        #region 事件

        public delegate void MapScaleChangedHandle(object sender);
        /// <summary>
        /// 地图比例尺发生了变化
        /// </summary>
        [Browsable(true), Description("地图比例尺发生了变化")]
        public event MapScaleChangedHandle MapScaleChanged;

        public delegate void LayerChangedHandle(object sender);
        /// <summary>
        /// 图层发生了变化
        /// </summary>
        [Browsable(true), Description("图层发生了变化")]
        public event MapScaleChangedHandle LayerChanged;

        public delegate void AfterTrackingLayerDrawHandle(object sender,
            moUserDrawingTool drawingTool);
        /// <summary>
        /// 地图跟踪图形已经绘制完毕
        /// </summary>
        [Browsable(true), Description("地图跟踪图形已经绘制完毕")]
        public event AfterTrackingLayerDrawHandle
            AfterTrackingLayerDraw;

        #endregion

        #region 母版和对象的事件处理
        //控件重绘
        private void moMapControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mBufferMap2, e.ClipRectangle,
                e.ClipRectangle, GraphicsUnit.Pixel);
        }
        //控件大小调整
        private void moMapControl_Resize(object sender, EventArgs e)
        {
            ResizeBufferMap();
            RedrawMap();
        }
        //需要绘制闪烁图形
        private void MFlashControl_NeedDrawFlashShapes(object sender, moShape[] shapes)
        {
            double sMapScale = mMapDrawingReference.MapScale;
            double dpm = mMapDrawingReference.dpm;
            double mpu = mMapDrawingReference.mpu;
            moRectangle sExtent = GetExtent();
            Graphics g = Graphics.FromImage(mBufferMap2);
            int sShapeCount = shapes.Length;
            for (int i = 0; i <= sShapeCount - 1; i++)
            {
                if (shapes[i].GetType() == typeof(moPoint))
                {
                    moPoint sPoint = (moPoint)shapes[i];
                    moMapDrawingTools.DrawPoint(g, sExtent, sMapScale, dpm, mpu, sPoint, mFlashPointSymbol);
                }
                //else if (shapes[i].GetType() == typeof(moPoints))
                //{
                //    moPoints sPoints = (moPoints)shapes[i];
                //    moMapDrawingTools.DrawPoints(g, sExtent, sMapScale, dpm, mpu, sPoints, mFlashPointSymbol);
                //}
                else if (shapes[i].GetType() == typeof(moRectangle))
                {
                    moRectangle sRect = (moRectangle)shapes[i];
                    moMapDrawingTools.DrawRectangle(g, sExtent, sMapScale, dpm, mpu, sRect, mFlashFillSymbol);
                }
                else if (shapes[i].GetType() == typeof(moMultiPolyline))
                {
                    moMultiPolyline sMultiPolyline = (moMultiPolyline)shapes[i];
                    moMapDrawingTools.DrawMultiPolyline(g, sExtent, sMapScale, dpm, mpu, sMultiPolyline, mFlashLineSymbol);
                }
                else if (shapes[i].GetType() == typeof(moMultiPolygon))
                {
                    moMultiPolygon sMultiPolygon = (moMultiPolygon)shapes[i];
                    moMapDrawingTools.DrawMultiPolygon(g, sExtent, sMapScale, dpm, mpu, sMultiPolygon, mFlashFillSymbol);
                }
            }
            g.Dispose();
            Refresh();
        }
        //需要清除闪烁图形
        private void MFlashControl_NeedClearFlashShapes(object sender)
        {
            RedrawTrackingShapes();
        }
        #endregion

        #region 私有函数

        //创建默认投影坐标系统，更改为WGS1984坐标系
        private void CreateDefaultProjectionCS()
        {
            string sProjCSName = "WGS_1984";
            string sGeoCSName = "WGS_1984";
            string sDatumName = "WGS_1984";
            string sSpheroidName = "Krassowsky_1940";
            double sSemiMajor = 6378137;
            double sInverseFlattening = 298.257;
            double sOriginLatitude = 0;
            double sCentralMeridian = 0;
            double sFalseEasting = 0;
            double sFalseNorthing = 0;
            double sScaleFactor = 1;
            double sStandardParallelOne = 0;
            double sStandardParallelTwo = 0;
            moLinearUnitConstant sLinearUnit = moLinearUnitConstant.Meter;
            moProjectionTypeConstant sProjType = moProjectionTypeConstant.None;
            _ProjectionCS = new moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);

            /*string sProjCSName = "Beijing54 Lambert Conformal Conic 2SP";
            string sGeoCSName = "Beijing 1954";
            string sDatumName = "Beijing 1954";
            string sSpheroidName = "Krassowsky_1940";
            double sSemiMajor = 6378245;
            double sInverseFlattening = 298.3;
            double sOriginLatitude = 0;
            double sCentralMeridian = 105;
            double sFalseEasting = 0;
            double sFalseNorthing = 0;
            double sScaleFactor = 1;
            double sStandardParallelOne = 30;
            double sStandardParallelTwo = 62;
            moLinearUnitConstant sLinearUnit = moLinearUnitConstant.Meter;
            moProjectionTypeConstant sProjType = moProjectionTypeConstant.Lambert_Conformal_Conic_2SP;
            _ProjectionCS = new moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);*/
        }

        //新建地图绘制参考对象
        private void CreateMapDrawingReference()
        {
            Graphics g = Graphics.FromHwnd(Handle);
            double dpm = g.DpiX / 0.0254;
            g.Dispose();
            double mpu = _ProjectionCS.ToMeters(1);
            mMapDrawingReference = new moMapDrawingReference(0, 0, 1000000, dpm, mpu);
        }

        //调整缓冲位图尺寸
        private void ResizeBufferMap()
        {
            Rectangle sClientRectangle = ClientRectangle;
            if (sClientRectangle.Width > 0 && sClientRectangle.Height > 0)
            {
                if (sClientRectangle.Width != mBufferMap1.Width || sClientRectangle.Height != mBufferMap1.Height)
                {
                    mBufferMap1 = new Bitmap(sClientRectangle.Width, sClientRectangle.Height);
                    mBufferMap2 = new Bitmap(sClientRectangle.Width, sClientRectangle.Height);
                    mBufferMap3 = new Bitmap(sClientRectangle.Width, sClientRectangle.Height);
                }
            }
        }

        //初始化选择和闪烁图形符号
        private void InitailizeSymbols()
        {
            //选择点符号
            mSelectedPointSymbol = new moSimpleMarkerSymbol
            {
                Color = SelectionColor,
                Size = 3
            };
            //选择线符号
            mSelectedLineSymbol = new moSimpleLineSymbol
            {
                Color = SelectionColor,
                Size = 3000 / mMapDrawingReference.dpm
            };
            //选择面符号
            mSelectedFillSymbol = new moSimpleFillSymbol
            {
                Color = Color.Transparent
            };
            mSelectedFillSymbol.Outline.Color = SelectionColor;
            mSelectedFillSymbol.Outline.Size = 3000 / mMapDrawingReference.dpm;
            //闪烁点符号
            mFlashPointSymbol = new moSimpleMarkerSymbol
            {
                Color = FlashColor,
                Style = moSimpleMarkerSymbolStyleConstant.SolidCircle,
                Size = 3
            };
            //闪烁线符号
            mFlashLineSymbol = new moSimpleLineSymbol
            {
                Color = FlashColor,
                Size = 1.5
            };
            //闪烁面符号
            mFlashFillSymbol = new moSimpleFillSymbol
            {
                Color = FlashColor
            };
            mFlashFillSymbol.Outline.Color = Color.Black;
            mFlashFillSymbol.Outline.Size = 0.35;
        }

        //绘制缓冲位图1
        private void DrawBufferMap1()
        {
            //（1）获取地图窗口的范围
            moRectangle sExtent = GetExtent();
            if (sExtent.IsEmpty == true)
            {
                return;
            }
            //（2）绘制所有图层的要素，采用倒序
            double sMapScale = mMapDrawingReference.MapScale;
            double dpm = mMapDrawingReference.dpm;
            double mpu = mMapDrawingReference.mpu;
            Graphics g = Graphics.FromImage(mBufferMap1);
            g.Clear(Color.White);
            int sLayerCount = _Layers.Count;
            for (int i = sLayerCount - 1; i >= 0; i--)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true)
                {
                    sLayer.DrawFeatures(g, sExtent, sMapScale, dpm, mpu);
                }
            }
            //（3）绘制所有图层的注记，依然倒序
            List<RectangleF> sPlacedLabelExtents = new List<RectangleF>();
            for (int i = sLayerCount - 1; i >= 0; i--)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true)
                {
                    sLayer.DrawLabels(g, sExtent, sMapScale, dpm, mpu, sPlacedLabelExtents);
                }
            }
            g.Dispose();
        }

        //绘制缓冲位图2
        private void DrawBufferMap2()
        {
            //（1）获取地图窗口的范围
            moRectangle sExtent = GetExtent();
            if (sExtent.IsEmpty == true)
            {
                return;
            }
            //（2）绘制缓冲位图1
            Graphics g = Graphics.FromImage(mBufferMap2);
            g.Clear(Color.White);
            Rectangle sRect = new Rectangle(0, 0, mBufferMap1.Width, mBufferMap1.Height);
            g.DrawImage(mBufferMap1, sRect, sRect, GraphicsUnit.Pixel);
            //（3）绘制所有图层的选择要素，采用倒序
            double sMapScale = mMapDrawingReference.MapScale;
            double dpm = mMapDrawingReference.dpm;
            double mpu = mMapDrawingReference.mpu;
            int sLayerCount = _Layers.Count;
            for (int i = sLayerCount - 1; i >= 0; i--)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.ShapeType == moGeometryTypeConstant.Point)
                {
                    sLayer.DrawSelectedFeatures(g, sExtent, sMapScale, dpm, mpu, mSelectedPointSymbol);
                }
                else if (sLayer.ShapeType == moGeometryTypeConstant.MultiPolyline)
                {
                    sLayer.DrawSelectedFeatures(g, sExtent, sMapScale, dpm, mpu, mSelectedLineSymbol);
                }
                else if (sLayer.ShapeType == moGeometryTypeConstant.MultiPolygon)
                {
                    sLayer.DrawSelectedFeatures(g, sExtent, sMapScale, dpm, mpu, mSelectedFillSymbol);
                }
            }
            //（4）触发事件，以便用户程序继续绘图
            if (AfterTrackingLayerDraw != null)
            {
                //新建绘图工具
                moUserDrawingTool sDrawingTool = CreateDrawingTool(g);
                AfterTrackingLayerDraw(this, sDrawingTool);
            }
            g.Dispose();
        }

        //生成一个用户绘图工具
        private moUserDrawingTool CreateDrawingTool
            (Graphics graphics)
        {
            moRectangle sExtent = GetExtent();
            double sMapScale = mMapDrawingReference.MapScale;
            double dpm = mMapDrawingReference.dpm;
            double mpu = mMapDrawingReference.mpu;
            moUserDrawingTool sDrawingTool = new moUserDrawingTool
                (graphics, sExtent, sMapScale, dpm, mpu);
            return sDrawingTool;

        }

        #endregion

        public Bitmap DrawCurrentExtentWithScale(double scale)
        {
            //（1）获取地图窗口的范围
            moRectangle sExtent = GetExtent();
            if (sExtent.IsEmpty == true)
            {
                _ = MessageBox.Show("地图范围为空，请重试。");
                throw new Exception("地图范围为空");
            }
            //（2）绘制所有图层的要素，采用倒序
            double sMapScale = mMapDrawingReference.MapScale;
            double dpm = mMapDrawingReference.dpm * scale;
            double mpu = mMapDrawingReference.mpu;
            Bitmap exportBitmap = new Bitmap(Convert.ToInt32(scale * mBufferMap1.Width), Convert.ToInt32(scale * mBufferMap1.Height));
            Graphics g = Graphics.FromImage(exportBitmap);
            g.Clear(Color.White);
            int sLayerCount = _Layers.Count;
            for (int i = sLayerCount - 1; i >= 0; i--)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true)
                {
                    sLayer.DrawFeatures(g, sExtent, sMapScale, dpm, mpu);
                }
            }
            //（3）绘制所有图层的注记，依然倒序
            List<RectangleF> sPlacedLabelExtents = new List<RectangleF>();
            for (int i = sLayerCount - 1; i >= 0; i--)
            {
                moMapLayer sLayer = _Layers.GetItem(i);
                if (sLayer.Visible == true)
                {
                    sLayer.DrawLabels(g, sExtent, sMapScale, dpm, mpu, sPlacedLabelExtents);
                }
            }
            g.Dispose();
            return exportBitmap;
        }

    }
}
