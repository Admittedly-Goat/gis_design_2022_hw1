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
    public partial class UniqueValuePolygon : Form
    {
        private MyMapObjects.moMapLayer moMapLayer;
        private MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol;
        private MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer;
        public UniqueValuePolygon(MyMapObjects.moMapLayer moMapLayer, MyMapObjects.moUniqueValueRenderer moUniqueValueRenderer, MyMapObjects.moSimpleFillSymbol moSimpleFillSymbol)
        {
            InitializeComponent();
            this.moMapLayer = moMapLayer;
            this.moSimpleFillSymbol = moSimpleFillSymbol;
            this.moUniqueValueRenderer = moUniqueValueRenderer;
            int count = moMapLayer.AttributeFields.Count;
            for (int i = 0; i < count; i++)
            {
                listBox1.Items.Add(moMapLayer.AttributeFields.GetItem(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moUniqueValueRenderer.Field = listBox1.SelectedItem.ToString();
            List<object> sNames = new List<object>();
            Int32 sFeatureCount = moMapLayer.Features.Count;
            for (Int32 i = 0; i <= sFeatureCount - 1; i++)
            {
                object sName = moMapLayer.Features.GetItem(i).Attributes.GetItem(listBox1.SelectedIndex);
                sNames.Add(sName);
            }
            sNames.Distinct().ToList();
            Int32 sValueCount = sNames.Count;
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                moUniqueValueRenderer.AddUniqueValue(sNames[i].ToString(), sSymbol);
            }
            moUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
