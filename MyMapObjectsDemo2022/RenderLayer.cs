using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class RenderLayer : Form
    {
        private readonly MyMapObjects.moMapLayer _Layer;

        //MyMapObjects.moRenderer _Render;
        private readonly MyMapObjects.moMapControl _moMap;

        //赋初值，如果转到非用户选择的渲染类型页面，则
        private MyMapObjects.moSimpleRenderer _SimpleRenderer = new MyMapObjects.moSimpleRenderer();
        private MyMapObjects.moUniqueValueRenderer _UniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();
        private MyMapObjects.moClassBreaksRenderer _ClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
        private readonly MyMapObjects.moSimpleMarkerSymbol _SimpleMarkerSymbol = new MyMapObjects.moSimpleMarkerSymbol();
        private readonly MyMapObjects.moSimpleLineSymbol _SimpleLineSymbol = new MyMapObjects.moSimpleLineSymbol();
        private readonly MyMapObjects.moSimpleFillSymbol _SimpleFillSymbol = new MyMapObjects.moSimpleFillSymbol();

        public RenderLayer(MyMapObjects.moMapLayer sLayer, MyMapObjects.moMapControl smoMap)
        {
            //初始化变量
            _moMap = smoMap;
            _Layer = sLayer;
            //this._Render = _Layer.Renderer;

            //初始化函数
            InitializeComponent();

            //不同类型图层的初始化
            if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                iniPointLayer();
            }
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                iniMultiPolylineLayer();
            }
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                iniMultiPolygonLayer();
            }

            //初始化字段下拉列表
            int countFields = _Layer.AttributeFields.Count;
            for (int i = 0; i < countFields; i++)
            {
                if (_Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dDouble ||
                    _Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt16 ||
                    _Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt32 ||
                    _Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                {
                    _ = comboBoxClassBreakSelectField.Items.Add(_Layer.AttributeFields.GetItem(i).Name);
                }
                _ = comboBoxUniqueValueSelectField.Items.Add(_Layer.AttributeFields.GetItem(i).Name);

            }


        }

        #region 初始化函数
        private void iniPointLayer()
        {
            //判断当前的渲染是哪种形式，以初始化当前符号显示的界面
            //初始化简单渲染界面
            if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                _SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleMarkerSymbol simpleMarkerSymbol = (MyMapObjects.moSimpleMarkerSymbol)_SimpleRenderer.Symbol;
                //显示当前的符号
                showPointSymbolButton(buttonSimpleShowSymbol, simpleMarkerSymbol);
            }
            //初始化分级渲染界面
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                iniUniqueRenderer();
                showPointSymbolButton(buttonSimpleShowSymbol, _SimpleMarkerSymbol);
            }
            //初始化分级渲染界面
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                iniClassBreakRenderer();
                showPointSymbolButton(buttonSimpleShowSymbol, _SimpleMarkerSymbol);
            }

        }
        private void iniMultiPolylineLayer()
        {
            if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                _SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleLineSymbol simpleLineSymbol = (MyMapObjects.moSimpleLineSymbol)_SimpleRenderer.Symbol;
                //显示当前的符号
                showLineSymbolButton(buttonSimpleShowSymbol, simpleLineSymbol);
            }
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                iniUniqueRenderer();
                showLineSymbolButton(buttonSimpleShowSymbol, _SimpleLineSymbol);
            }
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                iniClassBreakRenderer();
                showLineSymbolButton(buttonSimpleShowSymbol, _SimpleLineSymbol);
            }




        }
        private void iniMultiPolygonLayer()
        {
            if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                //给当前的_SimpleRender赋值
                _SimpleRenderer = (MyMapObjects.moSimpleRenderer)_Layer.Renderer;
                //当前的符号
                MyMapObjects.moSimpleFillSymbol simpleFillSymbol = (MyMapObjects.moSimpleFillSymbol)_SimpleRenderer.Symbol; //面符号
                //显示当前的符号
                showFillSymbolButton(buttonSimpleShowSymbol, simpleFillSymbol);
            }
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                iniUniqueRenderer();
                showFillSymbolButton(buttonSimpleShowSymbol, _SimpleFillSymbol);
            }
            else if (_Layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                iniClassBreakRenderer();
                showFillSymbolButton(buttonSimpleShowSymbol, _SimpleFillSymbol);
            }




        }

        private void iniUniqueRenderer()
        {
            tabControlRenderType.SelectedIndex = 1;
            _UniqueValueRenderer = (MyMapObjects.moUniqueValueRenderer)_Layer.Renderer;
            //选择字段
            comboBoxUniqueValueSelectField.SelectedItem = _UniqueValueRenderer.Field;
            comboBoxUniqueValueSelectField.Text = _UniqueValueRenderer.Field;

            showExistUniqueValue((MyMapObjects.moUniqueValueRenderer)_Layer.Renderer);
        }

        private void iniClassBreakRenderer()
        {
            tabControlRenderType.SelectedIndex = 2;
            _ClassBreaksRenderer = (MyMapObjects.moClassBreaksRenderer)_Layer.Renderer;
            //选择字段
            comboBoxClassBreakSelectField.SelectedItem = _ClassBreaksRenderer.Field;
            comboBoxClassBreakSelectField.Text = _ClassBreaksRenderer.Field;

            showExistClassBreak((MyMapObjects.moClassBreaksRenderer)_Layer.Renderer);
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
                SymbolPoint cForm = new SymbolPoint(this, (MyMapObjects.moSimpleMarkerSymbol)_SimpleRenderer.Symbol);
                _ = cForm.ShowDialog();


            }
            //线
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                SymbolLine cForm = new SymbolLine(this, (MyMapObjects.moSimpleLineSymbol)_SimpleRenderer.Symbol);
                _ = cForm.ShowDialog();

            }
            //面
            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                SymbolFill cForm = new SymbolFill(this, (MyMapObjects.moSimpleFillSymbol)_SimpleRenderer.Symbol);
                _ = cForm.ShowDialog();

            }

        }

        /// <summary>
        /// 简单渲染的确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSimpleConfirm_Click(object sender, EventArgs e)
        {
            _Layer.Renderer = _SimpleRenderer;
            _moMap.RedrawMap();
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
            if (comboBoxUniqueValueSelectField.Text == "")
            {
                _ = MessageBox.Show("请先选择字段");
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
                if (_UniqueValueRenderer.Field == comboBoxUniqueValueSelectField.SelectedItem.ToString())
                {
                    //MessageBox.Show("2");
                    showExistUniqueValue(_UniqueValueRenderer);
                }
                else
                {
                    //清空行
                    dataGridViewUniqueValue.Rows.Clear();
                    int sFeatureCount = _Layer.Features.Count;//当前图层的要素个数
                    int sFieldIndex = _Layer.AttributeFields.FindField(comboBoxUniqueValueSelectField.SelectedItem.ToString());//当前选择字段的索引

                    //创建三个List，分别记录了该字段上的值的集合、每个值对应的符号的集合、每个值对应的要素个数集合
                    List<string> sValues = new List<string>();
                    List<MyMapObjects.moSymbol> sSymbols = new List<MyMapObjects.moSymbol>();
                    List<int> sValueCount = new List<int>();

                    //维护三个List
                    for (int i = 0; i < sFeatureCount; i++)
                    {
                        string sCurrentValue = GetValueString(_Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex));
                        if (sValues.Exists(t => t == sCurrentValue))
                        {
                            int sCurrentIndex = FindIndex(sValues, sCurrentValue);
                            sValueCount[sCurrentIndex] += 1;
                        }
                        else
                        {
                            MyMapObjects.moSymbol sCurrentSymbol;
                            if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                            {
                                sCurrentSymbol = new MyMapObjects.moSimpleMarkerSymbol();
                                sSymbols.Add(sCurrentSymbol);
                            }
                            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                            {
                                sCurrentSymbol = new MyMapObjects.moSimpleLineSymbol();
                                sSymbols.Add(sCurrentSymbol);
                            }
                            else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
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
                    _UniqueValueRenderer.Field = comboBoxUniqueValueSelectField.SelectedItem.ToString();
                    _UniqueValueRenderer.Symbols = sSymbols;
                    _UniqueValueRenderer.Values = sValues;


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
                if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    SymbolPoint cForm = new SymbolPoint(this, (MyMapObjects.moSimpleMarkerSymbol)_UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    _ = cForm.ShowDialog();

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    SymbolLine cForm = new SymbolLine(this, (MyMapObjects.moSimpleLineSymbol)_UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    _ = cForm.ShowDialog();

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    SymbolFill cForm = new SymbolFill(this, (MyMapObjects.moSimpleFillSymbol)_UniqueValueRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.UniqueValue, sClickIndex);
                    _ = cForm.ShowDialog();
                }

            }

        }

        //确认
        private void buttonUniqueValueConfirm_Click(object sender, EventArgs e)
        {
            _Layer.Renderer = _UniqueValueRenderer;
            _moMap.RedrawMap();
        }
        #endregion

        #region ClassBreak渲染界面控件
        private void buttonClassBreakLoadAll_Click(object sender, EventArgs e)
        {
            if (comboBoxClassBreakSelectField.Text == "")
            {
                _ = MessageBox.Show("请先选择字段");
            }
            else
            {

                //清空行
                dataGridViewClassBreak.Rows.Clear();
                int sFeatureCount = _Layer.Features.Count;//当前图层的要素个数
                int sFieldIndex = _Layer.AttributeFields.FindField(comboBoxClassBreakSelectField.SelectedItem.ToString());//当前选择字段的索引

                //创建三个List，分别记录了该字段上的值的集合、每个值对应的符号的集合、每个值对应的要素个数集合
                List<double> sBreakValues = new List<double>();
                List<MyMapObjects.moSymbol> sSymbols = new List<MyMapObjects.moSymbol>();
                List<int> sValueCount = new List<int>();

                //初始化sBreakValues列表
                List<double> sValues = new List<double>();
                for (int i = 0; i < sFeatureCount - 1; i++)
                {
                    //MessageBox.Show(this._Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());
                    double sValue = double.Parse(_Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());

                    sValues.Add(sValue);
                }
                double sMinValue = sValues.Min();
                double sMaxValue = sValues.Max();

                int sBreakCount = int.Parse(numericUpDownClassNumber.Value.ToString());
                double step = (sMaxValue - sMinValue) / sBreakCount;


                for (int i = 0; i < sBreakCount; i++)
                {
                    sBreakValues.Add(sMinValue + (step * (i + 1)));
                    sValueCount.Add(0);

                    MyMapObjects.moSymbol sCurrentSymbol;
                    if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                    {
                        sCurrentSymbol = new MyMapObjects.moSimpleMarkerSymbol();
                        sSymbols.Add(sCurrentSymbol);
                    }
                    else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                    {
                        sCurrentSymbol = new MyMapObjects.moSimpleLineSymbol();
                        sSymbols.Add(sCurrentSymbol);
                    }
                    else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                    {
                        sCurrentSymbol = new MyMapObjects.moSimpleFillSymbol();
                        sSymbols.Add(sCurrentSymbol);
                    }
                }


                for (int i = 0; i < _Layer.Features.Count; i++)
                {
                    double sCurrentFeatureValue = double.Parse(_Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());
                    if (sCurrentFeatureValue < sBreakValues[0])
                    {
                        sValueCount[0]++;
                    }
                    for (int j = 1; j < sBreakValues.Count; j++)
                    {
                        if (sCurrentFeatureValue < sBreakValues[j] &&
                           sCurrentFeatureValue > sBreakValues[j - 1])
                        {
                            sValueCount[j]++;
                        }
                    }

                }






                //赋值给_UniqueValueRenderer
                //赋值给UniqueRenderer
                _ClassBreaksRenderer.Field = comboBoxClassBreakSelectField.SelectedItem.ToString();
                _ClassBreaksRenderer.Symbols = sSymbols;
                _ClassBreaksRenderer.BreakValues = sBreakValues;
                //this._ClassBreaksRenderer.BreakCount = sBreakCount;


                //在表格中显示值
                drawClassBreakDataGridView(sSymbols, sBreakValues, sValueCount);



            }



        }
        private void buttonClassBreakConfirm_Click(object sender, EventArgs e)
        {
            _Layer.Renderer = _ClassBreaksRenderer;
            _moMap.RedrawMap();

        }
        private void dataGridViewClassBreak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int sClickIndex = e.RowIndex;
                if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    SymbolPoint cForm = new SymbolPoint(this, (MyMapObjects.moSimpleMarkerSymbol)_ClassBreaksRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.ClassBreaks, sClickIndex);
                    _ = cForm.ShowDialog();

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    SymbolLine cForm = new SymbolLine(this, (MyMapObjects.moSimpleLineSymbol)_ClassBreaksRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.ClassBreaks, sClickIndex);
                    _ = cForm.ShowDialog();

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    SymbolFill cForm = new SymbolFill(this, (MyMapObjects.moSimpleFillSymbol)_ClassBreaksRenderer.Symbols[sClickIndex], MyMapObjects.moRendererTypeConstant.ClassBreaks, sClickIndex);
                    _ = cForm.ShowDialog();
                }

            }


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
            return value == null ? string.Empty : value.ToString();
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
        private void drawUniqueValueDataGridView(List<MyMapObjects.moSymbol> sSymbols, List<string> sValues, List<int> sValueCount)
        {
            dataGridViewUniqueValue.Rows.Clear();
            for (int i = 0; i < sValues.Count; i++)
            {
                DataGridViewRow sRow = new DataGridViewRow();
                DataGridViewTextBoxCell sSymbolCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sValueCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sCountCell = new DataGridViewTextBoxCell();
                if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moSimpleMarkerSymbol sCurrentSymbol = (MyMapObjects.moSimpleMarkerSymbol)sSymbols[i];
                    sSymbolCell.Value = getMarkerSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;
                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moSimpleLineSymbol sCurrentSymbol = (MyMapObjects.moSimpleLineSymbol)sSymbols[i];
                    //显示当前的符号
                    sSymbolCell.Value = getLineSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moSimpleFillSymbol sCurrentSymbol = (MyMapObjects.moSimpleFillSymbol)sSymbols[i]; //面符号
                    MyMapObjects.moSimpleLineSymbol sOutLine = sCurrentSymbol.Outline;
                    //显示当前的符号
                    sSymbolCell.Style.BackColor = sCurrentSymbol.Color;
                    sSymbolCell.Value = getLineSymbolStyleString(sOutLine.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sOutLine.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sOutLine.Color;

                }
                sValueCell.Value = sValues[i];
                sCountCell.Value = sValueCount[i];

                _ = sRow.Cells.Add(sSymbolCell);
                _ = sRow.Cells.Add(sValueCell);
                _ = sRow.Cells.Add(sCountCell);

                _ = dataGridViewUniqueValue.Rows.Add(sRow);


            }

        }
        private void drawClassBreakDataGridView(List<MyMapObjects.moSymbol> sSymbols, List<double> sBreakValues, List<int> sValueCount)
        {
            dataGridViewClassBreak.Rows.Clear();
            for (int i = 0; i < sBreakValues.Count; i++)
            {
                DataGridViewRow sRow = new DataGridViewRow();
                DataGridViewTextBoxCell sSymbolCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sValueCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell sCountCell = new DataGridViewTextBoxCell();
                if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moSimpleMarkerSymbol sCurrentSymbol = (MyMapObjects.moSimpleMarkerSymbol)sSymbols[i];
                    sSymbolCell.Value = getMarkerSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;
                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moSimpleLineSymbol sCurrentSymbol = (MyMapObjects.moSimpleLineSymbol)sSymbols[i];
                    //显示当前的符号
                    sSymbolCell.Value = getLineSymbolStyleString(sCurrentSymbol.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sCurrentSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sCurrentSymbol.Color;

                }
                else if (_Layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moSimpleFillSymbol sCurrentSymbol = (MyMapObjects.moSimpleFillSymbol)sSymbols[i]; //面符号
                    MyMapObjects.moSimpleLineSymbol sOutLine = sCurrentSymbol.Outline;
                    //显示当前的符号
                    sSymbolCell.Style.BackColor = sCurrentSymbol.Color;
                    sSymbolCell.Value = getLineSymbolStyleString(sOutLine.Style.ToString());
                    sSymbolCell.Style.Font = new System.Drawing.Font("宋体", (float)sOutLine.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
                    sSymbolCell.Style.ForeColor = sOutLine.Color;

                }
                sValueCell.Value = sBreakValues[i];
                sCountCell.Value = sValueCount[i];

                _ = sRow.Cells.Add(sSymbolCell);
                _ = sRow.Cells.Add(sValueCell);
                _ = sRow.Cells.Add(sCountCell);

                _ = dataGridViewClassBreak.Rows.Add(sRow);

            }
        }
        /// <summary>
        /// 显示已经存在UniqueValueRenderer
        /// </summary>
        /// <param name="sUniqueValueRenderer"></param>
        private void showExistUniqueValue(MyMapObjects.moUniqueValueRenderer sUniqueValueRenderer)
        {
            int sFieldIndex = _Layer.AttributeFields.FindField(sUniqueValueRenderer.Field);//当前选择字段的索引
            List<MyMapObjects.moSymbol> sSymbols = sUniqueValueRenderer.Symbols;
            List<string> sValues = sUniqueValueRenderer.Values;
            List<int> sValueCount = new List<int>();
            int i = 0;
            while (i < sUniqueValueRenderer.ValueCount)
            {
                sValueCount.Add(0);
                string sCurrentValue = sValues[i];
                for (int j = 0; j < _Layer.Features.Count; j++)
                {
                    if (sCurrentValue == GetValueString(_Layer.Features.GetItem(j).Attributes.GetItem(sFieldIndex)))
                    {
                        sValueCount[i]++;
                    }
                }
                i++;
            }
            drawUniqueValueDataGridView(sSymbols, sValues, sValueCount);
        }
        private void showExistClassBreak(MyMapObjects.moClassBreaksRenderer sClassBreakRenderer)
        {
            int sFieldIndex = _Layer.AttributeFields.FindField(sClassBreakRenderer.Field);//当前选择字段的索引
            List<MyMapObjects.moSymbol> sSymbols = sClassBreakRenderer.Symbols;
            List<double> sBreakValues = sClassBreakRenderer.BreakValues;
            List<int> sValueCount = new List<int>();

            for (int i = 0; i < sBreakValues.Count; i++)
            {
                sValueCount.Add(0);
            }
            for (int i = 0; i < _Layer.Features.Count; i++)
            {
                double sCurrentFeatureValue = double.Parse(_Layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString());
                if (sCurrentFeatureValue < sBreakValues[0])
                {
                    sValueCount[0]++;
                }
                for (int j = 1; j < sBreakValues.Count; j++)
                {
                    if (sCurrentFeatureValue < sBreakValues[j] &&
                       sCurrentFeatureValue > sBreakValues[j - 1])
                    {
                        sValueCount[j]++;
                    }
                }

            }
            drawClassBreakDataGridView(sSymbols, sBreakValues, sValueCount);

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

            else
            {
                return style == "Square" ? "□" : style == "SolidSquare" ? "■" : style == "CircleDot" ? "☉" : style == "CircleCircle" ? "◎" : "●";
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
            else
            {
                return style == "Dash"
                    ? "----------------"
                    : style == "Dot"
                                    ? "••••••••••••••••"
                                    : style == "DashDot" ? "-•-•-•-•-•-•-•-•" : style == "DashDotDot" ? "-••-••-••-••-••" : "——————————————";
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
            button.Font = new System.Drawing.Font("宋体", (float)simpleMarkerSymbol.Size * (float)2.83, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
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
            button.Font = new System.Drawing.Font("宋体", (float)simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
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
            button.Font = new System.Drawing.Font("宋体", (float)simpleLineSymbol.Size * (float)2.83 * 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            button.ForeColor = simpleLineSymbol.Color;
            button.BackColor = simpleFillSymbol.Color;
        }



        #endregion


    }

}
