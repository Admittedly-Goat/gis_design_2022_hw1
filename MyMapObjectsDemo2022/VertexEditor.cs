using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class VertexEditor : Form
    {
        public MyMapObjects.moPoints HighlightedPoints;
        private readonly Action RedrawMap;
        private readonly Action CallMoMapEditVertex;
        private readonly Action NewPartMoMap;
        private readonly Action AddNewVertexMoMap;
        private readonly Action FindByGeometryMoMap;
        private readonly Action CloseWindow;
        private MyMapObjects.moPoint MovingVertex;
        private readonly MyMapObjects.moFeature SelectedFeature;
        public MyMapObjects.moPoints NewPart;
        public int RemainingPartPointNumber;
        private int AddVertexPartIndex;
        private int AddVertexPointIndex;
        private void ReloadAllPartsAndPoints()
        {
            // 向列表中添加全部的节点和部分信息
            listBox1.Items.Clear();
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                _ = listBox1.Items.Add("[0] 第1节点");
            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline multiPolyline = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                for (int i = 0; i < multiPolyline.Parts.Count; i++)
                {
                    _ = listBox1.Items.Add($"[{i}] 第{i + 1}部分");
                    for (int j = 0; j < multiPolyline.Parts.GetItem(i).Count; j++)
                    {
                        _ = listBox1.Items.Add($"    [{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon multiPolygon = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                for (int i = 0; i < multiPolygon.Parts.Count; i++)
                {
                    _ = listBox1.Items.Add($"[{i}] 第{i + 1}部分");
                    for (int j = 0; j < multiPolygon.Parts.GetItem(i).Count; j++)
                    {
                        _ = listBox1.Items.Add($"    [{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
        }
        public VertexEditor(Action redrawMap, MyMapObjects.moFeature feature, Action callMoMapEditVertex, Action newPartMoMap, Action addNewVertexMoMap, Action findByGeometryMoMap, Action closeWindow)
        {
            InitializeComponent();
            HighlightedPoints = new MyMapObjects.moPoints();
            RedrawMap = redrawMap;
            SelectedFeature = feature;
            ReloadAllPartsAndPoints();
            CallMoMapEditVertex = callMoMapEditVertex;
            NewPartMoMap = newPartMoMap;
            AddNewVertexMoMap = addNewVertexMoMap;
            FindByGeometryMoMap = findByGeometryMoMap;
            CloseWindow = closeWindow;
        }

        private void VertexEditor_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // 更改选定值的行为
            if (listBox1.SelectedIndex == -1)
            {
                HighlightedPoints.Clear();
                HighlightedPoints.UpdateExtent();
                RedrawMap();
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            // 选中的为部分，高亮部分
            if (selectedText.EndsWith("部分"))
            {
                int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    HighlightedPoints.Clear();
                    for (int i = 0; i < featureGeom.Parts.GetItem(selectedPartIndex).Count; i++)
                    {
                        HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(i));
                    }
                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    HighlightedPoints.Clear();
                    for (int i = 0; i < featureGeom.Parts.GetItem(selectedPartIndex).Count; i++)
                    {
                        HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(i));
                    }
                }
            }
            // 选中的为节点，高亮该节点
            else if (selectedText.EndsWith("节点"))
            {
                HighlightedPoints.Clear();
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moPoint featureGeom = (MyMapObjects.moPoint)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom);
                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex));

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex));

                }
            }
            HighlightedPoints.UpdateExtent();
            RedrawMap();
        }

        private void 移动节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断输入有效性
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("没有选择节点，无法修改。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("部分"))
            {
                _ = MessageBox.Show("您选择的是部分，请选择节点。");
                return;
            }
            //选择的是节点，开启节点编辑回调
            else if (selectedText.EndsWith("节点"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MovingVertex = (MyMapObjects.moPoint)SelectedFeature.Geometry;
                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    MovingVertex = featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex);

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    MovingVertex = featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex);

                }
            }
            CallMoMapEditVertex();
        }

        public void MoveVertexCallBack(MyMapObjects.moPoint newPointLocation)
        {
            //节点编辑回调
            MovingVertex.X = newPointLocation.X;
            MovingVertex.Y = newPointLocation.Y;
            HighlightedPoints.UpdateExtent();
            RedrawMap();
        }

        private void 删除部分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //边界条件判断
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("没有选择部分，无法删除。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            //直接删除该部分在Feature内对应的列表
            if (selectedText.EndsWith("部分"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                    MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    featureGeom.Parts.RemoveAt(selectedPartIndex);
                    featureGeom.UpdateExtent();

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                    MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    featureGeom.Parts.RemoveAt(selectedPartIndex);
                    featureGeom.UpdateExtent();
                }
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
            }
            else if (selectedText.EndsWith("节点"))
            {
                _ = MessageBox.Show("您选择的是节点，请选择部分。");
            }
        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("没有选择节点，无法删除。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("节点"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    if (featureGeom.Parts.GetItem(selectedPartIndex).Count <= 2)
                    {
                        _ = MessageBox.Show("线段少于2个点，无法继续操作，请直接删除部分。");
                        return;
                    }
                    featureGeom.Parts.GetItem(selectedPartIndex).RemoveAt(selectedPointIndex);
                    featureGeom.UpdateExtent();

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    if (featureGeom.Parts.GetItem(selectedPartIndex).Count <= 3)
                    {
                        _ = MessageBox.Show("多边形少于3个点，无法继续操作，请直接删除部分。");
                        return;
                    }
                    featureGeom.Parts.GetItem(selectedPartIndex).RemoveAt(selectedPointIndex);
                    featureGeom.UpdateExtent();
                }
                else
                {
                    _ = MessageBox.Show("单点无法添加新节点。");
                    return;
                }
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
            }
            else if (selectedText.EndsWith("部分"))
            {
                _ = MessageBox.Show("您选择的是部分，请选择节点。");
            }
        }

        private void 添加部分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                RemainingPartPointNumber = 2;

            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                RemainingPartPointNumber = 3;
            }
            else
            {
                _ = MessageBox.Show("单点无法添加新节点。");
                return;
            }
            HighlightedPoints.Clear();
            RedrawMap();
            ReloadAllPartsAndPoints();
            NewPart = new MyMapObjects.moPoints();
            NewPartMoMap();
        }

        public void AddPartCallBack()
        {
            //添加部分的回调
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                featureGeom.Parts.Add(NewPart);
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
                featureGeom.UpdateExtent();

            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                featureGeom.Parts.Add(NewPart);
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
                featureGeom.UpdateExtent();
            }

        }

        private void 在上部ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddVertex(0);
        }

        private void 在下部ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddVertex(1);
        }

        private void AddVertex(int pointIndexDelta)
        {
            // 添加节点
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("节点"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    AddVertexPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    AddVertexPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]) + pointIndexDelta;

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    AddVertexPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    AddVertexPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]) + pointIndexDelta;
                }
                else
                {
                    _ = MessageBox.Show("单点无法添加新节点。");
                    return;
                }
                RedrawMap();
                AddNewVertexMoMap();
            }
            else if (selectedText.EndsWith("部分"))
            {
                _ = MessageBox.Show("您选择的是部分，请选择节点。");
            }
        }

        public void AddVertexCallBack(MyMapObjects.moPoint newPoint)
        {
            //添加节点的回调
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                featureGeom.Parts.GetItem(AddVertexPartIndex).Insert(AddVertexPointIndex, newPoint);

            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                featureGeom.Parts.GetItem(AddVertexPartIndex).Insert(AddVertexPointIndex, newPoint);
            }
            ReloadAllPartsAndPoints();
            RedrawMap();
        }

        private void 图上选点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //调用图上选点frmmain传来的回调
            ReloadAllPartsAndPoints();
            FindByGeometryMoMap();
        }

        public void SelectByGeometryCallBack(MyMapObjects.moPoint position)
        {
            //图上选点在frmmain里的的回调
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                int selectedPart = -1;
                int selectedPoint = -1;
                double currentMinDist = double.MaxValue;
                for (int i = 0; i < featureGeom.Parts.Count; i++)
                {
                    for (int j = 0; j < featureGeom.Parts.GetItem(i).Count; j++)
                    {
                        if (Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).X - position.X, 2) + Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).Y - position.Y, 2) < currentMinDist)
                        {
                            selectedPart = i;
                            selectedPoint = j;
                            currentMinDist = Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).X - position.X, 2) + Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).Y - position.Y, 2);
                        }
                    }
                }
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().StartsWith($"    [{selectedPart}-{selectedPoint}]"))
                    {
                        listBox1.SelectedIndex = i;
                        return;
                    }
                }

            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                int selectedPart = -1;
                int selectedPoint = -1;
                double currentMinDist = double.MaxValue;
                for (int i = 0; i < featureGeom.Parts.Count; i++)
                {
                    for (int j = 0; j < featureGeom.Parts.GetItem(i).Count; j++)
                    {
                        if (Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).X - position.X, 2) + Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).Y - position.Y, 2) < currentMinDist)
                        {
                            selectedPart = i;
                            selectedPoint = j;
                            currentMinDist = Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).X - position.X, 2) + Math.Pow(featureGeom.Parts.GetItem(i).GetItem(j).Y - position.Y, 2);
                        }
                    }
                }
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().StartsWith($"    [{selectedPart}-{selectedPoint}]"))
                    {
                        listBox1.SelectedIndex = i;
                        return;
                    }
                }
            }
            else
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void 结束并保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //需要调用自己的close，并且调用frmmain里的回调进行修正
            Close();
            CloseWindow();
        }

        private void VertexEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //本身这就是close函数，只需要调用frmmain的回调即可
            CloseWindow();
        }
    }
}
