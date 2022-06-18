using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class addFieldDialogBox : Form
    {
        public string FieldName;
        public int TypeIndex;
        public string DefaultValue;

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
            Close();
        }
    }
}
