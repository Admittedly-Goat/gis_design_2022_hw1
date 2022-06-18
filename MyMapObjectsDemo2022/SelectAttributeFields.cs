using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class SelectAttributeFields : Form
    {
        private readonly MyMapObjects.moMapLayer Layer;
        public int SelectedFieldIndex = -1;
        public SelectAttributeFields(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            Layer = layer;
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                _ = comboBox1.Items.Add(layer.AttributeFields.GetItem(i).Name);
            }
        }

        private void SelectAttributeFields_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("您没有选择字段，或者选择的字段名称不正确。");
                return;
            }
            else
            {
                SelectedFieldIndex = comboBox1.SelectedIndex;
                Close();
            }
        }
    }
}
