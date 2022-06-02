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
    public partial class AttrSequenceChanger : Form
    {
        MyMapObjects.moMapLayer Layer;
        public AttrSequenceChanger(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            Layer = layer;
            ReloadAttr();
        }

        private void ReloadAttr()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Layer.AttributeFields.Count; i++)
            {
                listBox1.Items.Add(Layer.AttributeFields.GetItem(i).Name);
            }
        }

        private void AttrSequenceChanger_Load(object sender, EventArgs e)
        {

        }

        private void Finish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还未选择任何字段。");
                return;
            }
            if (listBox1.SelectedIndex == 0)
            {
                return;
            }
            var selectedField = Layer.AttributeFields.GetItem(listBox1.SelectedIndex);
            Layer.AttributeFields.SetField(listBox1.SelectedIndex, Layer.AttributeFields.GetItem(listBox1.SelectedIndex - 1));
            Layer.AttributeFields.SetField(listBox1.SelectedIndex - 1, selectedField);
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                var selectedValue = Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex, Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex - 1));
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex - 1, selectedValue);
            }
            ReloadAttr();
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您还未选择任何字段。");
                return;
            }
            if (listBox1.SelectedIndex == Layer.AttributeFields.Count-1)
            {
                return;
            }
            var selectedField = Layer.AttributeFields.GetItem(listBox1.SelectedIndex);
            Layer.AttributeFields.SetField(listBox1.SelectedIndex, Layer.AttributeFields.GetItem(listBox1.SelectedIndex + 1));
            Layer.AttributeFields.SetField(listBox1.SelectedIndex + 1, selectedField);
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                var selectedValue = Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex, Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex + 1));
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex + 1, selectedValue);
            }
            ReloadAttr();
        }
    }
}
