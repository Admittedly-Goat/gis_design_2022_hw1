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
    public partial class ClassBreaksLine : Form
    {
        private MyMapObjects.moMapLayer moMapLayer;
        private MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol;
        private MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer;
        public ClassBreaksLine(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moClassBreaksRenderer moClassBreaksRenderer, MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleLineSymbol = moSimpleLineSymbol;
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
                    int sValue = (int)moMapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                    sValues.Add(sValue);
                }
                //获取最小最大值
                int sMinValue = sValues.Min();
                int sMaxValue = sValues.Max();
                for (Int32 i = 0; i < num; i++)
                {
                    int sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / num;
                    MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                    moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                }
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
                    MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                    moClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                }
            }
            moClassBreaksRenderer.RampSize(moSimpleLineSymbol.Size);
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                moClassBreaksRenderer.RampColor(colorDialog1.Color, colorDialog1.Color);
            }
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
