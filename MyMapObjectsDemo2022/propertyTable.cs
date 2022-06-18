using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class propertyTable : Form
    {
        // 回调函数，令属性表为空
        private readonly Action frmMainSetPropertyTableNullMethod;

        // 回调函数，重绘地图
        private readonly Action RedrawMoMap;

        // 目标图层
        private readonly MyMapObjects.moMapLayer Layer;
        // 当前是否需要影响地图的选择情况，应对刷新属性表时出现的选择丢失
        public bool CanAffectLayerSelection = false;

        // 是否仅展示选择
        private bool ShowOnlySelected = false;

        public propertyTable(MyMapObjects.moMapLayer layer, Action frmMainSetPropertyTableNullMethod, Action redrawMoMap)
        {
            InitializeComponent();
            Layer = layer;
            this.frmMainSetPropertyTableNullMethod = frmMainSetPropertyTableNullMethod;
            RedrawMoMap = redrawMoMap;
            ReloadPropList();
        }

        // 重载属性表
        internal void ReloadPropList()
        {
            //清空
            propertyGrid.Rows.Clear();
            propertyGrid.Columns.Clear();
            bool isColumnEmpty = true;
            //新建表头
            _ = propertyGrid.Columns.Add("要素内部ID", "要素内部ID");
            propertyGrid.Columns["要素内部ID"].Frozen = true;
            propertyGrid.Columns["要素内部ID"].ReadOnly = true;
            for (int i = 0; i < Layer.AttributeFields.Count; i++)
            {
                _ = propertyGrid.Columns.Add(i.ToString(), Layer.AttributeFields.GetItem(i).Name);
                isColumnEmpty = false;
            }
            if (isColumnEmpty)
            {
                return;
            }

            //遍历feature，获取属性值和是否选中的情况
            List<int> selectedFeaturesIndex = new List<int>();
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                object[] values = new object[Layer.AttributeFields.Count + 1];
                values[0] = i;
                for (int j = 0; j < values.Length - 1; j++)
                {
                    values[j + 1] = Layer.Features.GetItem(i).Attributes.GetItem(j);
                }
                _ = propertyGrid.Rows.Add(values);
                for (int j = 0; j < Layer.SelectedFeatures.Count; j++)
                {
                    if (Layer.SelectedFeatures.GetItem(j) == Layer.Features.GetItem(i))
                    {
                        selectedFeaturesIndex.Add(i);
                        break;
                    }
                }
            }
            propertyGrid.ClearSelection();
            for (int i = 0; i < selectedFeaturesIndex.Count; i++)
            {
                propertyGrid.Rows[selectedFeaturesIndex[i]].Selected = true;
            }
            ShowOnlySelected = false;
        }

        // 关闭事件
        private void propertyTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMainSetPropertyTableNullMethod();
        }

        private void 添加字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //展示输入框
            addFieldDialogBox fieldDialogBox = new addFieldDialogBox();
            _ = fieldDialogBox.ShowDialog();
            //名字
            string inputName = fieldDialogBox.FieldName;
            if ((inputName != null) && (inputName != ""))
            {
                for (int i = 0; i < Layer.AttributeFields.Count; i++)
                {
                    if (Layer.AttributeFields.GetItem(i).Name == inputName)
                    {
                        _ = MessageBox.Show("字段已经存在。");
                        return;
                    }
                }
                //字段类型
                int selectedTypeIndex = fieldDialogBox.TypeIndex;
                if (selectedTypeIndex == 0)
                {
                }
                else if (selectedTypeIndex == 1)
                {
                }
                else if (selectedTypeIndex == 2)
                {
                }
                else if (selectedTypeIndex == 3)
                {
                }
                else if (selectedTypeIndex == 4)
                {
                }
                else
                {
                    _ = MessageBox.Show("您输入的类型无效。");
                    return;
                }
                string inputDefaultValue;
                object convertedTypeValue;
                //字段默认值
                inputDefaultValue = fieldDialogBox.DefaultValue;
                try
                {
                    if (selectedTypeIndex == 0)
                    {
                        convertedTypeValue = Convert.ToSingle(inputDefaultValue);
                    }
                    else if (selectedTypeIndex == 1)
                    {
                        convertedTypeValue = Convert.ToDouble(inputDefaultValue);
                    }
                    else if (selectedTypeIndex == 2)
                    {
                        convertedTypeValue = Convert.ToInt32(inputDefaultValue);
                    }
                    else if (selectedTypeIndex == 3)
                    {
                        convertedTypeValue = Convert.ToInt64(inputDefaultValue);
                    }
                    else if (selectedTypeIndex == 4)
                    {
                        convertedTypeValue = Convert.ToString(inputDefaultValue);
                    }
                    else
                    {
                        _ = MessageBox.Show("输入的默认值有误。");
                        return;
                    }
                }
                catch
                {
                    _ = MessageBox.Show("输入的默认值有误。");
                    return;
                }
                //操作图层，增加新字段
                Layer.AttributeFields.Append(new MyMapObjects.moField(inputName));
                for (int i = 0; i < Layer.Features.Count; i++)
                {
                    Layer.Features.GetItem(i).Attributes.SetItem(Layer.AttributeFields.Count - 1, inputDefaultValue);
                }
                ReloadPropList();
                return;
            }
            else
            {
                _ = MessageBox.Show("输入的字段名称无效。");
            }
        }

        private void 删除字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAttributeFields attributeFieldsForm = new SelectAttributeFields(Layer);
            _ = attributeFieldsForm.ShowDialog();
            int inputIndex = attributeFieldsForm.SelectedFieldIndex;
            if (inputIndex == -1)
            {
                _ = MessageBox.Show("字段有误。");
                return;
            }
            Layer.AttributeFields.RemoveAt(inputIndex);
            ReloadPropList();
            return;
        }

        private void 排列字段顺序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = new AttrSequenceChanger(Layer).ShowDialog();
            ReloadPropList();
        }


        private void propertyGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //劫持输入到自定义输入框
            _ = propertyGrid.CancelEdit();
            int i = e.ColumnIndex - 1;
            //开始修改
            while (true)
            {
                string value = Microsoft.VisualBasic.Interaction.InputBox("新值", "修改值");
                try
                {
                    if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dSingle)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToSingle(value));
                    }
                    else if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToDouble(value));
                    }
                    else if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt16)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToInt16(value));
                    }
                    else if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt32)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToInt32(value));
                    }
                    else if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToInt64(value));
                    }
                    else if (Layer.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dText)
                    {
                        Layer.Features.GetItem(Convert.ToInt32(propertyGrid.Rows[e.RowIndex].Cells[0].Value)).Attributes.SetItem(i, Convert.ToString(value));
                    }
                    ReloadPropList();
                    return;
                }
                catch
                {
                    _ = MessageBox.Show($"输入数据有误，数据未更改。该字段的类型为：{Layer.AttributeFields.GetItem(i).ValueType}");
                    ReloadPropList();
                    break;
                }
            }

        }

        private void 修改字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获得输入
            ChangeFieldName changeFieldName = new ChangeFieldName(Layer);
            _ = changeFieldName.ShowDialog();
            int inputIndex = changeFieldName.SelectedFieldIndex;

            // 检查输入是否合理
            if (inputIndex == -1)
            {
                _ = MessageBox.Show("选择字段有误。");
                return;
            }
            string valueNew = changeFieldName.NewName;
            for (int j = 0; j < Layer.AttributeFields.Count; j++)
            {
                if (Layer.AttributeFields.GetItem(j).Name == valueNew)
                {
                    _ = MessageBox.Show("字段名重复。");
                    return;
                }
            }
            if (valueNew == "")
            {
                _ = MessageBox.Show("字段名无效。");
                return;
            }

            //修改
            Layer.AttributeFields.GetItem(inputIndex).Name = valueNew;
            ReloadPropList();
            return;
        }

        private void propertyTable_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid_SelectionChanged(object sender, EventArgs e)
        {
            // 修改momap的显示
            if (CanAffectLayerSelection)
            {
                Layer.ClearSelection();
                for (int i = 0; i < propertyGrid.SelectedRows.Count; i++)
                {
                    Layer.SelectedFeatures.Add(Layer.Features.GetItem(Convert.ToInt32(propertyGrid.SelectedRows[i].Cells[0].Value)));
                }
                RedrawMoMap();
            }
            toolStripStatusLabel1.Text = $"已选择要素数：{Layer.SelectedFeatures.Count}";
        }

        private void 显示所有已选择要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOnlySelected = !ShowOnlySelected;
            if (ShowOnlySelected == true)
            {
                ExcludeNonSelectedRow();
            }
            else
            {
                ReloadPropList();
            }
        }

        public void ExcludeNonSelectedRow()
        {
            //排除未选择的行
            if (ShowOnlySelected == true)
            {
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
                for (int i = 0; i < propertyGrid.SelectedRows.Count; i++)
                {
                    selectedRows.Add(propertyGrid.SelectedRows[i]);
                }
                propertyGrid.Rows.Clear();
                foreach (DataGridViewRow i in selectedRows)
                {
                    _ = propertyGrid.Rows.Add(i);
                }
            }
        }

        private void 反选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> selectedFeatureId = new List<int>();
            for (int i = 0; i < propertyGrid.SelectedRows.Count; i++)
            {
                selectedFeatureId.Add(Convert.ToInt32(propertyGrid.SelectedRows[i].Cells[0].Value));
            }
            Layer.SelectedFeatures.Clear();
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(i));
            }
            foreach (int i in selectedFeatureId)
            {
                Layer.SelectedFeatures.RemoveAt(i);
            }
            CanAffectLayerSelection = false;
            ReloadPropList();
            CanAffectLayerSelection = true;
        }
    }
}
