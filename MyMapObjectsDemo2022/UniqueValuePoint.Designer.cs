
namespace MyMapObjectsDemo2022
{
    partial class UniqueValuePoint
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CircleCircle = new System.Windows.Forms.RadioButton();
            this.CircleDot = new System.Windows.Forms.RadioButton();
            this.SolidSquare = new System.Windows.Forms.RadioButton();
            this.Square = new System.Windows.Forms.RadioButton();
            this.SolidTriangle = new System.Windows.Forms.RadioButton();
            this.Triangle = new System.Windows.Forms.RadioButton();
            this.SolidCircle = new System.Windows.Forms.RadioButton();
            this.Circle = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(182, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择绑定字段：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(382, 37);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(404, 244);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(306, 672);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(651, 672);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CircleCircle
            // 
            this.CircleCircle.AutoSize = true;
            this.CircleCircle.Font = new System.Drawing.Font("宋体", 16F);
            this.CircleCircle.Location = new System.Drawing.Point(745, 452);
            this.CircleCircle.Margin = new System.Windows.Forms.Padding(4);
            this.CircleCircle.Name = "CircleCircle";
            this.CircleCircle.Size = new System.Drawing.Size(60, 31);
            this.CircleCircle.TabIndex = 45;
            this.CircleCircle.Text = "◎";
            this.CircleCircle.UseVisualStyleBackColor = true;
            // 
            // CircleDot
            // 
            this.CircleDot.AutoSize = true;
            this.CircleDot.Font = new System.Drawing.Font("宋体", 16F);
            this.CircleDot.Location = new System.Drawing.Point(745, 373);
            this.CircleDot.Margin = new System.Windows.Forms.Padding(4);
            this.CircleDot.Name = "CircleDot";
            this.CircleDot.Size = new System.Drawing.Size(60, 31);
            this.CircleDot.TabIndex = 44;
            this.CircleDot.Text = "☉";
            this.CircleDot.UseVisualStyleBackColor = true;
            // 
            // SolidSquare
            // 
            this.SolidSquare.AutoSize = true;
            this.SolidSquare.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidSquare.Location = new System.Drawing.Point(586, 452);
            this.SolidSquare.Margin = new System.Windows.Forms.Padding(4);
            this.SolidSquare.Name = "SolidSquare";
            this.SolidSquare.Size = new System.Drawing.Size(60, 31);
            this.SolidSquare.TabIndex = 43;
            this.SolidSquare.Text = "■";
            this.SolidSquare.UseVisualStyleBackColor = true;
            // 
            // Square
            // 
            this.Square.AutoSize = true;
            this.Square.Font = new System.Drawing.Font("宋体", 16F);
            this.Square.Location = new System.Drawing.Point(586, 373);
            this.Square.Margin = new System.Windows.Forms.Padding(4);
            this.Square.Name = "Square";
            this.Square.Size = new System.Drawing.Size(60, 31);
            this.Square.TabIndex = 42;
            this.Square.Text = "□";
            this.Square.UseVisualStyleBackColor = true;
            // 
            // SolidTriangle
            // 
            this.SolidTriangle.AutoSize = true;
            this.SolidTriangle.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidTriangle.Location = new System.Drawing.Point(415, 452);
            this.SolidTriangle.Margin = new System.Windows.Forms.Padding(4);
            this.SolidTriangle.Name = "SolidTriangle";
            this.SolidTriangle.Size = new System.Drawing.Size(60, 31);
            this.SolidTriangle.TabIndex = 41;
            this.SolidTriangle.Text = "▲";
            this.SolidTriangle.UseVisualStyleBackColor = true;
            // 
            // Triangle
            // 
            this.Triangle.AutoSize = true;
            this.Triangle.Font = new System.Drawing.Font("宋体", 16F);
            this.Triangle.Location = new System.Drawing.Point(415, 373);
            this.Triangle.Margin = new System.Windows.Forms.Padding(4);
            this.Triangle.Name = "Triangle";
            this.Triangle.Size = new System.Drawing.Size(60, 31);
            this.Triangle.TabIndex = 40;
            this.Triangle.Text = "△";
            this.Triangle.UseVisualStyleBackColor = true;
            // 
            // SolidCircle
            // 
            this.SolidCircle.AutoSize = true;
            this.SolidCircle.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidCircle.Location = new System.Drawing.Point(243, 452);
            this.SolidCircle.Margin = new System.Windows.Forms.Padding(4);
            this.SolidCircle.Name = "SolidCircle";
            this.SolidCircle.Size = new System.Drawing.Size(60, 31);
            this.SolidCircle.TabIndex = 39;
            this.SolidCircle.Text = "●";
            this.SolidCircle.UseVisualStyleBackColor = true;
            // 
            // Circle
            // 
            this.Circle.AutoSize = true;
            this.Circle.Font = new System.Drawing.Font("宋体", 16F);
            this.Circle.Location = new System.Drawing.Point(243, 373);
            this.Circle.Margin = new System.Windows.Forms.Padding(4);
            this.Circle.Name = "Circle";
            this.Circle.Size = new System.Drawing.Size(60, 31);
            this.Circle.TabIndex = 38;
            this.Circle.Text = "○";
            this.Circle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 323);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(182, 320);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "选择点要素的形状：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(559, 576);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 48;
            this.label2.Text = "毫米";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(393, 567);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 30);
            this.textBox1.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(182, 571);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 46;
            this.label5.Text = "设置点要素的大小：";
            // 
            // UniqueValuePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 778);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CircleCircle);
            this.Controls.Add(this.CircleDot);
            this.Controls.Add(this.SolidSquare);
            this.Controls.Add(this.Square);
            this.Controls.Add(this.SolidTriangle);
            this.Controls.Add(this.Triangle);
            this.Controls.Add(this.SolidCircle);
            this.Controls.Add(this.Circle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "UniqueValuePoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "点要素唯一值渲染";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton CircleCircle;
        private System.Windows.Forms.RadioButton CircleDot;
        private System.Windows.Forms.RadioButton SolidSquare;
        private System.Windows.Forms.RadioButton Square;
        private System.Windows.Forms.RadioButton SolidTriangle;
        private System.Windows.Forms.RadioButton Triangle;
        private System.Windows.Forms.RadioButton SolidCircle;
        private System.Windows.Forms.RadioButton Circle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}