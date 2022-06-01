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
    public partial class ChangeNameOfLayer : Form
    {
        private MyMapObjects.moMapLayer moMapLayer;
        public ChangeNameOfLayer(MyMapObjects.moMapLayer moMapLayer)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
        }

        private void ChangeNameOfLayer_Load(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            moMapLayer.changeName(newLayerName.Text);
            this.Close();
        }
    }
}
