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
    public partial class SelectAttributeType : Form
    {
        public int TypeIndex=-1;
        public SelectAttributeType()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("您没有选择类型。");
                return;
            }
            else
            {
                TypeIndex=comboBox1.SelectedIndex;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
