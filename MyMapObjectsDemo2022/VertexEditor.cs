using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class VertexEditor : Form
    {
        public MyMapObjects.moPoints HighlightedPoints;
        Action RedrawMap;
        Action CallMoMapEditVertex;
        Action NewPartMoMap;
        private MyMapObjects.moPoint MovingVertex;
        private MyMapObjects.moFeature SelectedFeature;
        public MyMapObjects.moPoints NewPart;
        public int RemainingPartPointNumber;
        private void ReloadAllPartsAndPoints()
        {
            listBox1.Items.Clear();
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                listBox1.Items.Add("[0] 第1节点");
            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                var multiPolyline = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                for (int i = 0; i < multiPolyline.Parts.Count; i++)
                {
                    listBox1.Items.Add($"[{i}] 第{i + 1}部分");
                    for (int j = 0; j < multiPolyline.Parts.GetItem(i).Count; j++)
                    {
                        listBox1.Items.Add($"    [{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                var multiPolygon = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                for (int i = 0; i < multiPolygon.Parts.Count; i++)
                {
                    listBox1.Items.Add($"[{i}] 第{i + 1}部分");
                    for (int j = 0; j < multiPolygon.Parts.GetItem(i).Count; j++)
                    {
                        listBox1.Items.Add($"    [{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
        }
        public VertexEditor(Action redrawMap, MyMapObjects.moFeature feature, Action callMoMapEditVertex, Action newPartMoMap)
        {
            InitializeComponent();
            HighlightedPoints = new MyMapObjects.moPoints();
            RedrawMap = redrawMap;
            SelectedFeature = feature;
            ReloadAllPartsAndPoints();
            CallMoMapEditVertex = callMoMapEditVertex;
            NewPartMoMap = newPartMoMap;
        }

        private void VertexEditor_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("部分"))
            {
                int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    HighlightedPoints.Clear();
                    for (int i = 0; i < featureGeom.Parts.GetItem(selectedPartIndex).Count; i++)
                    {
                        HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(i));
                    }
                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    HighlightedPoints.Clear();
                    for (int i = 0; i < featureGeom.Parts.GetItem(selectedPartIndex).Count; i++)
                    {
                        HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(i));
                    }
                }
            }
            else if (selectedText.EndsWith("节点"))
            {
                HighlightedPoints.Clear();
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    var featureGeom = (MyMapObjects.moPoint)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom);
                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex));

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    HighlightedPoints.Add(featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex));

                }
            }
            HighlightedPoints.UpdateExtent();
            RedrawMap();
        }

        private void 移动节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("没有选择节点，无法修改。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("部分"))
            {
                MessageBox.Show("您选择的是部分，请选择节点。");
                return;
            }
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
                    var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    MovingVertex = featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex);

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    MovingVertex = featureGeom.Parts.GetItem(selectedPartIndex).GetItem(selectedPointIndex);

                }
            }
            CallMoMapEditVertex();
        }

        public void MoveVertexCallBack(MyMapObjects.moPoint newPointLocation)
        {
            MovingVertex.X = newPointLocation.X;
            MovingVertex.Y = newPointLocation.Y;
            HighlightedPoints.UpdateExtent();
            RedrawMap();
        }

        private void 删除部分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("没有选择部分，无法删除。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("部分"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    featureGeom.Parts.RemoveAt(selectedPartIndex);
                    featureGeom.UpdateExtent();

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    featureGeom.Parts.RemoveAt(selectedPartIndex);
                    featureGeom.UpdateExtent();
                }
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
            }
            else if (selectedText.EndsWith("节点"))
            {
                MessageBox.Show("您选择的是节点，请选择部分。");
            }
        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("没有选择节点，无法删除。");
                return;
            }
            string selectedText = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (selectedText.EndsWith("节点"))
            {
                if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                    if (featureGeom.Parts.GetItem(selectedPartIndex).Count <= 2)
                    {
                        MessageBox.Show("线段少于2个点，无法继续操作，请直接删除部分。");
                        return;
                    }
                    featureGeom.Parts.GetItem(selectedPartIndex).RemoveAt(selectedPointIndex);
                    featureGeom.UpdateExtent();

                }
                else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    int selectedPartIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[0]);
                    int selectedPointIndex = Convert.ToInt32(selectedText.Split(']')[0].Split('[')[1].Split('-')[1]);
                    var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                    if (featureGeom.Parts.GetItem(selectedPartIndex).Count <= 3)
                    {
                        MessageBox.Show("多边形少于3个点，无法继续操作，请直接删除部分。");
                        return;
                    }
                    featureGeom.Parts.GetItem(selectedPartIndex).RemoveAt(selectedPointIndex);
                    featureGeom.UpdateExtent();
                }
                else
                {
                    MessageBox.Show("单点无法添加新节点。");
                    return;
                }
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
            }
            else if (selectedText.EndsWith("部分"))
            {
                MessageBox.Show("您选择的是部分，请选择节点。");
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
                MessageBox.Show("单点无法添加新节点。");
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
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                var featureGeom = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                featureGeom.Parts.Add(NewPart);
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
                featureGeom.UpdateExtent();

            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                var featureGeom = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                featureGeom.Parts.Add(NewPart);
                HighlightedPoints.Clear();
                RedrawMap();
                ReloadAllPartsAndPoints();
                featureGeom.UpdateExtent();
            }

        }
    }
}
