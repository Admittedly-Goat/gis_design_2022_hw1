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
    public partial class ChangeFieldName : Form
    {
        MyMapObjects.moMapLayer Layer;
        public int SelectedFieldIndex = -1;
        public string NewName;
        public ChangeFieldName(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            Layer = layer;
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                comboBox1.Items.Add(layer.AttributeFields.GetItem(i).Name);
            }
        }

        private void ChangeFieldName_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedFieldIndex = comboBox1.SelectedIndex;
            NewName = textBox1.Text;
            this.Close();
        }
    }
}
