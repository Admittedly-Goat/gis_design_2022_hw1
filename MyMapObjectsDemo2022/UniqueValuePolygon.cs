using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class UniqueValuePolygon : Form
    {
        private readonly MyMapObjects.moMapLayer moMapLayer;
        private readonly MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol;
        private readonly MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer;
        public UniqueValuePolygon(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer, MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleFillSymbol = moSimpleFillSymbol;
            this.moUniqueValueRenderer = moUniqueValueRenderer;
            int count = moMapLayer.AttributeFields.Count;
            for (int i = 0; i < count; i++)
            {
                _ = listBox1.Items.Add(moMapLayer.AttributeFields.GetItem(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _ = MessageBox.Show("未选择绑定字段");
            }
            else
            {
                moUniqueValueRenderer.Field = listBox1.SelectedItem.ToString();
                List<object> sNames = new List<object>();
                int sFeatureCount = moMapLayer.Features.Count;
                for (int i = 0; i <= sFeatureCount - 1; i++)
                {
                    object sName = moMapLayer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                    sNames.Add(sName);
                }
                _ = sNames.Distinct().ToList();
                int sValueCount = sNames.Count;
                for (int i = 0; i <= sValueCount - 1; i++)
                {
                    MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                    moUniqueValueRenderer.AddUniqueValue(sNames[i].ToString(), sSymbol);
                }
                moUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
                Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
