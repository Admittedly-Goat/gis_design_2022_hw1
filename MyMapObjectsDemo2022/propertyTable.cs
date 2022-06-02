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
    public partial class propertyTable : Form
    {
        Action frmMainSetPropertyTableNullMethod;
        Action RedrawMoMap;
        MyMapObjects.moMapLayer Layer;
        public propertyTable(MyMapObjects.moMapLayer layer, Action frmMainSetPropertyTableNullMethod, Action redrawMoMap)
        {
            InitializeComponent();
            this.Layer = layer;
            this.frmMainSetPropertyTableNullMethod = frmMainSetPropertyTableNullMethod;
            this.RedrawMoMap = redrawMoMap;
            ReloadPropList();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        internal void ReloadPropList()
        {
            propertyGrid.Rows.Clear();
            propertyGrid.Columns.Clear();
            bool isColumnEmpty = true;
            propertyGrid.Columns.Add("要素内部ID", "要素内部ID");
            propertyGrid.Columns["要素内部ID"].Frozen = true;
            propertyGrid.Columns["要素内部ID"].ReadOnly = true;
            for (int i = 0; i < Layer.AttributeFields.Count; i++)
            {
                propertyGrid.Columns.Add(i.ToString(), Layer.AttributeFields.GetItem(i).Name);
                isColumnEmpty = false;
            }
            if (isColumnEmpty)
            {
                return;
            }
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                object[] values = new object[Layer.AttributeFields.Count + 1];
                values[0] = i;
                for (int j = 0; j < values.Length - 1; j++)
                {
                    values[j + 1] = Layer.Features.GetItem(i).Attributes.GetItem(j);
                }
                propertyGrid.Rows.Add(values);
            }
        }

        private void propertyTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMainSetPropertyTableNullMethod();
        }

        private void 添加字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string inputName = Microsoft.VisualBasic.Interaction.InputBox("新字段名称", "添加新字段");
            if ((inputName != null) && (inputName != ""))
            {
                MyMapObjects.moValueTypeConstant selectedAttributeType;
                for (int i = 0; i < Layer.AttributeFields.Count; i++)
                {
                    if (Layer.AttributeFields.GetItem(i).Name == inputName)
                    {
                        MessageBox.Show("字段已经存在。");
                        return;
                    }
                }
                int selectedTypeIndex = -1;
                SelectAttributeType attributeTypeForm = new SelectAttributeType();
                var result = attributeTypeForm.ShowDialog();
                selectedTypeIndex = attributeTypeForm.TypeIndex;
                if (selectedTypeIndex == 0)
                {
                    selectedAttributeType = MyMapObjects.moValueTypeConstant.dSingle;
                }
                else if (selectedTypeIndex == 1)
                {
                    selectedAttributeType = MyMapObjects.moValueTypeConstant.dDouble;
                }
                else if (selectedTypeIndex == 2)
                {
                    selectedAttributeType = MyMapObjects.moValueTypeConstant.dInt32;
                }
                else if (selectedTypeIndex == 3)
                {
                    selectedAttributeType = MyMapObjects.moValueTypeConstant.dInt64;
                }
                else if (selectedTypeIndex == 4)
                {
                    selectedAttributeType = MyMapObjects.moValueTypeConstant.dText;
                }
                string inputDefaultValue;
                object convertedTypeValue;
                while (true)
                {
                    inputDefaultValue = Microsoft.VisualBasic.Interaction.InputBox("默认值", "请输入默认值");
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
                        break;
                    }
                    catch
                    {
                        MessageBox.Show("输入的默认值有误，请重试。");
                    }
                }
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
                MessageBox.Show("输入的字段名称无效，请重试。");
            }
        }

        private void 删除字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string inputName = Microsoft.VisualBasic.Interaction.InputBox("需要删除的字段名", "删除字段");
            for (int i = 0; i < Layer.AttributeFields.Count; i++)
            {
                if (Layer.AttributeFields.GetItem(i).Name == inputName)
                {
                    Layer.AttributeFields.RemoveAt(i);
                    ReloadPropList();
                    return;
                }
            }
            MessageBox.Show("没有找到字段。");
            return;
        }
    }
}
