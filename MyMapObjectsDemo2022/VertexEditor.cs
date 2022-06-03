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
        private MyMapObjects.moFeature SelectedFeature;
        private void ReloadAllPartsAndPoints()
        {
            treeView1.Nodes.Clear();
            if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                treeView1.Nodes.Add("[0] 第1节点");
            }else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                var multiPolyline = (MyMapObjects.moMultiPolyline)SelectedFeature.Geometry;
                for(int i = 0; i < multiPolyline.Parts.Count; i++)
                {
                    treeView1.Nodes.Add($"[{i}] 第{i + 1}部分");
                    for(int j = 0; j < multiPolyline.Parts.GetItem(i).Count; j++)
                    {
                        treeView1.Nodes[i].Nodes.Add($"[{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
            else if (SelectedFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                var multiPolygon = (MyMapObjects.moMultiPolygon)SelectedFeature.Geometry;
                for (int i = 0; i < multiPolygon.Parts.Count; i++)
                {
                    treeView1.Nodes.Add($"[{i}] 第{i + 1}部分");
                    for (int j = 0; j < multiPolygon.Parts.GetItem(i).Count; j++)
                    {
                        treeView1.Nodes[i].Nodes.Add($"[{i}-{j}] 第{j + 1}节点");
                    }
                }
            }
        }
        public VertexEditor(Action redrawMap, MyMapObjects.moFeature feature)
        {
            InitializeComponent();
            HighlightedPoints = new MyMapObjects.moPoints();
            RedrawMap = redrawMap;
            SelectedFeature = feature;
            ReloadAllPartsAndPoints();
        }

        private void VertexEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
