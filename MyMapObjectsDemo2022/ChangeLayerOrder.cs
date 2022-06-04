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

        public ChangeLayerOrder(MyMapObjects.moLayers moLayers, MyMapObjects.moMapControl moMap)
        {
            InitializeComponent();
            this.moLayers = moLayers;
            this.moMap = moMap;
            for (int i = 0; i < moLayers.Count; i++)
            {
                this.listBox1.Items.Add(moLayers.GetItem(i).Name);
            }
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
