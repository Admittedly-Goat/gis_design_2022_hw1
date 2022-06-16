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
    public partial class SymbolPoint : Form
    {
        private MyMapObjects.moSimpleMarkerSymbol simpleMarkerSymbol;
        private RenderLayer _fatherForm;
        private MyMapObjects.moRendererTypeConstant _renderType;
        private int _index;
        public SymbolPoint(RenderLayer fatherForm,MyMapObjects.moSimpleMarkerSymbol simpleMarkerSymbol,MyMapObjects.moRendererTypeConstant renderType= MyMapObjects.moRendererTypeConstant.Simple,int index=0)
        {
            //初始化变量

            this.simpleMarkerSymbol = simpleMarkerSymbol;
            this._fatherForm = fatherForm;
            this._renderType = renderType;
            this._index = index;

            InitializeComponent();
            //显示组件属性
            this.buttonShowSymbol.Text = getSymbolStyleString(simpleMarkerSymbol.Style.ToString());
            this.buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size*(float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonShowSymbol.ForeColor = simpleMarkerSymbol.Color;

            //初始化子组件的属性
            this.colorComboBox1.SelectedColor = simpleMarkerSymbol.Color;
            this.comboBoxStyle.Text = simpleMarkerSymbol.Style.ToString();
            this.numericUpDownSize.Value = (decimal)simpleMarkerSymbol.Size;
        }

        private void SymbolPoint_Load(object sender, EventArgs e)
        {

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            this.simpleMarkerSymbol.Color = this.colorComboBox1.SelectedColor;
            string style = this.comboBoxStyle.Text;
            simpleMarkerSymbol.Style = getSymbolStyleConstant(style);
            this.simpleMarkerSymbol.Size = (double)this.numericUpDownSize.Value;
        }

        /// <summary>
        /// 更改颜色的时候显示更改·
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorComboBox1_ColorChanged(object sender, ColorChangeArgs e)
        {
            this.buttonShowSymbol.ForeColor = this.colorComboBox1.SelectedColor;
        }

        /// <summary>
        /// 更改样式的时候显示更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxStyle_TextChanged(object sender, EventArgs e)
        {
            string style = this.comboBoxStyle.Text;
            this.buttonShowSymbol.Text = getSymbolStyleString(style);

        }

        /// <summary>
        /// 更改符号大小的时候显示更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            float size = (float)this.numericUpDownSize.Value * (float)2.83;
            this.buttonShowSymbol.Font = new System.Drawing.Font("宋体", size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private MyMapObjects.moSimpleMarkerSymbolStyleConstant getSymbolStyleConstant(string style)
        {
            if (style == "Circle")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Circle;
                //this.buttonShowSymbol.Text = "○";
            }
            else if (style == "SolidCircle")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle;
                //this.buttonShowSymbol.Text = "●";
            }
            else if (style == "Triangle")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Triangle;
                //this.buttonShowSymbol.Text = "△";
            }

            else if (style == "SolidTriangle")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidTriangle;
                //this.buttonShowSymbol.Text = "▲";
            }

            else if (style == "Square")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.Square;
                //this.buttonShowSymbol.Text = "□";
            }

            else if (style == "SolidSquare")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
                //this.buttonShowSymbol.Text = "■";
            }
            else if (style == "CircleDot")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleDot;
                //this.buttonShowSymbol.Text = "☉";
            }
            else if (style == "CircleCircle")
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.CircleCircle;
                //this.buttonShowSymbol.Text = "◎";
            }
            else
            {
                return simpleMarkerSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidCircle; ;
            }

        }


        private string getSymbolStyleString(string style)
        {
            if (style == "Circle")
            {

                return this.buttonShowSymbol.Text = "○";
            }
            else if (style == "SolidCircle")
            {

                return this.buttonShowSymbol.Text = "●";
            }
            else if (style == "Triangle")
            {

                return this.buttonShowSymbol.Text = "△";
            }

            else if (style == "SolidTriangle")
            {

                return this.buttonShowSymbol.Text = "▲";
            }

            else if (style == "Square")
            {

                return this.buttonShowSymbol.Text = "□";
            }

            else if (style == "SolidSquare")
            {

                return this.buttonShowSymbol.Text = "■";
            }
            else if (style == "CircleDot")
            {

                return this.buttonShowSymbol.Text = "☉";
            }
            else if (style == "CircleCircle")
            {

                return this.buttonShowSymbol.Text = "◎";
            }
            else
            {
                return this.buttonShowSymbol.Text = "●";
            }


        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (this._renderType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                Button buttonShowSymbol = (Button)_fatherForm.Controls.Find("buttonSimpleShowSymbol", true)[0];
                buttonShowSymbol.Text = getSymbolStyleString(simpleMarkerSymbol.Style.ToString());
                buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                buttonShowSymbol.ForeColor = simpleMarkerSymbol.Color;
                this.Close();
            }
            else if (this._renderType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewUniqueValue", true)[0];
                sDataGridView.Rows[this._index].Cells[0].Value = getSymbolStyleString(simpleMarkerSymbol.Style.ToString());
                sDataGridView.Rows[this._index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))); ;
                sDataGridView.Rows[this._index].Cells[0].Style.ForeColor = simpleMarkerSymbol.Color;
                this.Close();

            }
            else if (this._renderType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewClassBreak", true)[0];
                sDataGridView.Rows[this._index].Cells[0].Value = getSymbolStyleString(simpleMarkerSymbol.Style.ToString());
                sDataGridView.Rows[this._index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))); ;
                sDataGridView.Rows[this._index].Cells[0].Style.ForeColor = simpleMarkerSymbol.Color;
                this.Close();
            }
            

        }
    }
}
