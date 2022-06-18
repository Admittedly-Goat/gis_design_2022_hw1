using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class SymbolFill : Form
    {
        private readonly MyMapObjects.moSimpleLineSymbol _simpleLineSymbol;
        private readonly MyMapObjects.moSimpleFillSymbol _simpleFillSymbol;
        private readonly RenderLayer _fatherForm;
        private readonly MyMapObjects.moRendererTypeConstant _renderType = MyMapObjects.moRendererTypeConstant.Simple;
        private readonly int _index;
        public SymbolFill(RenderLayer fatherForm, MyMapObjects.moSimpleFillSymbol _simpleFillSymbol, MyMapObjects.moRendererTypeConstant renderType = MyMapObjects.moRendererTypeConstant.Simple, int index = 0)
        {
            //初始化变量

            _fatherForm = fatherForm;
            this._simpleFillSymbol = _simpleFillSymbol;
            _simpleLineSymbol = this._simpleFillSymbol.Outline;
            _renderType = renderType;
            _index = index;

            InitializeComponent();
            //显示组件属性
            buttonShowSymbol.BackColor = this._simpleFillSymbol.Color;
            buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
            buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;


            //初始化子组件的属性
            colorComboBox1.SelectedColor = _simpleLineSymbol.Color;
            comboBoxStyle.Text = _simpleLineSymbol.Style.ToString();
            numericUpDownSize.Value = (decimal)_simpleLineSymbol.Size;
            colorComboBoxBackColor.SelectedColor = _simpleFillSymbol.Color;
        }

        private void SymbolPoint_Load(object sender, EventArgs e)
        {

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
            if (style == "Solid")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            }
            else
            {
                return style == "Dash"
                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.Dash
                    : style == "Dot"
                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.Dot
                                    : style == "DashDot"
                                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot
                                                    : style == "DashDotDot"
                                                                    ? MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot
                                                                    : MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            }
        }

        private string getSymbolStyleString(string style)
        {
            if (style == "Solid")
            {

                return "———————————";
            }
            else
            {
                return style == "Dash"
                    ? "-----------"
                    : style == "Dot"
                                    ? "••••••••••••••••"
                                    : style == "DashDot" ? "-•-•-•-•-•-•-•-•" : style == "DashDotDot" ? "-••-••-••-••-••" : "——————————————";
            }


        }
        /// <summary>
        /// 更改按下时保存当前设置，所以在确定之前一定要更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChange_Click(object sender, EventArgs e)
        {
            //边缘
            _simpleLineSymbol.Color = colorComboBox1.SelectedColor;
            string style = comboBoxStyle.Text;
            _simpleLineSymbol.Style = getSymbolStyleConstant(style);
            _simpleLineSymbol.Size = (double)numericUpDownSize.Value;
            //面
            _simpleFillSymbol.Outline = _simpleLineSymbol;
            _simpleFillSymbol.Color = colorComboBoxBackColor.SelectedColor;



        }


        /// <summary>
        /// 确定按钮按下改变父窗口中的显示符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {

            if (_renderType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                Button buttonShowSymbol = (Button)_fatherForm.Controls.Find("buttonSimpleShowSymbol", true)[0];
                buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;
                buttonShowSymbol.BackColor = _simpleFillSymbol.Color;
                Close();

            }
            else if (_renderType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewUniqueValue", true)[0];
                sDataGridView.Rows[_index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[_index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134); ;
                sDataGridView.Rows[_index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                sDataGridView.Rows[_index].Cells[0].Style.BackColor = _simpleFillSymbol.Color;
                Close();
            }
            else if (_renderType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewClassBreak", true)[0];
                sDataGridView.Rows[_index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[_index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134); ;
                sDataGridView.Rows[_index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                sDataGridView.Rows[_index].Cells[0].Style.BackColor = _simpleFillSymbol.Color;
                Close();

            }

        }

        private void colorComboBoxBackColor_ColorChanged(object sender, ColorChangeArgs e)
        {
            buttonShowSymbol.BackColor = colorComboBoxBackColor.SelectedColor;

        }
    }
}
