using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class OperatorTypeSelection : Form
    {
        public int OperatorType;
        public OperatorTypeSelection()
        {
            InitializeComponent();
        }

        private void OperatorTypeSelection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("您没有选择操作符类型。");
                return;
            }
            OperatorType = comboBox1.SelectedIndex;
            Close();
        }
    }
}
