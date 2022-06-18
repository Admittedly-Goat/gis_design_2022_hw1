using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class UniqueValueLIne : Form
    {
        private readonly MyMapObjects.moMapLayer moMapLayer;
        private readonly MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol;
        private readonly MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer;
        public UniqueValueLIne(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer, MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleLineSymbol = moSimpleLineSymbol;
            this.moUniqueValueRenderer = moUniqueValueRenderer;
            int count = moMapLayer.AttributeFields.Count;
            for (int i = 0; i < count; i++)
            {
                _ = listBox1.Items.Add(moMapLayer.AttributeFields.GetItem(i).Name);
            }
            if (moSimpleLineSymbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Solid)
            {
                Solid.Checked = true;
            }
            else if (moSimpleLineSymbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Dash)
            {
                Dash.Checked = true;
            }
            else if (moSimpleLineSymbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Dot)
            {
                Dot.Checked = true;
            }
            else if (moSimpleLineSymbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot)
            {
                DashDot.Checked = true;
            }
            else if (moSimpleLineSymbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot)
            {
                DashDotDot.Checked = true;
            }

            string size = moSimpleLineSymbol.Size.ToString();
            textBox1.Text = size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Solid.Checked)
            {
                moSimpleLineSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            }
            else if (Dash.Checked)
            {
                moSimpleLineSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
            }
            else if (Dot.Checked)
            {
                moSimpleLineSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Dot;
            }
            else if (DashDot.Checked)
            {
                moSimpleLineSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot;
            }
            else if (DashDotDot.Checked)
            {
                moSimpleLineSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot;
            }

            moSimpleLineSymbol.Size = float.Parse(textBox1.Text);
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
                    MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol
                    {
                        Style = moSimpleLineSymbol.Style,
                        Size = moSimpleLineSymbol.Size
                    };
                    moUniqueValueRenderer.AddUniqueValue(sNames[i].ToString(), sSymbol);
                }
                moUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();
                Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
