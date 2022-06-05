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
    public partial class SimpleRendererPoint : Form
    {
        private MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol;
        public SimpleRendererPoint(MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol)
        {
            InitializeComponent();
            this.moSimpleMarkerSymbol = moSimpleMarkerSymbol;
            if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Circle)
                Circle.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle)
                SolidCircle.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Triangle)
                Triangle.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidTriangle)
                SolidTriangle.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Square)
                Square.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare)
                SolidSquare.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleDot)
                CircleDot.Checked = true;
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleCircle)
                CircleCircle.Checked = true;
            string size = moSimpleMarkerSymbol.Size.ToString();
            textBox1.Text = size;
        }

        //确定
        private void button1_Click(object sender, EventArgs e)
        {
            if (Circle.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Circle;
            else if (SolidCircle.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle;
            else if (Triangle.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Triangle;
            else if (SolidTriangle.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidTriangle;
            else if (Square.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Square;
            else if (SolidSquare.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            else if (CircleDot.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleDot;
            else if (CircleCircle.Checked)
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleCircle;
            moSimpleMarkerSymbol.Size = float.Parse(textBox1.Text);
            
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                moSimpleMarkerSymbol.Color = colorDialog1.Color;
            }
            this.Close();
        }
        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
