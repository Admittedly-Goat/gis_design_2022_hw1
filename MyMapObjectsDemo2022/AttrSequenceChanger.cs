using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class AttrSequenceChanger : Form
    {
        private readonly MyMapObjects.moMapLayer Layer;
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
                _ = listBox1.Items.Add(Layer.AttributeFields.GetItem(i).Name);
            }
        }

        private void AttrSequenceChanger_Load(object sender, EventArgs e)
        {

        }

        private void Finish_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("您还未选择任何字段。");
                return;
            }
            if (listBox1.SelectedIndex == 0)
            {
                return;
            }
            int selectedIndex = listBox1.SelectedIndex;
            MyMapObjects.moField selectedField = Layer.AttributeFields.GetItem(selectedIndex);
            Layer.AttributeFields.SetField(listBox1.SelectedIndex, Layer.AttributeFields.GetItem(listBox1.SelectedIndex - 1));
            Layer.AttributeFields.SetField(listBox1.SelectedIndex - 1, selectedField);
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                object selectedValue = Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex, Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex - 1));
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex - 1, selectedValue);
            }
            ReloadAttr();
            listBox1.SelectedIndex = selectedIndex - 1;
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("您还未选择任何字段。");
                return;
            }
            if (listBox1.SelectedIndex == Layer.AttributeFields.Count - 1)
            {
                return;
            }
            int selectedIndex = listBox1.SelectedIndex;
            MyMapObjects.moField selectedField = Layer.AttributeFields.GetItem(selectedIndex);
            Layer.AttributeFields.SetField(listBox1.SelectedIndex, Layer.AttributeFields.GetItem(listBox1.SelectedIndex + 1));
            Layer.AttributeFields.SetField(listBox1.SelectedIndex + 1, selectedField);
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                object selectedValue = Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex, Layer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex + 1));
                Layer.Features.GetItem(i).Attributes.SetItem(listBox1.SelectedIndex + 1, selectedValue);
            }
            ReloadAttr();
            listBox1.SelectedIndex = selectedIndex + 1;
        }
    }
}
