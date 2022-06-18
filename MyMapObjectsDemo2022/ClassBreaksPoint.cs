using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{

    public partial class ClassBreaksPoint : Form
    {
        private readonly MyMapObjects.moMapLayer moMapLayer;
        private readonly MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol;
        private readonly MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer;
        public ClassBreaksPoint(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer, MyMapObjects.moSimpleMarkerSymbol moSimpleMarkerSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleMarkerSymbol = moSimpleMarkerSymbol;
            this.moClassBreaksRenderer = moClassBreaksRenderer;
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
        }

        private void button1_Click(object sender, EventArgs e)
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
            //获取级数
            int num = int.Parse(textBox1.Text);
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("未选择绑定字段");
            }
            else
            {
                moClassBreaksRenderer.Field = listBox1.SelectedItem.ToString();
                //读出所有值
                int sFieldIndex = moMapLayer.AttributeFields.FindField(moClassBreaksRenderer.Field);
                if (sFieldIndex < 0)
                {
                    return;
                }
                if (moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt16
                    || moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt32
                    || moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                {
                    int sFeatureCount = moMapLayer.Features.Count;
                    List<int> sValues = new List<int>();
                    for (int i = 0; i < sFeatureCount - 1; i++)
                    {
                        int sValue = int.Parse(moMapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());
                        sValues.Add(sValue);
                    }
                    //获取最小最大值
                    int sMinValue = sValues.Min();
                    int sMaxValue = sValues.Max();
                    for (int i = 0; i < num; i++)
                    {
                        int sValue = sMinValue + ((sMaxValue - sMinValue) * (i + 1) / num);
                        MyMapObjects.moSimpleMarkerSymbol sSymbol = new MyMapObjects.moSimpleMarkerSymbol
                        {
                            Style = moSimpleMarkerSymbol.Style
                        };
                        moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    }
                    moClassBreaksRenderer.RampSize(moSimpleMarkerSymbol.Size);
                    //显示颜色对话框
                    DialogResult dr = colorDialog1.ShowDialog();
                    //选择符号颜色
                    if (dr == DialogResult.OK)
                    {
                        moClassBreaksRenderer.RampColor(colorDialog1.Color, colorDialog1.Color);
                    }
                    Close();
                }

                else if (moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dSingle ||
                    moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                {
                    int sFeatureCount = moMapLayer.Features.Count;
                    List<double> sValues = new List<double>();
                    for (int i = 0; i < sFeatureCount - 1; i++)
                    {
                        double sValue = (float)moMapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                        sValues.Add(sValue);
                    }
                    //获取最小最大值
                    double sMinValue = sValues.Min();
                    double sMaxValue = sValues.Max();
                    for (int i = 0; i < num; i++)
                    {
                        double sValue = sMinValue + ((sMaxValue - sMinValue) * (i + 1) / num);
                        MyMapObjects.moSimpleMarkerSymbol sSymbol = new MyMapObjects.moSimpleMarkerSymbol();
                        moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    }
                    moClassBreaksRenderer.RampSize(moSimpleMarkerSymbol.Size);
                    //显示颜色对话框
                    DialogResult dr = colorDialog1.ShowDialog();
                    //选择符号颜色
                    if (dr == DialogResult.OK)
                    {
                        moClassBreaksRenderer.RampColor(colorDialog1.Color, colorDialog1.Color);
                    }
                    Close();
                }
                else
                {
                    _ = MessageBox.Show("所选字段为文本类型，无法进行分级渲染，请选择其他字段");
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
