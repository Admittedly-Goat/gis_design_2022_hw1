using System;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class CreatePolygonLayer : Form
    {
        public CreatePolygonLayer()
        {
            InitializeComponent();
        }

        public event DelegateCreateLayer CreateLayer;

        private void button1_Click(object sender, EventArgs e)
        {
            MyMapObjects.moMapLayer sLayer = new MyMapObjects.moMapLayer();
            MyMapObjects.moSimpleRenderer renderer = new MyMapObjects.moSimpleRenderer
            {
                Symbol = new MyMapObjects.moSimpleFillSymbol()
            };
            sLayer.Renderer = renderer;
            sLayer.changeName(textBox1.Text);
            sLayer.changeShapeType(MyMapObjects.moGeometryTypeConstant.MultiPolygon);
            newLayer(sLayer);
            Close();
        }

        private void newLayer(MyMapObjects.moMapLayer layer)
        {
            CreateLayer(layer);
        }
    }
}
