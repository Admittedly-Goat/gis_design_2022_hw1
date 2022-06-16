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
    public partial class RenderLayer : Form
    {
        MyMapObjects.moMapLayer _Layer;
        //MyMapObjects.moRenderer _Render;
        MyMapObjects.moMapControl _moMap;

        //赋初值，如果转到非用户选择的渲染类型页面，则
        MyMapObjects.moSimpleRenderer _SimpleRenderer = new MyMapObjects.moSimpleRenderer();
        MyMapObjects.moUniqueValueRenderer _UniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();
        MyMapObjects.moClassBreaksRenderer _ClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
        MyMapObjects.moSimpleMarkerSymbol _SimpleMarkerSymbol = new MyMapObjects.moSimpleMarkerSymbol();
        MyMapObjects.moSimpleLineSymbol _SimpleLineSymbol = new MyMapObjects.moSimpleLineSymbol();
        MyMapObjects.moSimpleFillSymbol _SimpleFillSymbol = new MyMapObjects.moSimpleFillSymbol();

        public RenderLayer(MyMapObjects.moMapLayer sLayer, MyMapObjects.moMapControl smoMap)
        {
            //初始化变量
            this._moMap = smoMap;
            this._Layer=sLayer;
            //this._Render = _Layer.Renderer;

            //初始化函数
            InitializeComponent();

            //不同类型图层的初始化
            if(_Layer.ShapeType== MyMapObjects.moGeometryTypeConstant.Point)
            {
                iniPointLayer();
            }
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                iniMultiPolylineLayer();
            }
            else if(_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                iniMultiPolygonLayer();
            }

            //初始化字段下拉列表
            int countFields = this._Layer.AttributeFields.Count;
            for (int i = 0; i < countFields; i++)
            {

                this.comboBoxUniqueValueSelectField.Items.Add(this._Layer.AttributeFields.GetItem(i).Name);
            }

        }

        #region 初始化函数
        private void iniPointLayer()
        {
            //判断当前的渲染是哪种形式，以初始化当前符号显示的界面
            //初始化简单渲染界面
            if (this._Layer.Renderer.RendererType== MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                this._SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleMarkerSymbol simpleMarkerSymbol = (MyMapObjects.moSimpleMarkerSymbol)this._SimpleRenderer.Symbol;
                //显示当前的符号
                showPointSymbolButton(this.buttonSimpleShowSymbol,simpleMarkerSymbol);
            }
            //初始化分级渲染界面
            else if(this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                //this.tabControlRenderType.SelectedIndex = 1;
                ////MessageBox.Show("1");
                //this._UniqueValueRenderer = (MyMapObjects.moUniqueValueRenderer)_Layer.Renderer;
                ////选择字段
                //this.comboBoxUniqueValueSelectField.SelectedItem = _UniqueValueRenderer.Field;
                //this.comboBoxUniqueValueSelectField.Text = _UniqueValueRenderer.Field;

                //showExistUniqueValue((MyMapObjects.moUniqueValueRenderer)this._Layer.Renderer);
                iniUniqueRenderer();
                showPointSymbolButton(this.buttonSimpleShowSymbol, this._SimpleMarkerSymbol);
            }
            //初始化分级渲染界面

        }
        private void iniMultiPolylineLayer()
        {
            if (this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                this._SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleLineSymbol simpleLineSymbol = (MyMapObjects.moSimpleLineSymbol)this._SimpleRenderer.Symbol;
                //显示当前的符号
                showLineSymbolButton(this.buttonSimpleShowSymbol, simpleLineSymbol);
            }
            else if(this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                iniUniqueRenderer();
                showLineSymbolButton(this.buttonSimpleShowSymbol, this._SimpleLineSymbol);

            }

           


        }
        private void iniMultiPolygonLayer()
        {
            if (this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                this._SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleFillSymbol simpleFillSymbol = (MyMapObjects.moSimpleFillSymbol)this._SimpleRenderer.Symbol; //面符号
                //显示当前的符号
                showFillSymbolButton(this.buttonSimpleShowSymbol, simpleFillSymbol);
            }
            else if (this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                iniUniqueRenderer();
                showFillSymbolButton(this.buttonSimpleShowSymbol, this._SimpleFillSymbol);
            }




        }

        private void iniUniqueRenderer()
        {
            this.tabControlRenderType.SelectedIndex = 1;
            //MessageBox.Show("1");
            this._UniqueValueRenderer = (MyMapObjects.moUniqueValueRenderer)_Layer.Renderer;
            //选择字段
            this.comboBoxUniqueValueSelectField.SelectedItem = _UniqueValueRenderer.Field;
            this.comboBoxUniqueValueSelectField.Text = _UniqueValueRenderer.Field;

            showExistUniqueValue((MyMapObjects.moUniqueValueRenderer)this._Layer.Renderer);
        }
        #endregion

        #region 简单渲染界面控件
        /// <summary>
        /// 点击显示当前Symbol的button，跳转到更改Symbol的子界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSimpleShowSymbol_Click(object sender, EventArgs e)
        {
            //点
            if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {

                //SymbolPoint cForm = new SymbolPoint(this,this._SimpleMarkerSymbol);
                SymbolPoint cForm = new SymbolPoint(this, (MyMapObjects.moSimpleMarkerSymbol)this._SimpleRenderer.Symbol);
                cForm.ShowDialog();


            }
            //线
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                SymbolLine cForm = new SymbolLine(this, (MyMapObjects.moSimpleLineSymbol)this._SimpleRenderer.Symbol);
                cForm.ShowDialog();

            }
            //面
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                SymbolFill cForm = new SymbolFill(this, (MyMapObjects.moSimpleFillSymbol)this._SimpleRenderer.Symbol);
                cForm.ShowDialog();

            }

        }

        /// <summary>
        /// 简单渲染的确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSimpleConfirm_Click(object sender, EventArgs e)
        {
            this._Layer.Renderer = this._SimpleRenderer;
            this._moMap.RedrawMap();
            //this.Close();
        }
        #endregion

        #region 唯一值渲染界面控件
        /// <summary>
        /// 唯一值渲染的获取全部值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUniqueValueLoadAll_Click(object sender, EventArgs e)
        {
            if (this.comboBoxUniqueValueSelectField.Text == "")
            {
                MessageBox.Show("请先选择字段");
            }
            else
            {
                //如果当前
                //if (this._Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue && ((MyMapObjects.moUniqueValueRenderer)this._Layer.Renderer).Field == this.comboBoxUniqueValueSelectField.SelectedItem.ToString())
                //{
                //    showExistUniqueValue((MyMapObjects.moUniqueValueRenderer)this._Layer.Renderer);
                //    MessageBox.Show("1");
                //    if (((MyMapObjects.moUniqueValueRenderer)this._Layer.Renderer).Symbols == this._UniqueValueRenderer.Symbols)
                //    {
                //        MessageBox.Show("wrong");
                //    }
                //}
                if (this._UniqueValueRenderer.Field == this.comboBoxUniqueValueSelectField.SelectedItem.ToString())
                {
                    MessageBox.Show("2");
                    showExistUniqueValue(this._UniqueValueRenderer);

                }
                else
                {
                    //清空行
                    this.dataGridViewUniqueValue.Rows.Clear();
                    int sFeatureCount = this._Layer.Features.Count;//当前图层的要素个数
                    Int32 sFieldIndex = this._Layer.AttributeFields.FindField(this.comboBoxUniqueValueSelectField.SelectedItem.ToString());//当前选择字段的索引

                    //创建三个List，分别记录了该字段上的值的集合、每个值对应的符号的集合、每个值对应的要素个数集合
                    List<string> sValues = new List<string>();
                    List<MyMapObjects.moSymbol> sSymbols = new List<MyMapObjects.moSymbol>();
                    List<int> sValueCount = new List<int>();

                    //维护三个List
                    for (int i = 0; i < sFeatureCount; i++)
                    {
                        string sCurrentValue = GetValueString(this._Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex));
                        if (sValues.Exists(t => t == sCurrentValue))
                        {
                            int sCurrentIndex = FindIndex(sValues, sCurrentValue);
                            sValueCount[sCurrentIndex] += 1;
                        }
                        else
                        {
                            MyMapObjects.moSymbol sCurrentSymbol;
                            if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                            {
                                sCurrentSymbol = new MyMapObjects.moSimpleMarkerSymbol();
                                sSymbols.Add(sCurrentSymbol);
                            }
                            else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                            {
                                sCurrentSymbol = new MyMapObjects.moSimpleLineSymbol();
                                sSymbols.Add(sCurrentSymbol);
                            }
                            else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                            {
                                sCurrentSymbol = new MyMapObjects.moSimpleFillSymbol();
                                sSymbols.Add(sCurrentSymbol);
                            }
                            sValues.Add(sCurrentValue);
                            sValueCount.Add(1);
                        }
                    }

                    //赋值给_UniqueValueRenderer
                    //赋值给UniqueRenderer
                    this._UniqueValueRenderer.Field = this.comboBoxUniqueValueSelectField.SelectedItem.ToString();
                    this._UniqueValueRenderer.Symbols = sSymbols;
                    this._UniqueValueRenderer.Values = sValues;


                    //在表格中显示值
                    drawUniqueValueDataGridView(sSymbols, sValues, sValueCount);

                }

            }



        }

        //更改单一符号
        private void dataGridViewUniqueValue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int sClickIndex = e.RowIndex;
                if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    SymbolPoint cForm = new SymbolPoint(this, (MyMapObjects.moSimpleMarkerSymbol)this._UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    cForm.ShowDialog();

                }
                else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    SymbolLine cForm = new SymbolLine(this, (MyMapObjects.moSimpleLineSymbol)this._UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    cForm.ShowDialog();

                }
                else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    SymbolFill cForm = new SymbolFill(this, (MyMapObjects.moSimpleFillSymbol)this._UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    cForm.ShowDialog();
                }

            }

        }

        //确认
        private void buttonUniqueValueConfirm_Click(object sender, EventArgs e)
        {
            this._Layer.Renderer = this._UniqueValueRenderer;
            this._moMap.RedrawMap();
        }
        #endregion

        #region ClassBreak渲染界面控件
        private void buttonClassBreakLoadAll_Click(object sender, EventArgs e)
        {
            if (this.comboBoxClassBreakSelectField.Text == "")
            {
                MessageBox.Show("请先选择字段");
            }

        }
        private void buttonClassBreakConfirm_Click(object sender, EventArgs e)
        {

        }
        #endregion








        #region 私有函数
        /// <summary>
        /// 获取一个属性值的字符串形式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetValueString(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return value.ToString();
        }
        private int FindIndex(List<string> sValues, string sCurrentValue)
        {
            int index = -1;
            for (int i = 0; i < sValues.Count; i++)
            {
                if (sValues[i] == sCurrentValue)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        /// <summary>
        /// 根据给定的三个列表画UniqueValueDataGridView
        /// </summary>
        /// <param name="sSymbols"></param>
        /// <param name="sValues"></param>
        /// <param name="sValueCount"></param>
        private void drawUniqueValueDataGridView(List<MyMapObjects.moSymbol> sSymbols,List<string> sValues,List<int> sValueCount)
        {
            this.dataGridViewUniqueValue.Rows.Clear();
            for (int i = 0; i < sValues.Count; i++)
            {
                DataGridViewRow sRow = new DataGridViewRow();
                DataGridViewTextBoxCell sSymbolCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sValueCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sCountCell = new DataGridViewTextBoxCell();
                if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moSimpleMarkerSymbol sCurrentSymbol = (MyMapObjects.moSimpleMarkerSymbol)sSymbols[i];
                    sSymbolCell.Value = getMarkerSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;
                }
                else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moSimpleLineSymbol sCurrentSymbol = (MyMapObjects.moSimpleLineSymbol)sSymbols[i];
                    //显示当前的符号
                    sSymbolCell.Value = getLineSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;

                }
                else if (this._Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moSimpleFillSymbol sCurrentSymbol = (MyMapObjects.moSimpleFillSymbol)sSymbols[i]; //面符号
                    MyMapObjects.moSimpleLineSymbol sOutLine = sCurrentSymbol.Outline;
                    //显示当前的符号
                    sSymbolCell.Style.BackColor = sCurrentSymbol.Color;
                    sSymbolCell.Value = getLineSymbolStyleString(sOutLine.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sOutLine.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sSymbolCell.Style.ForeColor = sOutLine.Color;

                }
                sValueCell.Value = sValues[i];
                sCountCell.Value = sValueCount[i];

                sRow.Cells.Add(sSymbolCell);
                sRow.Cells.Add(sValueCell);
                sRow.Cells.Add(sCountCell);

                this.dataGridViewUniqueValue.Rows.Add(sRow);


            }

        }
        /// <summary>
        /// 显示已经存在UniqueValueRenderer
        /// </summary>
        /// <param name="sUniqueValueRenderer"></param>
        private void showExistUniqueValue(MyMapObjects.moUniqueValueRenderer sUniqueValueRenderer)
        {
            Int32 sFieldIndex = this._Layer.AttributeFields.FindField(sUniqueValueRenderer.Field);//当前选择字段的索引
            List<MyMapObjects.moSymbol> sSymbols = sUniqueValueRenderer.Symbols;
            List<string> sValues = sUniqueValueRenderer.Values;
            List<int> sValueCount = new List<int>();
            int i = 0;
            while (i < sUniqueValueRenderer.ValueCount)
            {
                sValueCount.Add(0);
                string sCurrentValue = sValues[i];
                for (int j = 0; j < this._Layer.Features.Count; j++)
                {
                    if (sCurrentValue == GetValueString(this._Layer.Features.GetItem(j).Attributes.GetItem(sFieldIndex)))
                    {
                        sValueCount[i]++;
                    }
                }
                i++;
            }
            drawUniqueValueDataGridView(sSymbols, sValues, sValueCount);
        }
        /// <summary>
        /// 通过名称获取点符号
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private string getMarkerSymbolStyleString(string style)
        {
            if (style == "Circle")
            {
                return "○";
            }
            else if (style == "SolidCircle")
            {
                return "●";
            }
            else if (style == "Triangle")
            {
                return "△";
            }

            else if (style == "SolidTriangle")
            {
                return "▲";
            }

            else if (style == "Square")
            {
                return "□";
            }
            else if (style == "SolidSquare")
            {
                return "■";
            }
            else if (style == "CircleDot")
            {
                return "☉";
            }
            else if (style == "CircleCircle")
            {
                return "◎";
            }
            else
            {
                return "●";
            }


        }
        /// <summary>
        /// 通过名称获取线符号
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private string getLineSymbolStyleString(string style)
        {
            if (style == "Solid")
            {

                return "——————————————";
            }
            else if (style == "Dash")
            {

                return "----------------";
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
        /// <summary>
        /// 在button上展示点符号
        /// </summary>
        /// <param name="sButton"></param>
        /// <param name="simpleMarkerSymbol"></param>
        private void showPointSymbolButton(Button button, MyMapObjects.moSimpleMarkerSymbol simpleMarkerSymbol)
        {
            button.Text = getMarkerSymbolStyleString(simpleMarkerSymbol.Style.ToString());
            button.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            button.ForeColor = simpleMarkerSymbol.Color;
        }
        /// <summary>
        /// 在button上展示线符号
        /// </summary>
        /// <param name="button"></param>
        /// <param name="simpleLineSymbol"></param>
        private void showLineSymbolButton(Button button, MyMapObjects.moSimpleLineSymbol simpleLineSymbol)
        {
            button.Text = getLineSymbolStyleString(simpleLineSymbol.Style.ToString());
            button.Font = new System.Drawing.Font("宋体", (float)simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            button.ForeColor = simpleLineSymbol.Color;
        }
        /// <summary>
        /// 在button上展示面符号
        /// </summary>
        /// <param name="button"></param>
        /// <param name="simpleLineSymbol"></param>
        private void showFillSymbolButton(Button button, MyMapObjects.moSimpleFillSymbol simpleFillSymbol)
        {
            MyMapObjects.moSimpleLineSymbol simpleLineSymbol = simpleFillSymbol.Outline;
            button.Text = getLineSymbolStyleString(simpleLineSymbol.Style.ToString());
            button.Font = new System.Drawing.Font("宋体", (float)simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            button.ForeColor = simpleLineSymbol.Color;
            button.BackColor = simpleFillSymbol.Color;
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }

}
