using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class SimpleRendererLine : Form
    {
        private readonly MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol;
        public SimpleRendererLine(MyMapObjects.moSimpleLineSymbol moSimpleLineSymbol)
        {
            InitializeComponent();
            this.moSimpleLineSymbol = moSimpleLineSymbol;
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
            Close();
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                moSimpleLineSymbol.Color = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
