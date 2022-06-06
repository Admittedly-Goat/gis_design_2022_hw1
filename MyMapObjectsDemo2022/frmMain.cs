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
        private MyMapObjects.moSimpleMarkerSymbol mMovingPointSymbol;  //正在移动的点符号
        private MyMapObjects.moSimpleLineSymbol mMovingPolylineSymbol;  //正在移动的线符号
        private MyMapObjects.moSimpleFillSymbol mEditingPolygonSymbol; // 正在编辑的多边形符号
        private MyMapObjects.moSimpleLineSymbol mEditingLineSymbol; // 正在编辑的线段符号
        private MyMapObjects.moSimpleMarkerSymbol mEditingVertexSymbol; // 正在编辑的图形顶点符号
        private MyMapObjects.moSimpleMarkerSymbol mEditingHighlightedVertexSymbol; // 正在编辑的高亮的图形顶点符号
        private MyMapObjects.moSimpleLineSymbol mElasticSymbol; // 橡皮筋符号
        private MyMapObjects.moSimpleMarkerSymbol mSimpleRendererPointSymbol = new MyMapObjects.moSimpleMarkerSymbol();//当前简单渲染的点符号
        private MyMapObjects.moSimpleLineSymbol mSimpleRendererLineSymbol = new MyMapObjects.moSimpleLineSymbol();//当前简单渲染的线符号
        private MyMapObjects.moSimpleFillSymbol mSimpleRendererPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();//当前简单渲染的面符号
        private bool mShowLngLat = false; // 是否显示经纬度
        private int checklistIndex = -1;   //全局图层列表索引

        // 与地图操作有关的变量
        private Int32 mMapOpStyle = 0; // 0：无，1：放大，2：缩小，3：漫游，4：选择，5：查询，6：移动，7：描绘，8：编辑
        private PointF mStartMouseLocation;
        private PointF mOriginMouseLocation;
        private bool mIsInZoomIn = true;
        private bool mIsInPan = false;
        private bool mIsInSelect = false;
        private bool mIsInIdentify = false;
        private bool mIsInMovingShapes = false;
        private List<MyMapObjects.moGeometry> mMovingGeometries = new List<MyMapObjects.moGeometry>(); // 正在移动的图形的集合
        private List<MyMapObjects.moFeature> mMovingFeatures = new List<MyMapObjects.moFeature>(); //正在移动的要素集合，index和上面的移动图形集合对应。
        private MyMapObjects.moGeometry mEditingGeometry; // 正在编辑的图形
        private List<MyMapObjects.moPoints> mSketchingShape; // 正在描绘的图形，用一个多点集合存储
        MyMapObjects.moSimpleMarkerSymbol pSymbol = new MyMapObjects.moSimpleMarkerSymbol();  //描绘点
        MyMapObjects.moSimpleLineSymbol lineSymbol = new MyMapObjects.moSimpleLineSymbol();   //描绘线
        private int identifySelectedLayerIndex = -1; //查询功能选择的Layer
        private propertyTable propertyTableForm; //属性表对象，作为窗体的一个附属类来进行操作，不使用复杂的委托等功能。
        private VertexEditor vertexEditorForm; //顶点编辑窗体
        private bool isPropertyTableShowing //属性表是否正在显示
        {
            get
            {
                return propertyTableForm != null;
            }
        }
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
            if (!(moMap.ProjectionCS.ProjType == MyMapObjects.moProjectionTypeConstant.None))
            {
                MessageBox.Show("根据IETF官方范式要求，本程序的标准格式——GeoJSON，的坐标系只能为WGS84经纬度，请先将坐标系统调整为WGS84后再打开。");
                return;
            }
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
                    DataIOTools.LoadMapLayer(sr, sFileName);
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
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            identifySelectedLayerIndex = checkedListBox1.SelectedIndex;
            moMap.Refresh();
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            else
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(identifySelectedLayerIndex); //获得选中的图层
                MyMapObjects.moSimpleRenderer sRenderer = new MyMapObjects.moSimpleRenderer();
                if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moSimpleMarkerSymbol mSimpleRendererPointSymbol_clone = new MyMapObjects.moSimpleMarkerSymbol();//用于判断是否进行了渲染操作，进而确认是否需要重绘地图
                    mSimpleRendererPointSymbol_clone = mSimpleRendererPointSymbol.Clone1();
                    SimpleRendererPoint cForm = new SimpleRendererPoint(mSimpleRendererPointSymbol);
                    cForm.ShowDialog();
                    sRenderer.Symbol = mSimpleRendererPointSymbol;
                    if (mSimpleRendererPointSymbol.Color != mSimpleRendererPointSymbol_clone.Color ||
                        mSimpleRendererPointSymbol.Size != mSimpleRendererPointSymbol_clone.Size ||
                        mSimpleRendererPointSymbol.Style != mSimpleRendererPointSymbol_clone.Style)
                    {
                        sLayer.Renderer = sRenderer;
                        moMap.RedrawMap();
                    }
                }
                else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moSimpleLineSymbol mSimpleRendererLineSymbol_clone = new MyMapObjects.moSimpleLineSymbol();//用于判断是否进行了渲染操作，进而确认是否需要重绘地图
                    mSimpleRendererLineSymbol_clone = mSimpleRendererLineSymbol.Clone1();
                    SimpleRendererLine cForm = new SimpleRendererLine(mSimpleRendererLineSymbol);
                    cForm.ShowDialog();
                    sRenderer.Symbol = mSimpleRendererLineSymbol;
                    if (mSimpleRendererLineSymbol.Color != mSimpleRendererLineSymbol_clone.Color ||
                        mSimpleRendererLineSymbol.Size != mSimpleRendererLineSymbol_clone.Size ||
                        mSimpleRendererLineSymbol.Style != mSimpleRendererLineSymbol_clone.Style)
                    {
                        sLayer.Renderer = sRenderer;
                        moMap.RedrawMap();
                    }
                }
                else
                {
                    MyMapObjects.moSimpleFillSymbol mSimpleRendererPolygonSymbol_clone = new MyMapObjects.moSimpleFillSymbol();//用于判断是否进行了渲染操作，进而确认是否需要重绘地图
                    mSimpleRendererPolygonSymbol_clone = mSimpleRendererPolygonSymbol.Clone1();
                    DialogResult dr = colorDialog1.ShowDialog();
                    //选择填充颜色
                    if (dr == DialogResult.OK)
                    {
                        mSimpleRendererPolygonSymbol.Color = colorDialog1.Color;
                    }
                    sRenderer.Symbol = mSimpleRendererPolygonSymbol;
                    if (mSimpleRendererPolygonSymbol.Color != mSimpleRendererPolygonSymbol_clone.Color)
                    {
                        sLayer.Renderer = sRenderer;
                        moMap.RedrawMap();
                    }
                }

            }
            checkedListBox1.SelectedIndex = identifySelectedLayerIndex;
        }

        private void btnUniqueValue_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            identifySelectedLayerIndex = checkedListBox1.SelectedIndex;
            moMap.Refresh();
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            else
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(identifySelectedLayerIndex); //获得选中的图层
                MyMapObjects.moUniqueValueRenderer sRenderer = new MyMapObjects.moUniqueValueRenderer();
                MyMapObjects.moUniqueValueRenderer sRenderer_clone = new MyMapObjects.moUniqueValueRenderer();
                sRenderer_clone = sRenderer.Clone1();
                if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    UniqueValuePoint cForm = new UniqueValuePoint(sLayer, sRenderer, mSimpleRendererPointSymbol);
                    cForm.ShowDialog();

                }
                else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    UniqueValueLIne cForm = new UniqueValueLIne(sLayer, sRenderer, mSimpleRendererLineSymbol);
                    cForm.ShowDialog();
                }
                else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    UniqueValuePolygon cForm = new UniqueValuePolygon(sLayer, sRenderer, mSimpleRendererPolygonSymbol);
                    cForm.ShowDialog();
                }
                if (sRenderer_clone.Field != sRenderer.Field)
                {
                    sLayer.Renderer = sRenderer;
                    moMap.RedrawMap();
                }

            }

            checkedListBox1.SelectedIndex = identifySelectedLayerIndex;

        }

        private void btnClassBreaks_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            identifySelectedLayerIndex = checkedListBox1.SelectedIndex;
            moMap.Refresh();
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            else
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(identifySelectedLayerIndex); //获得选中的图层
                MyMapObjects.moClassBreaksRenderer sRenderer = new MyMapObjects.moClassBreaksRenderer();
                MyMapObjects.moClassBreaksRenderer sRenderer_clone = sRenderer.Clone1();
                if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    ClassBreaksPoint cForm = new ClassBreaksPoint(sLayer, sRenderer, mSimpleRendererPointSymbol);
                    cForm.ShowDialog();
                    sRenderer.DefaultSymbol = mSimpleRendererPointSymbol;

                }
                else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    ClassBreaksLine cForm = new ClassBreaksLine(sLayer, sRenderer, mSimpleRendererLineSymbol);
                    cForm.ShowDialog();
                    sRenderer.DefaultSymbol = mSimpleRendererLineSymbol;
                }
                else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    ClassBreaksPolygon cForm = new ClassBreaksPolygon(sLayer, sRenderer, mSimpleRendererPolygonSymbol);
                    cForm.ShowDialog();
                    sRenderer.DefaultSymbol = mSimpleRendererPolygonSymbol;
                }
                if (sRenderer.Field != sRenderer_clone.Field)
                {
                    sLayer.Renderer = sRenderer;
                    moMap.RedrawMap();
                }

            }

            checkedListBox1.SelectedIndex = identifySelectedLayerIndex;
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
            int selectedIndex = checkedListBox1.SelectedIndex;
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
            else if (mMapOpStyle == 20)
            {
                OnEditingVertex_MouseDown(e);
            }
            else if (mMapOpStyle == 21)
            {
                var newPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
                if (vertexEditorForm != null)
                {
                    vertexEditorForm.HighlightedPoints.Add(newPoint);
                    vertexEditorForm.NewPart.Add(newPoint);
                    RedrawMapForVertexEditing();
                    if (vertexEditorForm.RemainingPartPointNumber == 1)
                    {
                        mMapOpStyle = -1;
                        vertexEditorForm.RemainingPartPointNumber = 0;
                        vertexEditorForm.AddPartCallBack();
                    }
                    else
                    {
                        vertexEditorForm.RemainingPartPointNumber--;
                    }
                }
            }
            else if (mMapOpStyle == 22)
            {
                var newPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
                if (vertexEditorForm != null)
                {
                    vertexEditorForm.AddVertexCallBack(newPoint);
                }
            }
            else if (mMapOpStyle == 23)
            {
                var newPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
                if (vertexEditorForm != null)
                {
                    vertexEditorForm.SelectByGeometryCallBack(newPoint);
                }
            }
            checkedListBox1.SelectedIndex = selectedIndex;
            if (e.Button == MouseButtons.Middle)
            {
                OnPan_MouseMiddleDown(e);
            }
        }

        private void OnEditingVertex_MouseDown(MouseEventArgs e)
        {
            var newPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            if (vertexEditorForm != null)
            {
                vertexEditorForm.MoveVertexCallBack(newPoint);
                mMapOpStyle = -1;
            }
        }

        //移动图形状态下鼠标按下
        private void OnMovingShape_MouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            checklistIndex = selectedIndex;
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(selectedIndex);
            //判断是否有选中的要素
            Int32 sSelFeatureCount = sLayer.SelectedFeatures.Count;
            if (sSelFeatureCount == 0)
            {
                return;
            }
            //复制图形
            mMovingGeometries.Clear();
            if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                {
                    MyMapObjects.moMultiPolygon sOriPolygon = (MyMapObjects.moMultiPolygon)sLayer.SelectedFeatures.GetItem(i).Geometry;
                    MyMapObjects.moMultiPolygon sDesPolygon = sOriPolygon.Clone();
                    mMovingGeometries.Add(sDesPolygon);
                    mMovingFeatures.Add(sLayer.SelectedFeatures.GetItem(i));
                }
            }
            else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                {
                    MyMapObjects.moMultiPolyline sOriPolyline = (MyMapObjects.moMultiPolyline)sLayer.SelectedFeatures.GetItem(i).Geometry;
                    MyMapObjects.moMultiPolyline sDesPolyline = sOriPolyline.Clone();
                    mMovingGeometries.Add(sDesPolyline);
                    mMovingFeatures.Add(sLayer.SelectedFeatures.GetItem(i));
                }
            }
            else if (sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                {
                    MyMapObjects.moPoint sOriPoint = (MyMapObjects.moPoint)sLayer.SelectedFeatures.GetItem(i).Geometry;
                    MyMapObjects.moPoint sDesPoint = sOriPoint.Clone();
                    mMovingGeometries.Add(sDesPoint);
                    mMovingFeatures.Add(sLayer.SelectedFeatures.GetItem(i));
                }
            }
            //设置变量
            mStartMouseLocation = e.Location;
            mOriginMouseLocation = e.Location;
            mIsInMovingShapes = true;
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        //查询状态下鼠标按下
        private void OnIdentify_MouseDown(MouseEventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            identifySelectedLayerIndex = checkedListBox1.SelectedIndex;
            moMap.Refresh();
            MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(e.Location, e.Location);
            double sTolerance = moMap.ToMapDistance(mSelectingTolerance);
            if (moMap.Layers.Count == 0)
            {
                return;
            }
            else
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(identifySelectedLayerIndex); //获得选中的图层
                MyMapObjects.moFeatures sFeatures = sLayer.SearchByBox(sBox, sTolerance);
                if (sLayer.Visible == false)
                    return;
                // 只选择最上部的那个对象
                if (sFeatures.Count > 0)
                {
                    MyMapObjects.moGeometry[] sGeometries = new MyMapObjects.moGeometry[1];
                    sGeometries[0] = sFeatures.GetItem(sFeatures.Count - 1).Geometry;
                    treeView1.BeginUpdate();
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("要素内部ID");
                    //比对并获取要素内部ID
                    for (int i = 0; i < sLayer.Features.Count; i++)
                    {
                        if (sLayer.Features.GetItem(i) == sFeatures.GetItem(sFeatures.Count - 1))
                        {
                            treeView1.Nodes[0].Nodes.Add(Convert.ToString(i));
                        }
                    }
                    for (int i = 0; i < sLayer.AttributeFields.Count; i++)
                    {
                        treeView1.Nodes.Add(sLayer.AttributeFields.GetItem(i).Name);
                        treeView1.Nodes[i + 1].Nodes.Add(sFeatures.GetItem(sFeatures.Count - 1).Attributes.GetItem(i).ToString());
                    }
                    treeView1.ExpandAll();
                    treeView1.EndUpdate();
                    moMap.FlashShapes(sGeometries, 3, 800);
                }
            }
            checkedListBox1.SelectedIndex = identifySelectedLayerIndex;
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

        private void OnPan_MouseMiddleDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
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
            int selectedIndex = checkedListBox1.SelectedIndex;
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
            if (e.Button == MouseButtons.Middle)
            {
                OnPan_MouseMiddleUp(e);
            }
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        //移动图形状态下鼠标松开(需要实现展示移动结果)
        private void OnMoveShape_MouseUp(MouseEventArgs e)
        {
            if (mIsInMovingShapes == false)
            {
                return;
            }
            mIsInMovingShapes = false;
            //更新图层要素的数据
            int sCount = mMovingGeometries.Count;
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(checklistIndex);

            //删除原有选中要素（删除的是选中要素list，如果多图层选中的话有问题）
            int selectedIndex = checklistIndex;
            int count = moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Count;
            for (Int32 i = 0; i < count; i++)
            {
                MyMapObjects.moFeature feature = moMap.Layers.GetItem(selectedIndex).SelectedFeatures.GetItem(i);
                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.GetItem(i).Geometry = mMovingGeometries[i];
            }
            //更新新要素的坐标（距离有问题）
            double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            ModifyMovingGeometries(sDeltaX, sDeltaY);

            //重绘地图
            moMap.RedrawMap();
            //清除移动图形列表
            mMovingGeometries.Clear();
            checklistIndex = -1;
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        private void OnIdentify_MouseUp(MouseEventArgs e)
        {

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
            if (propertyTableForm != null)
            {
                propertyTableForm.CanAffectLayerSelection = false;
                propertyTableForm.ReloadPropList();
                propertyTableForm.CanAffectLayerSelection = true;
            }
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

        private void OnPan_MouseMiddleUp(MouseEventArgs e)
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
            else if (mMapOpStyle == 7) //描绘多边形
            {
                OnSketch_MouseClick(e);
            }
            else if (mMapOpStyle == 8) //编辑
            {

            }
            else if (mMapOpStyle == 9)  //描绘线
            {
                OnSketchPolyline_MouseClick(e);
            }
            else if (mMapOpStyle == 10)  //描绘点
            {
                OnSketchPoint_MouseClick(e);
            }
        }


        private void OnSketchPolyline_MouseClick(MouseEventArgs e)
        {
            //将屏幕坐标转换为地图坐标并加入描绘图形
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            mSketchingShape.Last().Add(sPoint);
            //地图控件重绘跟踪图层
            moMap.RedrawTrackingShapes();
        }

        private void OnSketchPoint_MouseClick(MouseEventArgs e)
        {
            //将屏幕坐标转换为地图坐标并加入描绘图形
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            mSketchingShape.Last().Add(sPoint);
            //地图控件重绘跟踪图层
            MyMapObjects.moUserDrawingTool dTool = moMap.GetDrawingTool();
            dTool.DrawPoints(mSketchingShape.Last(), pSymbol);
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
            int selectedIndex = checkedListBox1.SelectedIndex;
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
            else if (mMapOpStyle == 9)
            {
                OnSketchLine_MouseMove(e);
            }
            ShowCoordinates(e.Location); //显示鼠标位置的地图坐标
            if (e.Button == MouseButtons.Middle)
            {
                OnPan_MouseMiddleMove(e);
            }
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        //描绘线状态下鼠标移动
        private void OnSketchLine_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            if (mSketchingShape.Count == 0)
                return;
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
                //sDrawingTool.DrawLine(sFirstPoint, sCurPoint, mElasticSymbol);
                sDrawingTool.DrawLine(sLastPoint, sCurPoint, mElasticSymbol);
            }
        }

        //描绘状态下鼠标移动
        private void OnSketch_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            if (mSketchingShape.Count == 0)
                return;
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

        private void OnPan_MouseMiddleMove(MouseEventArgs e)
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
            if (mMapOpStyle == 9)
            {
                DrawSketchingShapesLine(drawingTool); //绘制描绘图形
            }
            else
            {
                DrawSketchingShapes(drawingTool);
            }
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
            mMovingPolylineSymbol = new MyMapObjects.moSimpleLineSymbol();
            mMovingPolylineSymbol.Color = Color.Black;
            mMovingPointSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mMovingPointSymbol.Color = Color.Black;
            mEditingPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();
            mEditingPolygonSymbol.Color = Color.Transparent;
            mEditingPolygonSymbol.Outline.Color = Color.DarkGreen;
            mEditingPolygonSymbol.Outline.Size = 0.53;
            mEditingLineSymbol = new MyMapObjects.moSimpleLineSymbol();
            mEditingLineSymbol.Color = Color.DarkGreen;
            mEditingLineSymbol.Size = 0.53;
            mEditingVertexSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mEditingVertexSymbol.Color = Color.DarkGreen;
            mEditingVertexSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            mEditingVertexSymbol.Size = 2;
            mElasticSymbol = new MyMapObjects.moSimpleLineSymbol();
            mElasticSymbol.Color = Color.DarkGreen;
            mElasticSymbol.Size = 0.52;
            mElasticSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
            mEditingHighlightedVertexSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mEditingHighlightedVertexSymbol.Color = Color.Red;
            mEditingHighlightedVertexSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            mEditingHighlightedVertexSymbol.Size = 4;
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
                double sX = Math.Round(sPoint.X, 3);
                double sY = Math.Round(sPoint.Y, 3);
                tssCoordinate.Text = "X:" + sX.ToString() + ",Y:" + sY.ToString();
            }
            else
            {
                MyMapObjects.moPoint sLngLat = moMap.ProjectionCS.TransferToLngLat(sPoint);
                double sX = Math.Round(sLngLat.X, 3);
                double sY = Math.Round(sLngLat.Y, 3);
                tssCoordinate.Text = "经度:" + sX.ToString() + ",纬度:" + sY.ToString();
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

        //获取一个线图层
        private MyMapObjects.moMapLayer GetLineLayer()
        {
            Int32 sLayerCount = moMap.Layers.Count;
            MyMapObjects.moMapLayer sLayer = null;
            for (Int32 i = 0; i <= sLayerCount - 1; i++)
            {
                if (moMap.Layers.GetItem(i).ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    sLayer = moMap.Layers.GetItem(i);
                    break;
                }
            }
            return sLayer;
        }

        //获取一个点图层
        private MyMapObjects.moMapLayer GetPointLayer()
        {
            Int32 sLayerCount = moMap.Layers.Count;
            MyMapObjects.moMapLayer sLayer = null;
            for (Int32 i = 0; i <= sLayerCount - 1; i++)
            {
                if (moMap.Layers.GetItem(i).ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
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
                else if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolyline))
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mMovingGeometries[i];
                    Int32 sPartCount = sMultiPolyline.Parts.Count;
                    for (Int32 j = 0; j <= sPartCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sMultiPolyline.Parts.GetItem(j);
                        Int32 sPointCount = sPoints.Count;
                        for (Int32 k = 0; k <= sPointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            sPoint.X = sPoint.X + deltaX;
                            sPoint.Y = sPoint.Y + deltaY;
                        }
                    }
                    sMultiPolyline.UpdateExtent();
                }
                else if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moPoint))
                {
                    MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mMovingGeometries[i];
                    sPoint.X = sPoint.X + deltaX;
                    sPoint.Y = sPoint.Y + deltaY;
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
                else if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolyline))
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mMovingGeometries[i];
                    sDrawingTool.DrawMultiPolyline(sMultiPolyline, mMovingPolylineSymbol);
                }
                else if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moPoint))
                {
                    MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mMovingGeometries[i];
                    sDrawingTool.DrawPoint(sPoint, mMovingPointSymbol);
                }
            }
        }

        //绘制正在描绘的图形。针对多边形
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
            if (mSketchingShape.Count == 0)
                return;
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

        //绘制正在描绘的图形，针对线段设计
        private void DrawSketchingShapesLine(MyMapObjects.moUserDrawingTool drawingTool)
        {
            if (mSketchingShape == null)
                return;
            Int32 sPartCount = mSketchingShape.Count;
            //绘制已经描绘完成的部分
            for (Int32 i = 0; i <= sPartCount - 2; i++)
            {
                drawingTool.DrawPolyline(mSketchingShape[i], mEditingLineSymbol);
            }
            //正在描绘的部分（只有一个Part）
            if (mSketchingShape.Count == 0)
                return;
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
            if (sLastPart.Count >= 2)
                drawingTool.DrawPolyline(sLastPart, mEditingLineSymbol);
            //绘制所有顶点手柄
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = mSketchingShape[i];
                drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
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
            var selectedIndex = checkedListBox1.SelectedIndex;
            ChangeLayerOrder cForm = new ChangeLayerOrder(moMap.Layers, moMap);
            cForm.ShowDialog();
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        private void 编辑节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 保存图层为GeoJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(moMap.ProjectionCS.ProjType == MyMapObjects.moProjectionTypeConstant.None))
            {
                MessageBox.Show("根据IETF官方范式要求，本程序的标准格式——GeoJSON，的坐标系只能为WGS84经纬度，请先将坐标系统调整为WGS84后再保存。");
                return;
            }
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = Convert.ToInt32(checkedListBox1.SelectedIndex);
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "GeoJSON Files (*.geojson)|*.geojson";
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
            DataIOTools.SaveLayerAsGeoJSON(sFileName, moMap.Layers.GetItem(selectedIndex));
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
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
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
            if (!(moMap.ProjectionCS.ProjType == MyMapObjects.moProjectionTypeConstant.None))
            {
                MessageBox.Show("根据IETF官方范式要求，本程序的标准格式——GeoJSON，的坐标系只能为WGS84经纬度，请先将坐标系统调整为WGS84后再打开。");
                return;
            }
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void moMap_LayerChanged(object sender)
        {
            moMap.isAlreadyRedrawLayer = true;
            checkedListBox1.Items.Clear();
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                checkedListBox1.Items.Add($"[{i}] " + moMap.Layers.GetItem(i).Name, moMap.Layers.GetItem(i).Visible);
            }
            moMap.isAlreadyRedrawLayer = false;
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            moMap.isAlreadyRedrawLayer = true;
            var currentLayerList = checkedListBox1.CheckedItems;
            // [ true, false, true ...], index represents layer sequence
            var currentVisibilityCondition = new List<bool>();
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                currentVisibilityCondition.Add(false);
            }
            for (int i = 0; i < currentLayerList.Count; i++)
            {
                currentVisibilityCondition[Convert.ToInt32(currentLayerList[i].ToString().Split(']')[0].Split('[')[1])] = true;
            }
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                moMap.Layers.GetItem(i).Visible = currentVisibilityCondition[i];
            }
            moMap.isAlreadyRedrawLayer = false;
            moMap.RedrawMap();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tssCoordinate_Click(object sender, EventArgs e)
        {

        }

        private void 新建点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePointLayer cForm = new CreatePointLayer();
            cForm.CreateLayer += CreateLayer;
            cForm.ShowDialog();
        }


        //创建图层
        public void CreateLayer(MyMapObjects.moMapLayer layer)
        {
            moMap.Layers.Add(layer);
            moMap.RefreshLayerList();
            moMap.RedrawMap();
        }

        private void SetPropertyTableToNull()
        {
            propertyTableForm = null;
        }


        private void 新建线图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateLineLayer cForm = new CreateLineLayer();
            cForm.CreateLayer += CreateLayer;
            cForm.ShowDialog();
        }

        private void 新建面图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePolygonLayer cForm = new CreatePolygonLayer();
            cForm.CreateLayer += CreateLayer;
            cForm.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            moMap.Layers.RemoveAt(selectedIndex);
            moMap.RefreshLayerList();
            moMap.RedrawMap();
        }

        private void 修改名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            ChangeNameOfLayer cForm = new ChangeNameOfLayer(moMap.Layers.GetItem(selectedIndex));
            cForm.ShowDialog();
            moMap.RefreshLayerList();
            moMap.RedrawMap();
        }

        private void 属性表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isPropertyTableShowing)
            {
                MessageBox.Show("当前正在显示属性表，请关闭后重试。");
                return;
            }
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            propertyTableForm = new propertyTable(moMap.Layers.GetItem(selectedIndex), SetPropertyTableToNull, RedrawMapForPropertyTableChange);
            propertyTableForm.Show();
            propertyTableForm.ReloadPropList();
            propertyTableForm.CanAffectLayerSelection = true;
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        private void RedrawMapForPropertyTableChange()
        {
            moMap.RedrawTrackingShapes();
            moMap.RedrawMap();
        }

        private void 几何选取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void 删除已选择的图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            int count = moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Count;
            for (Int32 i = 0; i < count; i++)
            {
                MyMapObjects.moFeature feature = moMap.Layers.GetItem(selectedIndex).SelectedFeatures.GetItem(i);
                moMap.Layers.GetItem(selectedIndex).Features.Remove(feature);
            }
            moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Clear();
            moMap.RefreshLayerList();
            moMap.RedrawTrackingShapes();
            moMap.RedrawMap();
            if (propertyTableForm != null)
            {
                propertyTableForm.ReloadPropList();
            }
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        private void 移动已选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = checkedListBox1.SelectedIndex;
            btnMovePolygon_Click(sender, e);
            checkedListBox1.SelectedIndex = selectedIndex;
        }

        private void 增加新要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 编辑节点ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void 停止编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            btnEndEdit_Click(sender, e);
        }

        private void 面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            if (moMap.Layers.GetItem(selectedIndex).ShapeType != MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MessageBox.Show("选择要素与图层不匹配！");
                checkedListBox1.SelectedIndex = selectedIndex;
                return;
            }
            else
            {
                btnSketchPolygon_Click(sender, e);
                checkedListBox1.SelectedIndex = selectedIndex;
            }
        }

        private void 线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            if (moMap.Layers.GetItem(selectedIndex).ShapeType != MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MessageBox.Show("选择要素与图层不匹配！");
                checkedListBox1.SelectedIndex = selectedIndex;
                return;
            }
            else
            {
                checkedListBox1.SelectedIndex = selectedIndex;
                mMapOpStyle = 9;
            }
        }

        private void 点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            if (moMap.Layers.GetItem(selectedIndex).ShapeType != MyMapObjects.moGeometryTypeConstant.Point)
            {
                MessageBox.Show("选择要素与图层不匹配！");
                checkedListBox1.SelectedIndex = selectedIndex;
                return;
            }
            else
            {
                checkedListBox1.SelectedIndex = selectedIndex;
                mMapOpStyle = 10;
            }
        }

        private void btnEndPolylineSketch_Click(object sender, EventArgs e)    //停止编辑线
        {
            if (mSketchingShape.Last().Count == 1)
            {
                return;
            }
            if (mSketchingShape.Last().Count == 0)
            {
                mSketchingShape.Remove(mSketchingShape.Last());

            }
            if (mSketchingShape.Count > 0)
            {
                MyMapObjects.moMapLayer sLayer = GetLineLayer();
                if (sLayer != null)
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
                    sMultiPolyline.Parts.AddRange(mSketchingShape.ToArray());
                    sMultiPolyline.UpdateExtent();
                    MyMapObjects.moFeature sFeature = sLayer.GetNewFeature();
                    sFeature.Geometry = sMultiPolyline;
                    sLayer.Features.Add(sFeature);
                }
            }
            InitializeSketchingShape();
            moMap.RedrawMap();
        }

        private void btnEndPointSketch_Click(object sender, EventArgs e)    //停止编辑点
        {
            if (mSketchingShape.Last().Count == 0)
            {
                mSketchingShape.Remove(mSketchingShape.Last());

            }
            if (mSketchingShape.Count > 0)
            {
                MyMapObjects.moMapLayer sLayer = GetPointLayer();
                if (sLayer != null)
                {
                    MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                    sPoints.AddRange(mSketchingShape.Last().ToArray());
                    for (int i = 0; i < sPoints.Count; i++)
                    {
                        MyMapObjects.moPoint sPoint = sPoints.GetItem(i);
                        MyMapObjects.moFeature sFeature = sLayer.GetNewFeature();
                        sFeature.Geometry = sPoint;
                        sLayer.Features.Add(sFeature);
                    }
                    MyMapObjects.moUserDrawingTool dTool = moMap.GetDrawingTool();
                    dTool.DrawPoints(sPoints, pSymbol);
                }
            }
            InitializeSketchingShape();
            moMap.RedrawMap();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)  //停止描绘
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            int selectedIndex = checkedListBox1.SelectedIndex;
            if (moMap.Layers.GetItem(selectedIndex).ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)   //描绘多边形
            {
                btnEndSketch_Click(sender, e);
            }
            else if (moMap.Layers.GetItem(selectedIndex).ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)    //描绘线
            {
                btnEndPolylineSketch_Click(sender, e);
            }
            else if (moMap.Layers.GetItem(selectedIndex).ShapeType == MyMapObjects.moGeometryTypeConstant.Point)    //描绘点
            {
                btnEndPointSketch_Click(sender, e);
            }
            checkedListBox1.SelectedIndex=selectedIndex;
        }

        private void 停止部分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnEndPart_Click(sender, e);
        }

        private void 属性选取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还未选择任何的对象，点击左侧列表中的条目即可选取。");
                return;
            }
            var selectedIndex = checkedListBox1.SelectedIndex;
            SelectAttributeFields selectAttributeFieldsForm = new SelectAttributeFields(moMap.Layers.GetItem(selectedIndex));
            selectAttributeFieldsForm.ShowDialog();
            int selectedAttributeIndex = selectAttributeFieldsForm.SelectedFieldIndex;
            int operatorType = -1;
            if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dText)
            {
                MessageBox.Show("该字段为文本型，仅支持等于判断，已为您选择此操作类型。");
                operatorType = 2;
            }
            else
            {
                OperatorTypeSelection operatorTypeSelection = new OperatorTypeSelection();
                operatorTypeSelection.ShowDialog();
                operatorType = operatorTypeSelection.OperatorType;
            }
            string selectValue = Microsoft.VisualBasic.Interaction.InputBox("请输入值");
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                moMap.Layers.GetItem(i).SelectedFeatures.Clear();
            }
            try
            {
                for (int i = 0; i < moMap.Layers.GetItem(selectedIndex).Features.Count; i++)
                {
                    object featureValue = moMap.Layers.GetItem(selectedIndex).Features.GetItem(i).Attributes.GetItem(selectedAttributeIndex);
                    if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt16)
                    {
                        var featureValueConverted = Convert.ToInt16(featureValue);
                        var selectedValueConverted = Convert.ToInt16(selectValue);
                        if (operatorType == 0)
                        {
                            if (featureValueConverted > selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 1)
                        {
                            if (featureValueConverted < selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 2)
                        {
                            if (featureValueConverted == selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 3)
                        {
                            if (featureValueConverted >= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 4)
                        {
                            if (featureValueConverted <= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                    else if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt32)
                    {
                        var featureValueConverted = Convert.ToInt32(featureValue);
                        var selectedValueConverted = Convert.ToInt32(selectValue);
                        if (operatorType == 0)
                        {
                            if (featureValueConverted > selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 1)
                        {
                            if (featureValueConverted < selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 2)
                        {
                            if (featureValueConverted == selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 3)
                        {
                            if (featureValueConverted >= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 4)
                        {
                            if (featureValueConverted <= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                    else if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                    {
                        var featureValueConverted = Convert.ToInt64(featureValue);
                        var selectedValueConverted = Convert.ToInt64(selectValue);
                        if (operatorType == 0)
                        {
                            if (featureValueConverted > selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 1)
                        {
                            if (featureValueConverted < selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 2)
                        {
                            if (featureValueConverted == selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 3)
                        {
                            if (featureValueConverted >= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 4)
                        {
                            if (featureValueConverted <= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                    else if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dSingle)
                    {
                        var featureValueConverted = Convert.ToSingle(featureValue);
                        var selectedValueConverted = Convert.ToSingle(selectValue);
                        if (operatorType == 0)
                        {
                            if (featureValueConverted > selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 1)
                        {
                            if (featureValueConverted < selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 2)
                        {
                            if (featureValueConverted == selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 3)
                        {
                            if (featureValueConverted >= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 4)
                        {
                            if (featureValueConverted <= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                    else if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                    {
                        var featureValueConverted = Convert.ToDouble(featureValue);
                        var selectedValueConverted = Convert.ToDouble(selectValue);
                        if (operatorType == 0)
                        {
                            if (featureValueConverted > selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 1)
                        {
                            if (featureValueConverted < selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 2)
                        {
                            if (featureValueConverted == selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 3)
                        {
                            if (featureValueConverted >= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 4)
                        {
                            if (featureValueConverted <= selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                        else if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                    else if (moMap.Layers.GetItem(selectedIndex).AttributeFields.GetItem(selectAttributeFieldsForm.SelectedFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dText)
                    {
                        var featureValueConverted = Convert.ToString(featureValue);
                        var selectedValueConverted = Convert.ToString(selectValue);
                        if (operatorType == 5)
                        {
                            if (featureValueConverted != selectedValueConverted)
                            {
                                moMap.Layers.GetItem(selectedIndex).SelectedFeatures.Add(moMap.Layers.GetItem(selectedIndex).Features.GetItem(i));
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("输入的值有误，请重新选择。");
            }
            moMap.RedrawMap();
            moMap.RedrawTrackingShapes();
            checkedListBox1.SelectedIndex = selectedIndex;
            if (propertyTableForm != null)
            {
                propertyTableForm.CanAffectLayerSelection = false;
                propertyTableForm.ReloadPropList();
                propertyTableForm.CanAffectLayerSelection = true;
            }
        }

        private void 查看操作指南ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("首先，您需要在左侧选定一个想要编辑的图层。\n之后，请选择您需要编辑的对象，之后开始编辑节点。\n之后，在新对话框上，您可以看到一些新的功能，包括几何选点、删除节点、删除部分等。");
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
                //绘制高亮顶点
                if (vertexEditorForm != null)
                {
                    drawingTool.DrawPoints(vertexEditorForm.HighlightedPoints, mEditingHighlightedVertexSymbol);
                }
            }
            else if (mEditingGeometry.GetType() == typeof(MyMapObjects.moMultiPolyline))
            {
                MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mEditingGeometry;
                //绘制边界
                drawingTool.DrawMultiPolyline(sMultiPolyline, mEditingPolygonSymbol.Outline);
                //绘制顶点手柄
                Int32 sPartCount = sMultiPolyline.Parts.Count;
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sMultiPolyline.Parts.GetItem(i);
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
                //绘制高亮顶点
                if (vertexEditorForm != null)
                {
                    drawingTool.DrawPoints(vertexEditorForm.HighlightedPoints, mEditingHighlightedVertexSymbol);
                }
            }
            else if (mEditingGeometry.GetType() == typeof(MyMapObjects.moPoint))
            {
                MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mEditingGeometry;
                drawingTool.DrawPoint(sPoint, mEditingVertexSymbol);
                //绘制高亮顶点
                if (vertexEditorForm != null)
                {
                    drawingTool.DrawPoints(vertexEditorForm.HighlightedPoints, mEditingHighlightedVertexSymbol);
                }
            }
        }

        private void RedrawMapForVertexEditing()
        {
            moMap.RedrawMap();
            moMap.RedrawTrackingShapes();
        }

        private void 打开节点编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还未选择任何的图层，请在左侧点击图层以选择。");
                return;
            }
            var layerIndex = checkedListBox1.SelectedIndex;
            //查找多边形图层
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(layerIndex);
            //是否有且只有一个选中的图形
            if (sLayer.SelectedFeatures.Count != 1)
            {
                MessageBox.Show("您选中了不止一个要素或者没有选择要素，请您重新选择仅一个要素。");
                checkedListBox1.SelectedIndex = layerIndex;
                return;
            }
            mEditingGeometry = sLayer.SelectedFeatures.GetItem(0).Geometry;
            //设置操作类型
            mMapOpStyle = 8;
            //地图重回跟踪层
            moMap.RedrawTrackingShapes();

            vertexEditorForm = new VertexEditor(RedrawMapForVertexEditing, sLayer.SelectedFeatures.GetItem(0), CallBackMovingVertex, CallBackNewPartMoMap, AddNewVertexMoMap, GeomSelect, ExitEditing);
            vertexEditorForm.Show();
            checkedListBox1.SelectedIndex=layerIndex;
        }

        private void CallBackMovingVertex()
        {
            mMapOpStyle = 20;
        }

        private void CallBackNewPartMoMap()
        {
            mMapOpStyle = 21;

        }

        private void AddNewVertexMoMap()
        {
            mMapOpStyle = 22;
        }

        private void GeomSelect()
        {
            mMapOpStyle = 23;
        }

        private void ExitEditing()
        {
            mEditingGeometry = null;
            if (vertexEditorForm != null)
            {
                vertexEditorForm.Close();
            }
            RedrawMapForVertexEditing();
        }

        private void 简单渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSimpleRenderer_Click(moMap, e);
        }

        private void 唯一值渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUniqueValue_Click(moMap, e);
        }

        private void 分级渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnClassBreaks_Click(moMap, e);
        }

        private void 更改图层注记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还没有在左侧选择任何图层，单击图层文本即可选取。");
                return;
            }
            identifySelectedLayerIndex = checkedListBox1.SelectedIndex;
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(identifySelectedLayerIndex); //获得选中的图层
            MyMapObjects.moLabelRenderer sLabelRenderer = new MyMapObjects.moLabelRenderer();
            LabelRenderer cForm = new LabelRenderer(sLayer, sLabelRenderer, moMap);
            cForm.ShowDialog();



        }

        //private void ToolStripMenuItem_Click(object sender, EventArgs e)
        //{



        //}
    

        private void 导出地图为bitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var scale = Microsoft.VisualBasic.Interaction.InputBox("请输入解析度，即1图上像素代表多少屏幕像素", "解析度输入");
                var scaleDouble = Convert.ToDouble(scale);
                if (scaleDouble < 1)
                {
                    MessageBox.Show("解析度请不要小于1，请重新操作。");
                    return;
                }
                SaveFileDialog sDialog = new SaveFileDialog();
                sDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";
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
                moMap.DrawCurrentExtentWithScale(scaleDouble).Save(sFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现问题，请重试。问题：" + ex.Message);

            }

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 坐标系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckAllLayerProjectionLngLatIsValid())
            {
                MessageBox.Show("您的地理数据中有一些要素超过了经度在-180到180度，纬度在-90到90度之间的经纬度限制，无法从当前的投影直角坐标系转换到经纬度坐标，再切换到其它坐标系。您需要首先解决这些要素，然后重新转换。");
                return;
            }
            var coordinateSelectForm = new CoordinateSystemChange();
            coordinateSelectForm.ShowDialog();
            int coordinateSystemSelectIndex = coordinateSelectForm.coordinateSystemSelected;
            for(int i = 0; i < moMap.Layers.Count; i++)
            {
                TransferCurrentLayerProjectionToLngLat(moMap.Layers.GetItem(i));
            }
            if (coordinateSystemSelectIndex == 0)
            {
                string sProjCSName = "WGS_1984";
                string sGeoCSName = "WGS_1984";
                string sDatumName = "WGS_1984";
                string sSpheroidName = "WGS_1984";
                double sSemiMajor = 6378137;
                double sInverseFlattening = 298.257;
                double sOriginLatitude = 0;
                double sCentralMeridian = 0;
                double sFalseEasting = 0;
                double sFalseNorthing = 0;
                double sScaleFactor = 1;
                double sStandardParallelOne = 0;
                double sStandardParallelTwo = 0;
                MyMapObjects.moLinearUnitConstant sLinearUnit = MyMapObjects.moLinearUnitConstant.Meter;
                MyMapObjects.moProjectionTypeConstant sProjType = MyMapObjects.moProjectionTypeConstant.None;
                var projectionCS = new MyMapObjects.moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                    sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                    sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);
                moMap.ProjectionCS = projectionCS;
            }
            else if (coordinateSystemSelectIndex == 1)
            {
                string sProjCSName = "Beijing54 Lambert Conformal Conic 2SP";
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
                MyMapObjects.moLinearUnitConstant sLinearUnit = MyMapObjects.moLinearUnitConstant.Meter;
                MyMapObjects.moProjectionTypeConstant sProjType = MyMapObjects.moProjectionTypeConstant.Lambert_Conformal_Conic_2SP;
                var projectionCS=new MyMapObjects.moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                    sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                    sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);
                moMap.ProjectionCS = projectionCS;
            }
            else if (coordinateSystemSelectIndex == 2)
            {
                string sProjCSName = "Mercator";
                string sGeoCSName = "WGS_1984";
                string sDatumName = "WGS_1984";
                string sSpheroidName = "WGS_1984";
                double sSemiMajor = 6378137;
                double sInverseFlattening = 298.257;
                double sOriginLatitude = 0;
                double sCentralMeridian = 0;
                double sFalseEasting = 0;
                double sFalseNorthing = 0;
                double sScaleFactor = 1;
                double sStandardParallelOne = 0;
                double sStandardParallelTwo = 0;
                MyMapObjects.moLinearUnitConstant sLinearUnit = MyMapObjects.moLinearUnitConstant.Meter;
                MyMapObjects.moProjectionTypeConstant sProjType = MyMapObjects.moProjectionTypeConstant.Mercator;
                var projectionCS = new MyMapObjects.moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                    sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                    sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);
                moMap.ProjectionCS = projectionCS;
            }
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                TransferCurrentLayerProjectionToMap(moMap.Layers.GetItem(i));
            }
            moMap.FullExtent();
        }

        private bool CheckAllLayerProjectionLngLatIsValid()
        {
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                var layer = moMap.Layers.GetItem(i);
                if (!CheckCurrentLayerProjectionIsValid(layer))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckCurrentLayerProjectionIsValid(MyMapObjects.moMapLayer layer)
        {
            for (int i = 0; i < layer.Features.Count; i++)
            {
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    var geometry = (MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry);
                        if (pointCurrentCoordinate.X < -180 || pointCurrentCoordinate.X > 180 || pointCurrentCoordinate.Y <= -90 || pointCurrentCoordinate.Y >= 90)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    var geometry = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry.Parts.GetItem(j).GetItem(k));
                                if (pointCurrentCoordinate.X < -180 || pointCurrentCoordinate.X > 180 || pointCurrentCoordinate.Y <= -90 || pointCurrentCoordinate.Y >= 90)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    var geometry = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry.Parts.GetItem(j).GetItem(k));
                                if (pointCurrentCoordinate.X < -180 || pointCurrentCoordinate.X > 180 || pointCurrentCoordinate.Y <= -90 || pointCurrentCoordinate.Y >= 90)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void TransferCurrentLayerProjectionToLngLat(MyMapObjects.moMapLayer layer)
        {
            for (int i = 0; i < layer.Features.Count; i++)
            {
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    var geometry = (MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry);
                        geometry.X = pointCurrentCoordinate.X;
                        geometry.Y = pointCurrentCoordinate.Y;
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    var geometry = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry.Parts.GetItem(j).GetItem(k));
                                geometry.Parts.GetItem(j).GetItem(k).X = pointCurrentCoordinate.X;
                                geometry.Parts.GetItem(j).GetItem(k).Y = pointCurrentCoordinate.Y;
                            }
                            geometry.Parts.GetItem(j).UpdateExtent();
                        }
                        geometry.UpdateExtent();
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    var geometry = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToLngLat(geometry.Parts.GetItem(j).GetItem(k));
                                geometry.Parts.GetItem(j).GetItem(k).X = pointCurrentCoordinate.X;
                                geometry.Parts.GetItem(j).GetItem(k).Y = pointCurrentCoordinate.Y;
                            }
                            geometry.Parts.GetItem(j).UpdateExtent();
                        }
                        geometry.UpdateExtent();
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                layer.UpdateExtent();
            }
        }

        private void TransferCurrentLayerProjectionToMap(MyMapObjects.moMapLayer layer)
        {
            for (int i = 0; i < layer.Features.Count; i++)
            {
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    var geometry = (MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        var pointCurrentCoordinate = moMap.ProjectionCS.TransferToProjCo(geometry);
                        geometry.X = pointCurrentCoordinate.X;
                        geometry.Y = pointCurrentCoordinate.Y;
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    var geometry = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToProjCo(geometry.Parts.GetItem(j).GetItem(k));
                                geometry.Parts.GetItem(j).GetItem(k).X = pointCurrentCoordinate.X;
                                geometry.Parts.GetItem(j).GetItem(k).Y = pointCurrentCoordinate.Y;
                            }
                            geometry.Parts.GetItem(j).UpdateExtent();
                        }
                        geometry.UpdateExtent();
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    var geometry = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                    try
                    {
                        for (int j = 0; j < geometry.Parts.Count; j++)
                        {
                            for (int k = 0; k < geometry.Parts.GetItem(j).Count; k++)
                            {
                                var pointCurrentCoordinate = moMap.ProjectionCS.TransferToProjCo(geometry.Parts.GetItem(j).GetItem(k));
                                geometry.Parts.GetItem(j).GetItem(k).X = pointCurrentCoordinate.X;
                                geometry.Parts.GetItem(j).GetItem(k).Y = pointCurrentCoordinate.Y;
                            }
                            geometry.Parts.GetItem(j).UpdateExtent();
                        }
                        geometry.UpdateExtent();
                    }
                    catch
                    {
                        MessageBox.Show("内部错误，坐标转换失败；建议您重启本程序以避免出现未定义的问题。");
                    }
                }
                layer.UpdateExtent();
            }
        }

        private void 土木GISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(moMap.ProjectionCS.ProjType == MyMapObjects.moProjectionTypeConstant.None))
            {
                MessageBox.Show("根据IETF官方范式要求，本程序的标准格式——GeoJSON，的坐标系只能为WGS84经纬度，请先将坐标系统调整为WGS84后再打开。");
                return;
            }
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
            DataIOTools.LoadTuMuGISProjectFileToMoMapObject(moMap, sFileName);
            moMap.FullExtent();
            moMap.RedrawMap();
            moMap_LayerChanged(null);


        }

        private void 保存当前项目为土木GIS工程文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(moMap.ProjectionCS.ProjType == MyMapObjects.moProjectionTypeConstant.None))
            {
                MessageBox.Show("根据IETF官方范式要求，本程序的标准格式——GeoJSON，的坐标系只能为WGS84经纬度，请先将坐标系统调整为WGS84后再保存。");
                return;
            }
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "土木GIS Project Files (*.tmproj)|*.tmproj";
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
            DataIOTools.SaveAsTuMuGISProjectFile(moMap, sFileName);
        }

        private void moMap_Load(object sender, EventArgs e)
        {

        }
    }
}
