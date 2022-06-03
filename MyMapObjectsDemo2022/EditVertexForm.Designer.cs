namespace MyMapObjectsDemo2022
{
    partial class EditVertexForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("第0点");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("第1点");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("第0部分", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.GeomTree = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.几何选点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建部分ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在选择部分前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在选择部分后ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在选择节点前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在选择节点后ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除部分ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动选定节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeomTree
            // 
            this.GeomTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeomTree.Location = new System.Drawing.Point(103, 0);
            this.GeomTree.Name = "GeomTree";
            treeNode1.Name = "Node1";
            treeNode1.Text = "第0点";
            treeNode2.Name = "Node2";
            treeNode2.Text = "第1点";
            treeNode3.Name = "Node0";
            treeNode3.Text = "第0部分";
            this.GeomTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.GeomTree.Size = new System.Drawing.Size(218, 503);
            this.GeomTree.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.几何选点ToolStripMenuItem,
            this.新建部分ToolStripMenuItem,
            this.新建节点ToolStripMenuItem,
            this.删除部分ToolStripMenuItem,
            this.删除节点ToolStripMenuItem,
            this.移动选定节点ToolStripMenuItem,
            this.退出编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(103, 503);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 几何选点ToolStripMenuItem
            // 
            this.几何选点ToolStripMenuItem.Name = "几何选点ToolStripMenuItem";
            this.几何选点ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.几何选点ToolStripMenuItem.Text = "几何选点";
            // 
            // 新建部分ToolStripMenuItem
            // 
            this.新建部分ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在选择部分前ToolStripMenuItem,
            this.在选择部分后ToolStripMenuItem});
            this.新建部分ToolStripMenuItem.Name = "新建部分ToolStripMenuItem";
            this.新建部分ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.新建部分ToolStripMenuItem.Text = "新建部分";
            // 
            // 新建节点ToolStripMenuItem
            // 
            this.新建节点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在选择节点前ToolStripMenuItem,
            this.在选择节点后ToolStripMenuItem});
            this.新建节点ToolStripMenuItem.Name = "新建节点ToolStripMenuItem";
            this.新建节点ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.新建节点ToolStripMenuItem.Text = "新建节点";
            // 
            // 在选择部分前ToolStripMenuItem
            // 
            this.在选择部分前ToolStripMenuItem.Name = "在选择部分前ToolStripMenuItem";
            this.在选择部分前ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.在选择部分前ToolStripMenuItem.Text = "在选择部分前";
            // 
            // 在选择部分后ToolStripMenuItem
            // 
            this.在选择部分后ToolStripMenuItem.Name = "在选择部分后ToolStripMenuItem";
            this.在选择部分后ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.在选择部分后ToolStripMenuItem.Text = "在选择部分后";
            // 
            // 在选择节点前ToolStripMenuItem
            // 
            this.在选择节点前ToolStripMenuItem.Name = "在选择节点前ToolStripMenuItem";
            this.在选择节点前ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.在选择节点前ToolStripMenuItem.Text = "在选择节点前";
            // 
            // 在选择节点后ToolStripMenuItem
            // 
            this.在选择节点后ToolStripMenuItem.Name = "在选择节点后ToolStripMenuItem";
            this.在选择节点后ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.在选择节点后ToolStripMenuItem.Text = "在选择节点后";
            // 
            // 删除部分ToolStripMenuItem
            // 
            this.删除部分ToolStripMenuItem.Name = "删除部分ToolStripMenuItem";
            this.删除部分ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.删除部分ToolStripMenuItem.Text = "删除选定部分";
            // 
            // 删除节点ToolStripMenuItem
            // 
            this.删除节点ToolStripMenuItem.Name = "删除节点ToolStripMenuItem";
            this.删除节点ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.删除节点ToolStripMenuItem.Text = "删除选定节点";
            // 
            // 退出编辑ToolStripMenuItem
            // 
            this.退出编辑ToolStripMenuItem.Name = "退出编辑ToolStripMenuItem";
            this.退出编辑ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.退出编辑ToolStripMenuItem.Text = "退出编辑";
            // 
            // 移动选定节点ToolStripMenuItem
            // 
            this.移动选定节点ToolStripMenuItem.Name = "移动选定节点ToolStripMenuItem";
            this.移动选定节点ToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.移动选定节点ToolStripMenuItem.Text = "移动选定节点";
            // 
            // EditVertexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 503);
            this.Controls.Add(this.GeomTree);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditVertexForm";
            this.Text = "EditVertexForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView GeomTree;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 几何选点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建部分ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在选择部分前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在选择部分后ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在选择节点前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在选择节点后ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除部分ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动选定节点ToolStripMenuItem;
    }
}