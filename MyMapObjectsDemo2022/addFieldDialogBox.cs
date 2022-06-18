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
    public partial class addFieldDialogBox : Form
    {
        public String FieldName;
        public int TypeIndex;
        public String DefaultValue;

        public addFieldDialogBox()
        {
            InitializeComponent();
        }

        private void addFieldDialogBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FieldName = textBox1.Text;
            TypeIndex = comboBox1.SelectedIndex;
            DefaultValue = textBox2.Text;
            this.Close();
        }
    }
}
