using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class UniqueValuePoint : Form
    {
        private readonly MyMapObjects.moMapLayer moMapLayer;
        private readonly MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol;
        private readonly MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer;
        public UniqueValuePoint(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer, MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleMarkerSymbol = moSimpleMarkerSymbol;
            this.moUniqueValueRenderer = moUniqueValueRenderer;
            int count = moMapLayer.AttributeFields.Count;
            for (int i = 0; i < count; i++)
            {
                _ = listBox1.Items.Add(moMapLayer.AttributeFields.GetItem(i).Name);
            }
            if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Circle)
            {
                Circle.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle)
            {
                SolidCircle.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Triangle)
            {
                Triangle.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidTriangle)
            {
                SolidTriangle.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.Square)
            {
                Square.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare)
            {
                SolidSquare.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleDot)
            {
                CircleDot.Checked = true;
            }
            else if (moSimpleMarkerSymbol.Style == MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleCircle)
            {
                CircleCircle.Checked = true;
            }

            string size = moSimpleMarkerSymbol.Size.ToString();
            textBox1.Text = size;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Circle.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Circle;
            }
            else if (SolidCircle.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle;
            }
            else if (Triangle.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Triangle;
            }
            else if (SolidTriangle.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidTriangle;
            }
            else if (Square.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Square;
            }
            else if (SolidSquare.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            }
            else if (CircleDot.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleDot;
            }
            else if (CircleCircle.Checked)
            {
                moSimpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleCircle;
            }

            moSimpleMarkerSymbol.Size = float.Parse(textBox1.Text);
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("未选择绑定字段");
            }
            else
            {
                moUniqueValueRenderer.Field = listBox1.SelectedItem.ToString();
                List<object> sNames = new List<object>();
                int sFeatureCount = moMapLayer.Features.Count;
                for (int i = 0; i <= sFeatureCount - 1; i++)
                {
                    object sName = moMapLayer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                    sNames.Add(sName);
                }
                _ = sNames.Distinct().ToList();
                int sValueCount = sNames.Count;
                for (int i = 0; i <= sValueCount - 1; i++)
                {
                    MyMapObjects.moSimpleMarkerSymbol sSymbol = new MyMapObjects.moSimpleMarkerSymbol
                    {
                        Style = moSimpleMarkerSymbol.Style,
                        Size = moSimpleMarkerSymbol.Size
                    };
                    moUniqueValueRenderer.AddUniqueValue(sNames[i].ToString(), sSymbol);
                }
                moUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleMarkerSymbol();
                Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
