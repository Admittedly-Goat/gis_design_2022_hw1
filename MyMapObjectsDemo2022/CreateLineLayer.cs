﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class CreateLineLayer : Form
    {
        public CreateLineLayer()
        {
            InitializeComponent();
        }

        public event DelegateCreateLayer CreateLayer;

        private void button1_Click(object sender, EventArgs e)
        {
            MyMapObjects.moMapLayer sLayer = new MyMapObjects.moMapLayer();
            var renderer = new MyMapObjects.moSimpleRenderer();
            renderer.Symbol = new MyMapObjects.moSimpleLineSymbol();
            sLayer.Renderer = renderer;
            sLayer.changeName(textBox1.Text);
            sLayer.changeShapeType(MyMapObjects.moGeometryTypeConstant.MultiPolyline);
            newLayer(sLayer);
            this.Close();
        }

        private void newLayer(MyMapObjects.moMapLayer layer)
        {
            CreateLayer(layer);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateLineLayer_Load(object sender, EventArgs e)
        {

        }
    }
}
