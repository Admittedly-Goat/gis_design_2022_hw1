using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class frmMain : Form
    {
        #region 字段
        // 选项变量
        private Color mZoomBoxColor = Color.DeepPink; // 缩放盒颜色
        private double mZoomBoxWidth = 0.53; // 缩放盒边界宽度，单位毫米
        private Color mSelectBoxColor = Color.DarkGreen; // 选择盒颜色
        private double mSelectBoxWidth = 0.53; // 选择盒边界宽度
        private double mZoomRatioFixed = 2; // 固定放大系数
        private double mZoomRatioMouseWheel = 1.2; // 滑轮放大系数
        private double mSelectingTolerance = 3; // 选择容限，像素
        private MyMapObjects.moSimpleFillSymbol mSelectingBoxSymbol; // 选择盒符号
        private MyMapObjects.moSimpleFillSymbol mZoomBoxSymbol; // 缩放盒符号
        private MyMapObjects.moSimpleFillSymbol mMovingPolygonSymbol; // 正在移动的多边形符号
        private MyMapObjects.moSimpleFillSymbol mEditingPolygonSymbol; // 正在编辑的多边形符号
        private MyMapObjects.moSimpleMarkerSymbol mEditingVertexSymbol; // 正在编辑的图形顶点符号
        private MyMapObjects.moSimpleLineSymbol mElasticSymbol; // 橡皮筋符号
        private bool mShowLngLat = false; // 是否显示经纬度

        // 与地图操作有关的变量
        private Int32 mMapOpStyle = 0; // 0：无，1：放大，2：缩小，3：漫游，4：选择，5：查询，6：移动，7：描绘，8：编辑
        private PointF mStartMouseLocation;
        private bool mIsInZoomIn = true;
        private bool mIsInPan = false;
        private bool mIsInSelect = false;
        private bool mIsInIdentify = false;
        private bool mIsInMovingShapes = false;
        private List<MyMapObjects.moGeometry> mMovingGeometries = new List<MyMapObjects.moGeometry>(); // 正在移动的图形的集合
        private MyMapObjects.moGeometry mEditingGeometry; // 正在编辑的图形
        private List<MyMapObjects.moPoints> mSketchingShape; // 正在描绘的图形，用一个多点集合存储

        #endregion

        #region 构造函数
        public frmMain()
        {
            InitializeComponent();
            moMap.MouseWheel += moMap_MouseWheel;
        }
        #endregion

        #region 窗体和控件事件处理
        private void lay文件课上实习格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog sDialog = new OpenFileDialog();
            string sFileName = "";
            if (sDialog.ShowDialog(this)
                == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }

            try
            {
                FileStream sStream =
                    new FileStream(sFileName, FileMode.Open);
                BinaryReader sr = new BinaryReader(sStream);
                MyMapObjects.moMapLayer sLayer =
                    DataIOTools.LoadMapLayer(sr);
                moMap.Layers.Add(sLayer);
                if (moMap.Layers.Count == 1)
                {
                    moMap.FullExtent();
                }
                else
                {
                    moMap.RedrawMap();
                }
                sr.Dispose();
                sStream.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            moMap.FullExtent(); //全范围显示
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 1;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 2;
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 3;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 4;
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 5;
        }

        private void btnSimpleRenderer_Click(object sender, EventArgs e)
        {
            //（1）查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
            {
                return;

            }
            //(2)新建一个简单渲染对象
            MyMapObjects.moSimpleRenderer sRenderer = new MyMapObjects.moSimpleRenderer();
            MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
            sRenderer.Symbol = sSymbol;
            sLayer.Renderer = sRenderer;
            moMap.RedrawMap();
        }

        private void btnUniqueValue_Click(object sender, EventArgs e)
        {
            //(1)查找一个多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
            {
                return;
            }
            //(2)假定第一个字段为名称并且为字符型，新建一个唯一值渲染对象
            MyMapObjects.moUniqueValueRenderer sRenderer = new MyMapObjects.moUniqueValueRenderer();
            sRenderer.Field = "名称";
            List<String> sNames = new List<string>();
            Int32 sFeatureCount = sLayer.Features.Count;
            for (Int32 i = 0; i <= sFeatureCount - 1; i++)
            {
                string sName = (string)sLayer.Features.GetItem(i).Attributes.GetItem(0);
                sNames.Add(sName);
            }
            sNames.Distinct().ToList();
            Int32 sValueCount = sNames.Count;
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                sRenderer.AddUniqueValue(sNames[i], sSymbol);
            }
            sRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            sLayer.Renderer = sRenderer;
            moMap.RedrawMap();
        }

        private void btnClassBreaks_Click(object sender, EventArgs e)
        {
            //查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
            {
                return;
            }
            //新建一个分级渲染对象，并假定图层中存在名称为“F5”的字段且为单精度浮点型
            MyMapObjects.moClassBreaksRenderer sRenderer = new MyMapObjects.moClassBreaksRenderer();
            sRenderer.Field = "F5";
            //读出所有值
            Int32 sFieldIndex = sLayer.AttributeFields.FindField(sRenderer.Field);
            if (sFieldIndex < 0)
            {
                return;
            }
            if (sLayer.AttributeFields.GetItem(sFieldIndex).ValueType != MyMapObjects.moValueTypeConstant.dSingle)
            {
                return;
            }
            Int32 sFeatureCount = sLayer.Features.Count;
            List<double> sValues = new List<double>();
            for (Int32 i = 0; i < sFeatureCount - 1; i++)
            {
                double sValue = (float)sLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                sValues.Add(sValue);
            }
            //获取最小最大值，并分五级
            double sMinValue = sValues.Min();
            double sMaxValue = sValues.Max();
            for (Int32 i = 0; i <= 4; i++)
            {
                double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / 5;
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                sRenderer.AddBreakValue(sValue, sSymbol);
            }
            //生成渐变色
            Color sStartColor = Color.FromArgb(255, 255, 192, 192);
            Color sEndColor = Color.Maroon;
            sRenderer.RampColor(sStartColor, sEndColor);
            //赋给图层
            sRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            sLayer.Renderer = sRenderer;
            //重绘地图
            moMap.RedrawMap();
        }

        private void btnShowLabel_Click(object sender, EventArgs e)
        {
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            //获取第一个图层
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(0);
            //新建一个注记渲染对象
            MyMapObjects.moLabelRenderer sLabelRenderer = new MyMapObjects.moLabelRenderer();
            //设置绑定字段为索引号为0的字段
            sLabelRenderer.Field = sLayer.AttributeFields.GetItem(0).Name;
            //设置注记符号
            Font sOldFont = sLabelRenderer.TextSymbol.Font;
            sLabelRenderer.TextSymbol.Font = new Font(sOldFont.Name, 12);
            sLabelRenderer.TextSymbol.UseMask = true;
            sLabelRenderer.LabelFeatures = true;
            //赋给图层
            sLayer.LabelRenderer = sLabelRenderer;
            //重绘地图
            moMap.RedrawMap();

        }

        private void btnMovePolygon_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 6;

        }

        private void btnSketchPolygon_Click(object sender, EventArgs e)
        {
            mMapOpStyle = 7;
        }

        private void btnEndPart_Click(object sender, EventArgs e)
        {
            //判断是否可以结束及是否最少三个点
            if (mSketchingShape.Last().Count < 3)
            {
                return;
            }
            //描绘图形中增加一个多点对象
            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
            mSketchingShape.Add(sPoints);
            //重绘跟踪层
            moMap.RedrawTrackingShapes();
        }

        private void btnEndSketch_Click(object sender, EventArgs e)
        {
            if ((mSketchingShape.Last().Count >= 1) && (mSketchingShape.Last().Count < 3))
            {
                return;
            }
            if (mSketchingShape.Last().Count == 0)
            {
                mSketchingShape.Remove(mSketchingShape.Last());

            }
            if (mSketchingShape.Count > 0)
            {
                MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
                if (sLayer != null)
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
                    sMultiPolygon.Parts.AddRange(mSketchingShape.ToArray());
                    sMultiPolygon.UpdateExtent();
                    MyMapObjects.moFeature sFeature = sLayer.GetNewFeature();
                    sFeature.Geometry = sMultiPolygon;
                    sLayer.Features.Add(sFeature);
                }
            }
            InitializeSketchingShape();
            moMap.RedrawMap();
        }

        private void btnEditPolygon_Click(object sender, EventArgs e)
        {
            //查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
            {
                return;
            }
            //是否有且只有一个选中的图形
            if (sLayer.SelectedFeatures.Count != 1)
            {
                return;
            }
            //复制
            MyMapObjects.moMultiPolygon sOriMultiPolygon = (MyMapObjects.moMultiPolygon)sLayer.SelectedFeatures.GetItem(0).Geometry;
            MyMapObjects.moMultiPolygon sDesMultiPolygon = sOriMultiPolygon.Clone();
            mEditingGeometry = sDesMultiPolygon;
            //设置操作类型
            mMapOpStyle = 8;
            //地图重回跟踪层
            moMap.RedrawTrackingShapes();
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            //修改数据，不在编辑
            //清除
            mEditingGeometry = null;
            moMap.RedrawMap();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //（1）初始化符号
            InitializeSymbols();
            //（2）初始化描绘图形
            InitializeSketchingShape();
            //（3）显示比例尺
            ShowMapScale();
        }

        private void chkShowLngLat_CheckedChanged(object sender, EventArgs e)
        {
            mShowLngLat = chkShowLngLat.Checked;
        }

        private void moMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) //放大
            {
                OnZoomIn_MouseDown(e);
            }
            else if (mMapOpStyle == 2) //缩小
            {

            }
            else if (mMapOpStyle == 3) //漫游
            {
                OnPan_MouseDown(e);
            }
            else if (mMapOpStyle == 4) //选择
            {
                OnSelect_MouseDown(e);
            }
            else if (mMapOpStyle == 5) //查询
            {
                OnIdentify_MouseDown(e);
            }
            else if (mMapOpStyle == 6) //移动
            {
                OnMovingShape_MouseDown(e);
            }
            else if (mMapOpStyle == 7) //描绘
            {

            }
            else if (mMapOpStyle == 8) //编辑
            {

            }
        }

        //移动图形状态下鼠标按下
        private void OnMovingShape_MouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
            {
                return;
            }
            //判断是否有选中的要素
            Int32 sSelFeatureCount = sLayer.SelectedFeatures.Count;
            if (sSelFeatureCount == 0)
            {
                return;
            }
            //复制图形
            mMovingGeometries.Clear();
            for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
            {
                MyMapObjects.moMultiPolygon sOriPolygon = (MyMapObjects.moMultiPolygon)sLayer.SelectedFeatures.GetItem(i).Geometry;
                MyMapObjects.moMultiPolygon sDesPolygon = sOriPolygon.Clone();
                mMovingGeometries.Add(sDesPolygon);
            }
            //设置变量
            mStartMouseLocation = e.Location;
            mIsInMovingShapes = true;
        }

        //查询状态下鼠标按下
        private void OnIdentify_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInIdentify = true;
            }
        }

        //选择状态下鼠标按下
        private void OnSelect_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInSelect = true;
            }
        }

        //漫游状态下鼠标按下
        private void OnPan_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInPan = true;
            }
        }

        private void OnZoomIn_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInZoomIn = true;

            }
        }

        private void moMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) //放大
            {
                OnZoomIn_MouseUp(e);
            }
            else if (mMapOpStyle == 2) //缩小
            {

            }
            else if (mMapOpStyle == 3) //漫游
            {
                OnPan_MouseUp(e);
            }
            else if (mMapOpStyle == 4) //选择
            {
                OnSelect_MouseUp(e);
            }
            else if (mMapOpStyle == 5) //查询
            {
                OnIdentify_MouseUp(e);
            }
            else if (mMapOpStyle == 6) //移动
            {
                OnMoveShape_MouseUp(e);
            }
            else if (mMapOpStyle == 7) //描绘
            {

            }
            else if (mMapOpStyle == 8) //编辑
            {

            }
        }

        //移动图形状态下鼠标松开
        private void OnMoveShape_MouseUp(MouseEventArgs e)
        {
            if (mIsInMovingShapes == false)
            {
                return;
            }
            mIsInMovingShapes = false;
            //作相应的数据修改，不再编写
            //重绘地图
            moMap.RedrawMap();
            //清除移动图形列表
            mMovingGeometries.Clear();
        }

        private void OnIdentify_MouseUp(MouseEventArgs e)
        {
            if (mIsInIdentify == false)
            {
                return;
            }
            mIsInIdentify = false;
            moMap.Refresh();
            MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double sTolerance = moMap.ToMapDistance(mSelectingTolerance);
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            else
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(0); //获得第一个图层
                MyMapObjects.moFeatures sFeatures = sLayer.SearchByBox(sBox, sTolerance);
                Int32 sSelFeatureCount = sFeatures.Count;
                if (sSelFeatureCount > 0)
                {
                    MyMapObjects.moGeometry[] sGeometries = new MyMapObjects.moGeometry[sSelFeatureCount];
                    for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                    {
                        sGeometries[i] = sFeatures.GetItem(i).Geometry;
                    }
                    moMap.FlashShapes(sGeometries, 3, 800);
                }
            }
        }

        //选择状态下鼠标松开
        private void OnSelect_MouseUp(MouseEventArgs e)
        {
            if (mIsInSelect == false)
            {
                return;
            }
            mIsInSelect = false;
            MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double sTolerance = moMap.ToMapDistance(mSelectingTolerance);
            moMap.SelectByBox(sBox, sTolerance, 0);
            moMap.RedrawTrackingShapes();
        }

        //漫游状态下鼠标松开
        private void OnPan_MouseUp(MouseEventArgs e)
        {
            if (mIsInPan == false)
            {
                return;
            }
            mIsInPan = false;
            double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            moMap.PanDelta(sDeltaX, sDeltaY);
        }

        //放大状态下鼠标松开
        private void OnZoomIn_MouseUp(MouseEventArgs e)
        {
            if (mIsInZoomIn == false)
            {
                return;
            }
            mIsInZoomIn = false;
            if (mStartMouseLocation.X == e.Location.X && mStartMouseLocation.Y == e.Location.Y)
            {
                //单点放大
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                moMap.ZoomByCenter(sPoint, mZoomRatioFixed);
            }
            else
            {
                //矩形放大
                MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                moMap.ZoomToExtent(sBox);
            }
        }

        private void moMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) //放大
            {

            }
            else if (mMapOpStyle == 2) //缩小
            {
                OnZoomOut_MouseClick(e);
            }
            else if (mMapOpStyle == 3) //漫游
            {

            }
            else if (mMapOpStyle == 4) //选择
            {

            }
            else if (mMapOpStyle == 5) //查询
            {

            }
            else if (mMapOpStyle == 6) //移动
            {

            }
            else if (mMapOpStyle == 7) //描绘
            {
                OnSketch_MouseClick(e);
            }
            else if (mMapOpStyle == 8) //编辑
            {

            }
        }

        private void OnSketch_MouseClick(MouseEventArgs e)
        {
            //将屏幕坐标转换为地图坐标并加入描绘图形
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            mSketchingShape.Last().Add(sPoint);
            //地图控件重绘跟踪图层
            moMap.RedrawTrackingShapes();
        }

        //缩小状态下鼠标单击
        private void OnZoomOut_MouseClick(MouseEventArgs e)
        {
            //单点缩小
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            moMap.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
        }

        private void moMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) //放大
            {
                OnZoomIn_MouseMove(e);
            }
            else if (mMapOpStyle == 2) //缩小
            {

            }
            else if (mMapOpStyle == 3) //漫游
            {
                OnPan_MouseMove(e);
            }
            else if (mMapOpStyle == 4) //选择
            {
                OnSelect_MouseMove(e);
            }
            else if (mMapOpStyle == 5) //查询
            {
                OnIdentify_MouseMove(e);
            }
            else if (mMapOpStyle == 6) //移动
            {
                OnMoveShape_MouseMove(e);
            }
            else if (mMapOpStyle == 7) //描绘
            {
                OnSketch_MouseMove(e);
            }
            else if (mMapOpStyle == 8) //编辑
            {

            }
            ShowCoordinates(e.Location); //显示鼠标位置的地图坐标
        }

        //描绘状态下鼠标移动
        private void OnSketch_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
            Int32 sPointCount = sLastPart.Count;
            if (sPointCount == 0)
            {
            }
            else if (sPointCount == 1)
            {
                moMap.Refresh();
                MyMapObjects.moPoint sFirstPoint = sLastPart.GetItem(0);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawLine(sFirstPoint, sCurPoint, mElasticSymbol);
            }
            else
            {
                moMap.Refresh();
                MyMapObjects.moPoint sFirstPoint = sLastPart.GetItem(0);
                MyMapObjects.moPoint sLastPoint = sLastPart.GetItem(sPointCount - 1);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawLine(sFirstPoint, sCurPoint, mElasticSymbol);
                sDrawingTool.DrawLine(sLastPoint, sCurPoint, mElasticSymbol);
            }
        }

        //移动图形状态下鼠标移动
        private void OnMoveShape_MouseMove(MouseEventArgs e)
        {
            if (mIsInMovingShapes == false)
            {
                return;
            }
            double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            ModifyMovingGeometries(sDeltaX, sDeltaY);
            moMap.Refresh();
            DrawMovingShapes();
            mStartMouseLocation = e.Location;
        }

        private void OnIdentify_MouseMove(MouseEventArgs e)
        {
            if (mIsInIdentify == false)
            {
                return;
            }
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mSelectingBoxSymbol);
        }

        //选择状态下鼠标移动
        private void OnSelect_MouseMove(MouseEventArgs e)
        {
            if (mIsInSelect == false)
            {
                return;
            }
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mSelectingBoxSymbol);
        }
        //漫游状态下鼠标移动
        private void OnPan_MouseMove(MouseEventArgs e)
        {
            if (mIsInPan == false)
            {
                return;
            }
            moMap.PanMapImageTo(e.Location.X - mStartMouseLocation.X, e.Location.Y - mStartMouseLocation.Y);
        }

        //放大状态下鼠标移动
        private void OnZoomIn_MouseMove(MouseEventArgs e)
        {
            if (mIsInZoomIn == false)
            {
                return;
            }
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);
        }

        private void moMap_MapScaleChanged(object sender)
        {
            ShowMapScale(); //显示比例尺
        }

        private void moMap_AfterTrackingLayerDraw(object sender, MyMapObjects.moUserDrawingTool drawingTool)
        {
            DrawSketchingShapes(drawingTool); //绘制描绘图形
            DrawEditingShapes(drawingTool); //绘制正在编辑的图形
        }

        private void moMap_MouseWheel(object sender, MouseEventArgs e)
        {
            //计算地图控件中心的地图坐标
            double sX = moMap.ClientRectangle.Width / 2;
            double sY = moMap.ClientRectangle.Height / 2;
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(sX, sY);
            if (e.Delta > 0)
            {
                moMap.ZoomByCenter(sPoint, mZoomRatioMouseWheel);
            }
            else
            {
                moMap.ZoomByCenter(sPoint, 1 / mZoomRatioMouseWheel);
            }
        }
        #endregion

        #region 地图事件处理
        #endregion

        #region 私有函数
        // 初始化符号
        private void InitializeSymbols()
        {
            mSelectingBoxSymbol = new MyMapObjects.moSimpleFillSymbol();
            mSelectingBoxSymbol.Color = Color.Transparent;
            mSelectingBoxSymbol.Outline.Color = mSelectBoxColor;
            mSelectingBoxSymbol.Outline.Size = mSelectBoxWidth;
            mZoomBoxSymbol = new MyMapObjects.moSimpleFillSymbol();
            mZoomBoxSymbol.Color = Color.Transparent;
            mZoomBoxSymbol.Outline.Color = mZoomBoxColor;
            mZoomBoxSymbol.Outline.Size = mZoomBoxWidth;
            mMovingPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();
            mMovingPolygonSymbol.Color = Color.Transparent;
            mMovingPolygonSymbol.Outline.Color = Color.Black;
            mEditingPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();
            mEditingPolygonSymbol.Color = Color.Transparent;
            mEditingPolygonSymbol.Outline.Color = Color.DarkGreen;
            mEditingPolygonSymbol.Outline.Size = 0.53;
            mEditingVertexSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mEditingVertexSymbol.Color = Color.DarkGreen;
            mEditingVertexSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            mEditingVertexSymbol.Size = 2;
            mElasticSymbol = new MyMapObjects.moSimpleLineSymbol();
            mElasticSymbol.Color = Color.DarkGreen;
            mElasticSymbol.Size = 0.52;
            mElasticSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
        }

        // 初始化描绘图形
        private void InitializeSketchingShape()
        {
            mSketchingShape = new List<MyMapObjects.moPoints>();
            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
            mSketchingShape.Add(sPoints);
        }

        // 根据屏幕坐标显示地图坐标
        private void ShowCoordinates(PointF point)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(point.X, point.Y);
            if (mShowLngLat == false)
            {
                double sX = Math.Round(sPoint.X);
                double sY = Math.Round(sPoint.Y);
                tssCoordinate.Text = "X:" + sX.ToString() + ",Y:" + sY.ToString();
            }
            else
            {
                MyMapObjects.moPoint sLngLat = moMap.ProjectionCS.TransferToLngLat(sPoint);
                double sX = Math.Round(sLngLat.X, 4);
                double sY = Math.Round(sLngLat.Y, 4);
                tssCoordinate.Text = "X:" + sX.ToString() + ",Y:" + sY.ToString();
            }
        }

        //显示当前比例尺
        private void ShowMapScale()
        {
            tssMapScale.Text = "1: " + moMap.MapScale.ToString("0.00");
        }

        //根据屏幕上两点获得一个地图坐标下的矩形
        private MyMapObjects.moRectangle GetMapRectByTwoPoints(PointF point1, PointF point2)
        {
            MyMapObjects.moPoint sPoint1 = moMap.ToMapPoint(point1.X, point1.Y);
            MyMapObjects.moPoint sPoint2 = moMap.ToMapPoint(point2.X, point2.Y);
            double sMinX = Math.Min(sPoint1.X, sPoint2.X);
            double sMaxX = Math.Max(sPoint1.X, sPoint2.X);
            double sMinY = Math.Min(sPoint1.Y, sPoint2.Y);
            double sMaxY = Math.Max(sPoint1.Y, sPoint2.Y);
            MyMapObjects.moRectangle sRect = new MyMapObjects.moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sRect;
        }

        //获取一个多边形图层
        private MyMapObjects.moMapLayer GetPolygonLayer()
        {
            Int32 sLayerCount = moMap.Layers.Count;
            MyMapObjects.moMapLayer sLayer = null;
            for (Int32 i = 0; i <= sLayerCount - 1; i++)
            {
                if (moMap.Layers.GetItem(i).ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    sLayer = moMap.Layers.GetItem(i);
                    break;
                }
            }
            return sLayer;
        }

        //根据指定的平移量修改移动图形的坐标
        private void ModifyMovingGeometries(double deltaX, double deltaY)
        {
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolygon))
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mMovingGeometries[i];
                    Int32 sPartCount = sMultiPolygon.Parts.Count;
                    for (Int32 j = 0; j <= sPartCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sMultiPolygon.Parts.GetItem(j);
                        Int32 sPointCount = sPoints.Count;
                        for (Int32 k = 0; k <= sPointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            sPoint.X = sPoint.X + deltaX;
                            sPoint.Y = sPoint.Y + deltaY;
                        }
                    }
                    sMultiPolygon.UpdateExtent();
                }
            }
        }

        //绘制正在移动的图形
        private void DrawMovingShapes()
        {
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolygon))
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mMovingGeometries[i];
                    sDrawingTool.DrawMultiPolygon(sMultiPolygon, mMovingPolygonSymbol);
                }
            }
        }

        //绘制正在描绘的图形
        private void DrawSketchingShapes(MyMapObjects.moUserDrawingTool drawingTool)
        {
            if (mSketchingShape == null)
                return;
            Int32 sPartCount = mSketchingShape.Count;
            //绘制已经描绘完成的部分
            for (Int32 i = 0; i <= sPartCount - 2; i++)
            {
                drawingTool.DrawPolygon(mSketchingShape[i], mEditingPolygonSymbol);
            }
            //正在描绘的部分（只有一个Part）
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
            if (sLastPart.Count >= 2)
                drawingTool.DrawPolyline(sLastPart, mEditingPolygonSymbol.Outline);
            //绘制所有顶点手柄
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = mSketchingShape[i];
                drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
            }
        }

        //绘制正在编辑的图形
        private void DrawEditingShapes(MyMapObjects.moUserDrawingTool drawingTool)
        {
            if (mEditingGeometry == null)
                return;
            if (mEditingGeometry.GetType() == typeof(MyMapObjects.moMultiPolygon))
            {
                MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mEditingGeometry;
                //绘制边界
                drawingTool.DrawMultiPolygon(sMultiPolygon, mEditingPolygonSymbol);
                //绘制顶点手柄
                Int32 sPartCount = sMultiPolygon.Parts.Count;
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sMultiPolygon.Parts.GetItem(i);
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
        }

        #endregion

        private void btnProjection_Click(object sender, EventArgs e)
        {

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 详情面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 编辑节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 保存图层为GeoJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var propertyWindowForm = new propertyTable();
            propertyWindowForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            btnFullExtent_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            btnZoomIn_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            btnZoomOut_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            btnPan_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            btnIdentify_Click(sender, e);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void geoJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog sDialog = new OpenFileDialog();
            string sFileName = "";
            if (sDialog.ShowDialog(this)
                == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }

            try
            {
                moMap.Layers.Add(DataIOTools.LoadMapLayerFromGeoJSON(sFileName));
            }

            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (moMap.Layers.Count == 1)
            {
                moMap.FullExtent();
            }
            else
            {
                moMap.RedrawMap();
            }
        }

        private void tssMapScale_Click(object sender, EventArgs e)
        {

        }
    }
}
