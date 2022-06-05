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
    public partial class ClassBreaksPolygon : Form
    {
        private MyMapObjects.moMapLayer moMapLayer;
        private MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol;
        private MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer;
        public ClassBreaksPolygon(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer, MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleFillSymbol = moSimpleFillSymbol;
            this.moClassBreaksRenderer = moClassBreaksRenderer;
            int count = moMapLayer.AttributeFields.Count;
            for (int i = 0; i < count; i++)
            {
                listBox1.Items.Add(moMapLayer.AttributeFields.GetItem(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取级数
            int num = int.Parse(textBox1.Text);
            if(listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("未选择绑定字段");
            }
            else
            {
                moClassBreaksRenderer.Field = listBox1.SelectedItem.ToString();
                //读出所有值
                Int32 sFieldIndex = moMapLayer.AttributeFields.FindField(moClassBreaksRenderer.Field);
                if (sFieldIndex < 0)
                {
                    return;
                }
                if (moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt16
                    || moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt32
                    || moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                {
                    Int32 sFeatureCount = moMapLayer.Features.Count;
                    List<int> sValues = new List<int>();
                    for (Int32 i = 0; i < sFeatureCount - 1; i++)
                    {
                        int sValue = int.Parse(moMapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());
                        sValues.Add(sValue);
                    }
                    //获取最小最大值
                    int sMinValue = sValues.Min();
                    int sMaxValue = sValues.Max();
                    for (Int32 i = 0; i < num; i++)
                    {
                        int sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / num;
                        MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                        moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    }
                    //生成渐变色
                    Color sStartColor = Color.FromArgb(255, 255, 192, 192);
                    Color sEndColor = Color.Maroon;
                    moClassBreaksRenderer.RampColor(sStartColor, sEndColor);
                    this.Close();
                }

                else if (moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dSingle ||
                    moMapLayer.AttributeFields.GetItem(sFieldIndex).ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                {
                    Int32 sFeatureCount = moMapLayer.Features.Count;
                    List<double> sValues = new List<double>();
                    for (Int32 i = 0; i < sFeatureCount - 1; i++)
                    {
                        double sValue = (float)moMapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                        sValues.Add(sValue);
                    }
                    //获取最小最大值
                    double sMinValue = sValues.Min();
                    double sMaxValue = sValues.Max();
                    for (Int32 i = 0; i < num; i++)
                    {
                        double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / num;
                        MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                        moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    }
                    //生成渐变色
                    Color sStartColor = Color.FromArgb(255, 255, 192, 192);
                    Color sEndColor = Color.Maroon;
                    moClassBreaksRenderer.RampColor(sStartColor, sEndColor);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("所选字段为文本类型，无法进行分级渲染，请选择其他字段");
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
