using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class CoordinateSystemChange : Form
    {
        public int coordinateSystemSelected = -1;
        public CoordinateSystemChange()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("您还未选择任何坐标系。");
                return;
            }
            coordinateSystemSelected = listBox1.SelectedIndex;
            Close();
        }
    }
}
