namespace MyMapObjectsDemo2022
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("123");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ObjectID", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("sdfergvergeg");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Another prop", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssMapScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.chkShowLngLat = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.土木GISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lay文件课上实习格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存当前项目为土木GIS工程文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存图层为GeoJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.导出地图为bitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建点图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建线图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建面图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改样式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改图层注记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.详情面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.几何选取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性选取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.几何编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除已选择的图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动已选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.增加新要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.moMap = new MyMapObjects.moMapControl();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssCoordinate,
            this.tssMapScale});
            this.statusStrip1.Location = new System.Drawing.Point(0, 716);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1145, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssCoordinate
            // 
            this.tssCoordinate.AutoSize = false;
            this.tssCoordinate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssCoordinate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssCoordinate.Name = "tssCoordinate";
            this.tssCoordinate.Size = new System.Drawing.Size(200, 19);
            this.tssCoordinate.Text = "#";
            // 
            // tssMapScale
            // 
            this.tssMapScale.AutoSize = false;
            this.tssMapScale.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssMapScale.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssMapScale.Name = "tssMapScale";
            this.tssMapScale.Size = new System.Drawing.Size(200, 19);
            this.tssMapScale.Text = "#";
            this.tssMapScale.Click += new System.EventHandler(this.tssMapScale_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(972, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 664);
            this.panel1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode9.Name = "Node1";
            treeNode9.Text = "123";
            treeNode10.Name = "Node0";
            treeNode10.Text = "ObjectID";
            treeNode11.Name = "Node3";
            treeNode11.Text = "sdfergvergeg";
            treeNode12.Name = "Node2";
            treeNode12.Text = "Another prop";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode12});
            this.treeView1.Size = new System.Drawing.Size(173, 664);
            this.treeView1.TabIndex = 0;
            // 
            // chkShowLngLat
            // 
            this.chkShowLngLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowLngLat.AutoSize = true;
            this.chkShowLngLat.Location = new System.Drawing.Point(406, 719);
            this.chkShowLngLat.Name = "chkShowLngLat";
            this.chkShowLngLat.Size = new System.Drawing.Size(96, 16);
            this.chkShowLngLat.TabIndex = 35;
            this.chkShowLngLat.Text = "显示地理坐标";
            this.chkShowLngLat.UseVisualStyleBackColor = true;
            this.chkShowLngLat.CheckedChanged += new System.EventHandler(this.chkShowLngLat_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出ToolStripMenuItem,
            this.图层ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1145, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayer,
            this.保存ToolStripMenuItem,
            this.导出ToolStripMenuItem1});
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.导出ToolStripMenuItem.Text = "文件";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // addLayer
            // 
            this.addLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.土木GISToolStripMenuItem,
            this.geoJSONToolStripMenuItem,
            this.lay文件课上实习格式ToolStripMenuItem});
            this.addLayer.Name = "addLayer";
            this.addLayer.Size = new System.Drawing.Size(100, 22);
            this.addLayer.Text = "打开";
            // 
            // 土木GISToolStripMenuItem
            // 
            this.土木GISToolStripMenuItem.Name = "土木GISToolStripMenuItem";
            this.土木GISToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.土木GISToolStripMenuItem.Text = "土木GIS项目文件";
            // 
            // geoJSONToolStripMenuItem
            // 
            this.geoJSONToolStripMenuItem.Name = "geoJSONToolStripMenuItem";
            this.geoJSONToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.geoJSONToolStripMenuItem.Text = "GeoJSON";
            this.geoJSONToolStripMenuItem.Click += new System.EventHandler(this.geoJSONToolStripMenuItem_Click);
            // 
            // lay文件课上实习格式ToolStripMenuItem
            // 
            this.lay文件课上实习格式ToolStripMenuItem.Name = "lay文件课上实习格式ToolStripMenuItem";
            this.lay文件课上实习格式ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.lay文件课上实习格式ToolStripMenuItem.Text = "Lay文件(课上实习格式)";
            this.lay文件课上实习格式ToolStripMenuItem.Click += new System.EventHandler(this.lay文件课上实习格式ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存当前项目为土木GIS工程文件ToolStripMenuItem,
            this.保存图层为GeoJSONToolStripMenuItem});
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 保存当前项目为土木GIS工程文件ToolStripMenuItem
            // 
            this.保存当前项目为土木GIS工程文件ToolStripMenuItem.Name = "保存当前项目为土木GIS工程文件ToolStripMenuItem";
            this.保存当前项目为土木GIS工程文件ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.保存当前项目为土木GIS工程文件ToolStripMenuItem.Text = "土木GIS工程文件";
            // 
            // 保存图层为GeoJSONToolStripMenuItem
            // 
            this.保存图层为GeoJSONToolStripMenuItem.Name = "保存图层为GeoJSONToolStripMenuItem";
            this.保存图层为GeoJSONToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.保存图层为GeoJSONToolStripMenuItem.Text = "GeoJSON";
            this.保存图层为GeoJSONToolStripMenuItem.Click += new System.EventHandler(this.保存图层为GeoJSONToolStripMenuItem_Click);
            // 
            // 导出ToolStripMenuItem1
            // 
            this.导出ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出地图为bitmapToolStripMenuItem});
            this.导出ToolStripMenuItem1.Name = "导出ToolStripMenuItem1";
            this.导出ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.导出ToolStripMenuItem1.Text = "导出";
            // 
            // 导出地图为bitmapToolStripMenuItem
            // 
            this.导出地图为bitmapToolStripMenuItem.Name = "导出地图为bitmapToolStripMenuItem";
            this.导出地图为bitmapToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.导出地图为bitmapToolStripMenuItem.Text = "导出地图为bitmap";
            // 
            // 图层ToolStripMenuItem
            // 
            this.图层ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.管理ToolStripMenuItem,
            this.详情面板ToolStripMenuItem});
            this.图层ToolStripMenuItem.Name = "图层ToolStripMenuItem";
            this.图层ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.图层ToolStripMenuItem.Text = "图层";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建点图层ToolStripMenuItem,
            this.新建线图层ToolStripMenuItem,
            this.新建面图层ToolStripMenuItem});
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 新建点图层ToolStripMenuItem
            // 
            this.新建点图层ToolStripMenuItem.Name = "新建点图层ToolStripMenuItem";
            this.新建点图层ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.新建点图层ToolStripMenuItem.Text = "新建点图层";
            // 
            // 新建线图层ToolStripMenuItem
            // 
            this.新建线图层ToolStripMenuItem.Name = "新建线图层ToolStripMenuItem";
            this.新建线图层ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.新建线图层ToolStripMenuItem.Text = "新建线图层";
            // 
            // 新建面图层ToolStripMenuItem
            // 
            this.新建面图层ToolStripMenuItem.Name = "新建面图层ToolStripMenuItem";
            this.新建面图层ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.新建面图层ToolStripMenuItem.Text = "新建面图层";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 管理ToolStripMenuItem
            // 
            this.管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改样式ToolStripMenuItem,
            this.更改图层注记ToolStripMenuItem,
            this.修改名称ToolStripMenuItem});
            this.管理ToolStripMenuItem.Name = "管理ToolStripMenuItem";
            this.管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.管理ToolStripMenuItem.Text = "管理";
            // 
            // 修改样式ToolStripMenuItem
            // 
            this.修改样式ToolStripMenuItem.Name = "修改样式ToolStripMenuItem";
            this.修改样式ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改样式ToolStripMenuItem.Text = "修改渲染样式";
            // 
            // 更改图层注记ToolStripMenuItem
            // 
            this.更改图层注记ToolStripMenuItem.Name = "更改图层注记ToolStripMenuItem";
            this.更改图层注记ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.更改图层注记ToolStripMenuItem.Text = "更改图层注记";
            // 
            // 修改名称ToolStripMenuItem
            // 
            this.修改名称ToolStripMenuItem.Name = "修改名称ToolStripMenuItem";
            this.修改名称ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改名称ToolStripMenuItem.Text = "更改名称";
            // 
            // 详情面板ToolStripMenuItem
            // 
            this.详情面板ToolStripMenuItem.Name = "详情面板ToolStripMenuItem";
            this.详情面板ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.详情面板ToolStripMenuItem.Text = "详细信息面板";
            this.详情面板ToolStripMenuItem.Click += new System.EventHandler(this.详情面板ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.几何选取ToolStripMenuItem,
            this.属性选取ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.查询ToolStripMenuItem.Text = "选择";
            // 
            // 几何选取ToolStripMenuItem
            // 
            this.几何选取ToolStripMenuItem.Name = "几何选取ToolStripMenuItem";
            this.几何选取ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.几何选取ToolStripMenuItem.Text = "几何选取";
            // 
            // 属性选取ToolStripMenuItem
            // 
            this.属性选取ToolStripMenuItem.Name = "属性选取ToolStripMenuItem";
            this.属性选取ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.属性选取ToolStripMenuItem.Text = "属性选取";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.几何编辑ToolStripMenuItem,
            this.属性表ToolStripMenuItem,
            this.坐标系统设置ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 几何编辑ToolStripMenuItem
            // 
            this.几何编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除已选择的图形ToolStripMenuItem,
            this.移动已选择ToolStripMenuItem,
            this.增加新要素ToolStripMenuItem,
            this.编辑节点ToolStripMenuItem});
            this.几何编辑ToolStripMenuItem.Name = "几何编辑ToolStripMenuItem";
            this.几何编辑ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.几何编辑ToolStripMenuItem.Text = "几何编辑";
            // 
            // 删除已选择的图形ToolStripMenuItem
            // 
            this.删除已选择的图形ToolStripMenuItem.Name = "删除已选择的图形ToolStripMenuItem";
            this.删除已选择的图形ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除已选择的图形ToolStripMenuItem.Text = "删除已选择";
            // 
            // 移动已选择ToolStripMenuItem
            // 
            this.移动已选择ToolStripMenuItem.Name = "移动已选择ToolStripMenuItem";
            this.移动已选择ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.移动已选择ToolStripMenuItem.Text = "移动已选择";
            // 
            // 增加新要素ToolStripMenuItem
            // 
            this.增加新要素ToolStripMenuItem.Name = "增加新要素ToolStripMenuItem";
            this.增加新要素ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.增加新要素ToolStripMenuItem.Text = "增加新要素";
            // 
            // 编辑节点ToolStripMenuItem
            // 
            this.编辑节点ToolStripMenuItem.Name = "编辑节点ToolStripMenuItem";
            this.编辑节点ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.编辑节点ToolStripMenuItem.Text = "编辑图形节点";
            // 
            // 属性表ToolStripMenuItem
            // 
            this.属性表ToolStripMenuItem.Name = "属性表ToolStripMenuItem";
            this.属性表ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.属性表ToolStripMenuItem.Text = "属性表与属性编辑";
            this.属性表ToolStripMenuItem.Click += new System.EventHandler(this.属性表ToolStripMenuItem_Click);
            // 
            // 坐标系统设置ToolStripMenuItem
            // 
            this.坐标系统设置ToolStripMenuItem.Name = "坐标系统设置ToolStripMenuItem";
            this.坐标系统设置ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.坐标系统设置ToolStripMenuItem.Text = "坐标系统设置";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1145, 28);
            this.panel2.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1145, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(87, 22);
            this.toolStripButton1.Text = "全范围显示";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton2.Text = "放大";
            this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton3.Text = "缩小";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton4.Text = "漫游";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton5.Text = "查询";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton6.Text = "选择";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.splitter3);
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1145, 664);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.splitter1);
            this.panel4.Controls.Add(this.moMap);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(196, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(768, 664);
            this.panel4.TabIndex = 6;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 664);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(964, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(8, 664);
            this.splitter3.TabIndex = 9;
            this.splitter3.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(188, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(8, 664);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.checkedListBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(188, 664);
            this.panel5.TabIndex = 8;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(188, 664);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.SelectedValueChanged += new System.EventHandler(this.checkedListBox1_SelectedValueChanged);
            this.checkedListBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseUp);
            // 
            // moMap
            // 
            this.moMap.BackColor = System.Drawing.Color.White;
            this.moMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.moMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moMap.FlashColor = System.Drawing.Color.Green;
            this.moMap.Location = new System.Drawing.Point(0, 0);
            this.moMap.Name = "moMap";
            this.moMap.SelectionColor = System.Drawing.Color.Cyan;
            this.moMap.Size = new System.Drawing.Size(768, 664);
            this.moMap.TabIndex = 5;
            this.moMap.MapScaleChanged += new MyMapObjects.moMapControl.MapScaleChangedHandle(this.moMap_MapScaleChanged);
            this.moMap.LayerChanged += new MyMapObjects.moMapControl.MapScaleChangedHandle(this.moMap_LayerChanged);
            this.moMap.AfterTrackingLayerDraw += new MyMapObjects.moMapControl.AfterTrackingLayerDrawHandle(this.moMap_AfterTrackingLayerDraw);
            this.moMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseClick);
            this.moMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseDown);
            this.moMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseMove);
            this.moMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseUp);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 740);
            this.Controls.Add(this.chkShowLngLat);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "土木GIS";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel tssCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel tssMapScale;
        private System.Windows.Forms.CheckBox chkShowLngLat;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel2;
        private MyMapObjects.moMapControl moMap;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripMenuItem 图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建点图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建线图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建面图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改样式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改图层注记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 详情面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 几何选取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性选取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLayer;
        private System.Windows.Forms.ToolStripMenuItem 土木GISToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存当前项目为土木GIS工程文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存图层为GeoJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 导出地图为bitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 几何编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除已选择的图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动已选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 增加新要素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lay文件课上实习格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标系统设置ToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter3;
    }
}

