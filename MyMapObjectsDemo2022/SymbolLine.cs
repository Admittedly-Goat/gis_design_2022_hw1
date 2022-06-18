﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class SymbolLine : Form
    {
        private MyMapObjects.moSimpleLineSymbol _simpleLineSymbol;
        private RenderLayer _fatherForm;
        private MyMapObjects.moRendererTypeConstant _renderType;
        private int _index;
        public SymbolLine(RenderLayer fatherForm, MyMapObjects.moSimpleLineSymbol _simpleLineSymbol, MyMapObjects.moRendererTypeConstant renderType = MyMapObjects.moRendererTypeConstant.Simple, int index = 0)
        {
            //初始化变量
            this._simpleLineSymbol = _simpleLineSymbol;
            this._fatherForm = fatherForm;
            this._renderType = renderType;
            this._index = index;

            InitializeComponent();
            //显示组件属性
            this.buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
            this.buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83*10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;

            //初始化子组件的属性
            this.colorComboBox1.SelectedColor = _simpleLineSymbol.Color;
            this.comboBoxStyle.Text = _simpleLineSymbol.Style.ToString();
            this.numericUpDownSize.Value = (decimal)_simpleLineSymbol.Size;
        }

        private void SymbolPoint_Load(object sender, EventArgs e)
        {

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            this._simpleLineSymbol.Color = this.colorComboBox1.SelectedColor;
            string style = this.comboBoxStyle.Text;
            _simpleLineSymbol.Style = getSymbolStyleConstant(style);
            this._simpleLineSymbol.Size = (double)this.numericUpDownSize.Value;
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
            float size = (float)this.numericUpDownSize.Value * (float)2.83*10;
            this.buttonShowSymbol.Font = new System.Drawing.Font("宋体", size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }


        private MyMapObjects.moSimpleLineSymbolStyleConstant getSymbolStyleConstant(string style)
        {
            if(style == "Solid")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            }
            else if(style=="Dash")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
            }
            else if (style == "Dot")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.Dot;
            }
            else if (style == "DashDot")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot;
            }
            else if (style == "DashDotDot")
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot;
            }
            else
            {
                return MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            }
        }
        
        private string getSymbolStyleString(string style)
        {
            if (style == "Solid")
            {

                return "-----------";
            }
            else if (style == "Dash")
            {

                return "———————————";
            }
            else if (style == "Dot")
            {

                return "••••••••••••••••";
            }

            else if (style == "DashDot")
            {

                return "-•-•-•-•-•-•-•-•";
            }

            else if (style == "DashDotDot")
            {

                return "-••-••-••-••-••";
            }
            else
            {
                return "——————————————";
            }


        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (this._renderType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                Button buttonShowSymbol = (Button)_fatherForm.Controls.Find("buttonSimpleShowSymbol", true)[0];
                buttonShowSymbol.Text = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                buttonShowSymbol.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                buttonShowSymbol.ForeColor = _simpleLineSymbol.Color;
                this.Close();
            }
            else if (this._renderType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewUniqueValue", true)[0];
                sDataGridView.Rows[this._index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[this._index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))); ;
                sDataGridView.Rows[this._index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                this.Close();

            }
            else if (this._renderType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                DataGridView sDataGridView = (DataGridView)_fatherForm.Controls.Find("dataGridViewClassBreak", true)[0];
                sDataGridView.Rows[this._index].Cells[0].Value = getSymbolStyleString(_simpleLineSymbol.Style.ToString());
                sDataGridView.Rows[this._index].Cells[0].Style.Font = new System.Drawing.Font("宋体", (float)_simpleLineSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))); ;
                sDataGridView.Rows[this._index].Cells[0].Style.ForeColor = _simpleLineSymbol.Color;
                this.Close();

            }

        }

        private void SymbolLine_Load(object sender, EventArgs e)
        {

        }
    }
}
