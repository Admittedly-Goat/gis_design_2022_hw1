using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class SymbolLine : Form
    {
        private readonly MyMapObjects.moSimpleLineSymbol _simpleLineSymbol;
        private readonly RenderLayer _fatherForm;
        private readonly MyMapObjects.moRendererTypeConstant _renderType;
        private readonly int _index;
        public SymbolLine(RenderLayer fatherForm, MyMapObjects.moSimpleLineSymbol _simpleLineSymbol, MyMapObjects.moRendererTypeConstant renderType = MyMapObjects.moRendererTypeConstant.Simple, int index = 0)
        {
            //初始化变量
            this._simpleLineSymbol = _simpleLineSymbol;
            _fatherForm = fatherForm;
            _renderType = renderType;
            _index = index;

            InitializeComponent();
            //显示组件属性
            buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
            buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;

            //初始化子组件的属性
            colorComboBox1.SelectedColor = _simpleLineSymbol.Color;
            comboBoxStyle.Text = _simpleLineSymbol.Style.ToString();
            numericUpDownSize.Value = (decimal)_simpleLineSymbol.Size;
        }

        private void SymbolPoint_Load(object sender, EventArgs e)
        {

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            _simpleLineSymbol.Color = colorComboBox1.SelectedColor;
            string style = comboBoxStyle.Text;
            _simpleLineSymbol.Style = getSymbolStyleConstant(style);
            _simpleLineSymbol.Size = (double)numericUpDownSize.Value;
        }

        /// <summary>
        /// 更改颜色的时候显示更改·
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorComboBox1_ColorChanged(object sender, ColorChangeArgs e)
        {
            buttonShowSymbol.ForeColor = colorComboBox1.SelectedColor;
        }

        /// <summary>
        /// 更改样式的时候显示更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxStyle_TextChanged(object sender, EventArgs e)
        {
            string style = comboBoxStyle.Text;
            buttonShowSymbol.Text = getSymbolStyleString(style);

        }

        /// <summary>
        /// 更改符号大小的时候显示更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            float size = (float)numericUpDownSize.Value * (float)2.83 * 10;
            buttonShowSymbol.Font = new System.Drawing.Font("宋体", size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        }


        private MyMapObjects.moSimpleLineSymbolStyleConstant getSymbolStyleConstant(string style)
        {
            return style == "Solid"
                ? MyMapObjects.moSimpleLineSymbolStyleConstant.Solid
                : style == "Dash"
                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.Dash
                    : style == "Dot"
                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.Dot
                                    : style == "DashDot"
                                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot
                                                    : style == "DashDotDot"
                                                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot
                                                                    : MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
        }

        private string getSymbolStyleString(string style)
        {
            return style == "Solid"
                ? "-----------"
                : style == "Dash"
                    ? "———————————"
                    : style == "Dot"
                                    ? "••••••••••••••••"
                                    : style == "DashDot" ? "-•-•-•-•-•-•-•-•" : style == "DashDotDot" ? "-••-••-••-••-••" : "——————————————";


        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (_renderType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                Button buttonShowSymbol = (Button)_fatherForm.Controls.Find("buttonSimpleShowSymbol", true)[0];
                buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;
                Close();
            }
            else if (_renderType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewUniqueValue", true)[0];
                sDataGridView.Rows[_index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[_index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134); ;
                sDataGridView.Rows[_index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                Close();

            }
            else if (_renderType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewClassBreak", true)[0];
                sDataGridView.Rows[_index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[_index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134); ;
                sDataGridView.Rows[_index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                Close();

            }

        }

        private void SymbolLine_Load(object sender, EventArgs e)
        {

        }
    }
}
