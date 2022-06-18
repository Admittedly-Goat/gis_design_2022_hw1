using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    public partial class LabelRenderer : Form
    {
        private readonly MyMapObjects.moMapLayer sLayer;
        private readonly MyMapObjects.moLabelRenderer sLabelRenderer;
        private readonly MyMapObjects.moMapControl moMap;

        public LabelRenderer(MyMapObjects.moMapLayer sLayer, MyMapObjects.moLabelRenderer sLabelRenderer, MyMapObjects.moMapControl moMap)
        {
            InitializeComponent();
            this.sLayer = sLayer;
            this.sLabelRenderer = sLabelRenderer;
            this.moMap = moMap;

            int count = sLayer.AttributeFields.Count;
            //初始化选择标记的字段名称
            for (int i = 0; i < count; i++)
            {
                _ = listBox1.Items.Add(sLayer.AttributeFields.GetItem(i).Name);
            }
            //初始化字体下拉框
            //StringBuilder str = new StringBuilder(2000);
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                _ = comboBoxFontFamily.Items.Add(family.Name);
            }


        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            //设置注记图层
            sLabelRenderer.Field = listBox1.SelectedIndex == -1 ? sLayer.AttributeFields.GetItem(0).Name : listBox1.SelectedItem.ToString();

            //设置是否注记
            sLabelRenderer.LabelFeatures = checkBoxShowLabel.Checked;

            //字体样式
            string FontFamily = comboBoxFontFamily.Text;

            //字体大小
            //使用try，catch检测输入
            float FontSize = 12;
            try
            {
                FontSize = float.Parse(textBoxFontSize.Text);
            }
            catch (OverflowException)
            {
                _ = MessageBox.Show("err:转化的不是一个float型数据");
                textBoxFontSize.Text = "12";
            }
            catch (FormatException)
            {
                _ = MessageBox.Show("err:格式错误");
                textBoxFontSize.Text = "12";
            }
            catch (ArgumentNullException)
            {
                _ = MessageBox.Show("err:null");
                textBoxFontSize.Text = "12";
            }
            //设定范围在5--72之间
            if (FontSize < 5 || FontSize > 72)
            {
                _ = MessageBox.Show("err:请输入5-72之间的数");
                textBoxFontSize.Text = "12";
            }
            else
            {
                sLabelRenderer.TextSymbol.Font = new Font(FontFamily, FontSize);
            }



            //描边
            sLabelRenderer.TextSymbol.UseMask = checkBoxMask.Checked;

            //描边宽度
            double MaskWidth = 0.5;
            try
            {
                MaskWidth = float.Parse(textBoxMaskWidth.Text);
            }
            catch (OverflowException)
            {
                _ = MessageBox.Show("err:转化的不是一个float型数据");
                textBoxMaskWidth.Text = "0.5";
            }
            catch (FormatException)
            {
                _ = MessageBox.Show("err:格式错误");
                textBoxMaskWidth.Text = "0.5";
            }
            catch (ArgumentNullException)
            {
                _ = MessageBox.Show("err:null");
                textBoxMaskWidth.Text = "0.5";
            }
            //设定范围在5--72之间
            if (MaskWidth < 0 || MaskWidth > 10)
            {
                _ = MessageBox.Show("err:请输入0-10之间的数");
                textBoxMaskWidth.Text = "0.5";
            }
            else
            {
                sLabelRenderer.TextSymbol.MaskWidth = MaskWidth;
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
                sLabelRenderer.TextSymbol.FontColor = colorDialog1.Color;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMaskColor_Click(object sender, EventArgs e)
        {
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //选择符号颜色
            if (dr == DialogResult.OK)
            {
                sLabelRenderer.TextSymbol.MaskColor = colorDialog1.Color;
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
