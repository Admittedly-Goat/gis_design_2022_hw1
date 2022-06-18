using System;
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
            MyMapObjects.moSimpleRenderer renderer = new MyMapObjects.moSimpleRenderer
            {
                Symbol = new MyMapObjects.moSimpleMarkerSymbol()
            };
            sLayer.Renderer = renderer;
            sLayer.changeName(textBox1.Text);
            newLayer(sLayer);
            Close();
        }


        private void newLayer(MyMapObjects.moMapLayer layer)
        {
            CreateLayer(layer);
        }

        private void CreatePointLayer_Load(object sender, EventArgs e)
        {

        }
    }
}
