using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class ChangeNameOfLayer : Form
    {
        private readonly MyMapObjects.moMapLayer moMapLayer;
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
            Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            moMapLayer.changeName(newLayerName.Text);
            Close();
        }
    }
}
