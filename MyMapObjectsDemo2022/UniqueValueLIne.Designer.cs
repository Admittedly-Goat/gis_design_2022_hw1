
namespace MyMapObjectsDemo2022
{
    partial class UniqueValueLIne
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DashDotDot = new System.Windows.Forms.RadioButton();
            this.DashDot = new System.Windows.Forms.RadioButton();
            this.Dot = new System.Windows.Forms.RadioButton();
            this.Dash = new System.Windows.Forms.RadioButton();
            this.Solid = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(647, 615);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 615);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(379, 70);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(404, 184);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(179, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择绑定字段：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // DashDotDot
            // 
            this.DashDotDot.AutoSize = true;
            this.DashDotDot.Font = new System.Drawing.Font("宋体", 16F);
            this.DashDotDot.Location = new System.Drawing.Point(583, 357);
            this.DashDotDot.Margin = new System.Windows.Forms.Padding(4);
            this.DashDotDot.Name = "DashDotDot";
            this.DashDotDot.Size = new System.Drawing.Size(117, 31);
            this.DashDotDot.TabIndex = 44;
            this.DashDotDot.Text = "-••-••";
            this.DashDotDot.UseVisualStyleBackColor = true;
            // 
            // DashDot
            // 
            this.DashDot.AutoSize = true;
            this.DashDot.Font = new System.Drawing.Font("宋体", 16F);
            this.DashDot.Location = new System.Drawing.Point(412, 436);
            this.DashDot.Margin = new System.Windows.Forms.Padding(4);
            this.DashDot.Name = "DashDot";
            this.DashDot.Size = new System.Drawing.Size(89, 31);
            this.DashDot.TabIndex = 43;
            this.DashDot.Text = "-•-•";
            this.DashDot.UseVisualStyleBackColor = true;
            // 
            // Dot
            // 
            this.Dot.AutoSize = true;
            this.Dot.Font = new System.Drawing.Font("宋体", 16F);
            this.Dot.Location = new System.Drawing.Point(412, 357);
            this.Dot.Margin = new System.Windows.Forms.Padding(4);
            this.Dot.Name = "Dot";
            this.Dot.Size = new System.Drawing.Size(89, 31);
            this.Dot.TabIndex = 42;
            this.Dot.Text = "••••";
            this.Dot.UseVisualStyleBackColor = true;
            // 
            // Dash
            // 
            this.Dash.AutoSize = true;
            this.Dash.Font = new System.Drawing.Font("宋体", 16F);
            this.Dash.Location = new System.Drawing.Point(240, 436);
            this.Dash.Margin = new System.Windows.Forms.Padding(4);
            this.Dash.Name = "Dash";
            this.Dash.Size = new System.Drawing.Size(89, 31);
            this.Dash.TabIndex = 41;
            this.Dash.Text = "----";
            this.Dash.UseVisualStyleBackColor = true;
            // 
            // Solid
            // 
            this.Solid.AutoSize = true;
            this.Solid.Font = new System.Drawing.Font("宋体", 16F);
            this.Solid.Location = new System.Drawing.Point(240, 357);
            this.Solid.Margin = new System.Windows.Forms.Padding(4);
            this.Solid.Name = "Solid";
            this.Solid.Size = new System.Drawing.Size(87, 31);
            this.Solid.TabIndex = 40;
            this.Solid.Text = "——";
            this.Solid.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 271);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(179, 303);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "选择线要素的形状：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(556, 548);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "毫米";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(389, 540);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 30);
            this.textBox1.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(179, 543);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "设置线要素的宽度：";
            // 
            // UniqueValueLIne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 716);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DashDotDot);
            this.Controls.Add(this.DashDot);
            this.Controls.Add(this.Dot);
            this.Controls.Add(this.Dash);
            this.Controls.Add(this.Solid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "UniqueValueLIne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "线要素唯一值渲染";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton DashDotDot;
        private System.Windows.Forms.RadioButton DashDot;
        private System.Windows.Forms.RadioButton Dot;
        private System.Windows.Forms.RadioButton Dash;
        private System.Windows.Forms.RadioButton Solid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}