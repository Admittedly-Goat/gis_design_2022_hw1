using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace MyMapObjectsDemo2022
{
    public partial class LabelRenderer : Form
    {
        private MyMapObjects.moMapLayer sLayer;
        private MyMapObjects.moLabelRenderer sLabelRenderer;
        private MyMapObjects.moMapControl moMap;

        public LabelRenderer(MyMapObjects.moMapLayer sLayer,MyMapObjects.moLabelRenderer sLabelRenderer, MyMapObjects.moMapControl moMap)
        {
            InitializeComponent();
            this.sLayer = sLayer;
            this.sLabelRenderer = sLabelRenderer;
            this.moMap = moMap;

            int count = sLayer.AttributeFields.Count;
            //初始化选择标记的字段名称
            for (int i = 0; i < count; i++)
            {
                this.listBox1.Items.Add(sLayer.AttributeFields.GetItem(i).Name);
            }
            //初始化字体下拉框
            //StringBuilder str = new StringBuilder(2000);
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                this.comboBoxFontFamily.Items.Add(family.Name);
            }


        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            //设置注记图层
            if (this.listBox1.SelectedIndex == -1)
            {
                this.sLabelRenderer.Field = sLayer.AttributeFields.GetItem(0).Name;
            }
            else {
                this.sLabelRenderer.Field = this.listBox1.SelectedItem.ToString();
            }

            //设置是否注记
            this.sLabelRenderer.LabelFeatures = this.checkBoxShowLabel.Checked;

            //字体样式
            string FontFamily = this.comboBoxFontFamily.Text;

            //字体大小
            //使用try，catch检测输入
            float FontSize = 12;
            try
            {
                FontSize = float.Parse(this.textBoxFontSize.Text);
            }
            catch (OverflowException)
            {
                MessageBox.Show("err:转化的不是一个float型数据");
                this.textBoxFontSize.Text = "12";
            }
            catch (FormatException)
            {
                MessageBox.Show("err:格式错误");
                this.textBoxFontSize.Text = "12";
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("err:null");
                this.textBoxFontSize.Text = "12";
            }
            //设定范围在5--72之间
            if(FontSize<5 || FontSize > 72)
            {
                MessageBox.Show("err:请输入5-72之间的数");
                this.textBoxFontSize.Text = "12";
            }
            else
            {
                this.sLabelRenderer.TextSymbol.Font = new Font(FontFamily, FontSize);
            }



            //描边
            sLabelRenderer.TextSymbol.UseMask = this.checkBoxMask.Checked;

            //描边宽度
            double MaskWidth = 0.5;
            try
            {
                MaskWidth = float.Parse(this.textBoxMaskWidth.Text);
            }
            catch (OverflowException)
            {
                MessageBox.Show("err:转化的不是一个float型数据");
                this.textBoxMaskWidth.Text = "0.5";
            }
            catch (FormatException)
            {
                MessageBox.Show("err:格式错误");
                this.textBoxMaskWidth.Text = "0.5";
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("err:null");
                this.textBoxMaskWidth.Text = "0.5";
            }
            //设定范围在5--72之间
            if (MaskWidth < 0 || MaskWidth > 10)
            {
                MessageBox.Show("err:请输入0-10之间的数");
                this.textBoxMaskWidth.Text = "0.5";
            }
            else
            {
                this.sLabelRenderer.TextSymbol.MaskWidth = MaskWidth;
            }


            //渲染
            sLayer.LabelRenderer = sLabelRenderer;
            moMap.RedrawMap();

            //this.Close();


        }

        private void buttonFontColor_Click(object sender, EventArgs e)
        {
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                this.sLabelRenderer.TextSymbol.FontColor = colorDialog1.Color;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaskColor_Click(object sender, EventArgs e)
        {
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                this.sLabelRenderer.TextSymbol.MaskColor = colorDialog1.Color;
            }

        }

        //if (moMap.Layers.Count == 0)
        //        return;
        //    获取第一个图层
        //    MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(0);
        //新建一个注记渲染对象
        //MyMapObjects.moLabelRenderer sLabelRenderer = new MyMapObjects.moLabelRenderer();
        //设定绑定字段为索引号为0的字段
        //sLabelRenderer.Field = sLayer.AttributeFields.GetItem(0).Name;
        //    设置注记符号
        //    Font sOldFont = sLabelRenderer.TextSymbol.Font;
        //sLabelRenderer.TextSymbol.Font = new Font(sOldFont.Name, 12);
        //sLabelRenderer.TextSymbol.UseMask = true;
        //    sLabelRenderer.LabelFeatures = true;
        //    赋值给图层
        //    sLayer.LabelRenderer = sLabelRenderer;
        //重绘图层
        //moMap.RedrawMap();
    }
}
