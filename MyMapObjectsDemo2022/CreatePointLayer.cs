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
    public delegate void DelegateCreateLayer(MyMapObjects.moMapLayer layer);
    public partial class CreatePointLayer : Form
    {
        public CreatePointLayer()
        {
            InitializeComponent();
        }

        
        public event DelegateCreateLayer CreateLayer;

        private void button1_Click(object sender, EventArgs e)
        {
            MyMapObjects.moMapLayer sLayer = new MyMapObjects.moMapLayer();
            sLayer.changeName(textBox1.Text);
            newLayer(sLayer);
            this.Close();
        }


        private void newLayer(MyMapObjects.moMapLayer layer)
        {
            CreateLayer(layer);
        }
    }
}
