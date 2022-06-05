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
    public partial class ChangeLayerOrder : Form
    {
        private MyMapObjects.moLayers moLayers;
        private MyMapObjects.moMapControl moMap;
        private const int ITEM_PADDING = 10;//各项之间的边距

        public ChangeLayerOrder(MyMapObjects.moLayers moLayers, MyMapObjects.moMapControl moMap)
        {
            InitializeComponent();
            this.moLayers = moLayers;
            this.moMap = moMap;
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.DrawItem += ListBox1_DrawItem;
            listBox1.MeasureItem += ListBox1_MeasureItem;
            for (int i = 0; i < moLayers.Count; i++)
            {
                MyMapObjects.moMapLayer moMapLayer = moLayers.GetItem(i);
                string item = moLayers.GetItem(i).Name+"\n" + "\n";
                item += "  -图层类型：" + moMapLayer.ShapeType.ToString() + "\n" + "\n";
                item += "  -图层字段："+ "\n";
                for (int j = 0; j < moMapLayer.AttributeFields.Count; j++)
                {
                    MyMapObjects.moField moField = moMapLayer.AttributeFields.GetItem(j);
                    item += "    -" + moMapLayer.AttributeFields.GetItem(j).Name +"\n";

                }

                item += "\n"+"  -渲染类型：" + moMapLayer.Renderer.RendererType.ToString() + "\n" + "\n";

                if (moMapLayer.LabelRenderer == null)
                {
                    item += "  -未设置注记";
                }
                else
                {
                    string isvisible = moMapLayer.LabelRenderer.LabelFeatures.ToString();
                    item += "  -注记可见：" + isvisible + "\n" + "\n";
                    if (moMapLayer.LabelRenderer.LabelFeatures == true)
                    {
                        item += "    -注记字段：" + moMapLayer.LabelRenderer.Field + "\n" + "\n";
                    }

                }

                


                this.listBox1.Items.Add(item);
            }

        }
        private void ListBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            int index = e.Index;//获取当前要进⾏绘制的⾏的序号，从0开始。
            if (index < 0)
            {
                return;
            }
            string text = Convert.ToString(listBox.Items[index]);
            //超范围后⾃动换⾏
            Size size = TextRenderer.MeasureText("[" + index + "]" + text, listBox.Font, listBox.Size, TextFormatFlags.WordBreak);
            e.ItemWidth = size.Width;
            if (index == this.listBox1.Items.Count - 1)
            {
                e.ItemHeight = size.Height + ITEM_PADDING * 3;//适当多⼀点⾼度，避免太挤s
            }
            else
            {
                e.ItemHeight = size.Height + ITEM_PADDING * 2;//适当多⼀点⾼度，避免太挤s
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index;//获取当前要进⾏绘制的⾏的序号，从0开始。
            if (index < 0)
            {
                return;
            }
            ListBox listBox = sender as ListBox;
            e.DrawBackground();//画背景颜⾊
            e.DrawFocusRectangle();//画聚焦项的边框
            Graphics g = e.Graphics;//获取Graphics对象。
            Rectangle itemBounds = e.Bounds;//获取当前要绘制的⾏的⼀个矩形范围。
                                            //⽂字绘制的区域，留出⼀定间隔
            Rectangle textBounds = new Rectangle(itemBounds.X, itemBounds.Y + ITEM_PADDING, itemBounds.Width,
            itemBounds.Height);
            string text = Convert.ToString(listBox.Items[index]);
            //因为⽂本可能会⾮常长，因此要⽤⾃绘实现ListBox项⽬的⾃动换⾏
            TextRenderer.DrawText(g, "[" + index + "]" + text, e.Font, textBounds, e.ForeColor,
            TextFormatFlags.WordBreak);
            g.DrawRectangle(Pens.Blue, itemBounds);//画每⼀项的边框，这样清楚分出来各项。
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            Point point = listBox1.PointToClient(new Point(e.X, e.Y));
            int index_to = this.listBox1.IndexFromPoint(point);
            if (index_to < 0)
            {
                index_to = this.listBox1.Items.Count - 1;
            }
            //获取拖放的数据内容
            object data = e.Data.GetData(typeof(string));
            //获取拖放的数据在原先图层中的顺序
            int index_from = -1;
            for (int i = 0; i < this.listBox1.Items.Count; i++)
            {
                if (this.listBox1.Items[i] == data)
                {
                    index_from = i;
                    break;
                }
            }
            //删除元数据
            this.listBox1.Items.Remove(data);
            //插入目标数据
            this.listBox1.Items.Insert(index_to, data);

            this.moLayers.MoveTo(index_from, index_to);
            this.moMap.RefreshLayerList();
            this.moMap.RedrawMap();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
            {
                return;
            }
            //开始拖放操作，DragDropEffects为枚举类型。
            //DragDropEffects.Move 为将源数据移动到目标数据
            this.listBox1.DoDragDrop(this.listBox1.SelectedItem, DragDropEffects.Move);
        }
    }
}
