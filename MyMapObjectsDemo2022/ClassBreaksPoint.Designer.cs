
namespace MyMapObjectsDemo2022
{
    partial class ClassBreaksPoint
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
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
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(702, 621);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(306, 621);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(404, 169);
            this.listBox1.TabIndex = 5;
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
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(179, 535);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "选择分级数目：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(379, 535);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 25);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "5";
            // 
            // CircleCircle
            // 
            this.CircleCircle.AutoSize = true;
            this.CircleCircle.Font = new System.Drawing.Font("宋体", 16F);
            this.CircleCircle.Location = new System.Drawing.Point(742, 413);
            this.CircleCircle.Margin = new System.Windows.Forms.Padding(4);
            this.CircleCircle.Name = "CircleCircle";
            this.CircleCircle.Size = new System.Drawing.Size(60, 31);
            this.CircleCircle.TabIndex = 35;
            this.CircleCircle.Text = "◎";
            this.CircleCircle.UseVisualStyleBackColor = true;
            // 
            // CircleDot
            // 
            this.CircleDot.AutoSize = true;
            this.CircleDot.Font = new System.Drawing.Font("宋体", 16F);
            this.CircleDot.Location = new System.Drawing.Point(742, 334);
            this.CircleDot.Margin = new System.Windows.Forms.Padding(4);
            this.CircleDot.Name = "CircleDot";
            this.CircleDot.Size = new System.Drawing.Size(60, 31);
            this.CircleDot.TabIndex = 34;
            this.CircleDot.Text = "☉";
            this.CircleDot.UseVisualStyleBackColor = true;
            // 
            // SolidSquare
            // 
            this.SolidSquare.AutoSize = true;
            this.SolidSquare.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidSquare.Location = new System.Drawing.Point(583, 413);
            this.SolidSquare.Margin = new System.Windows.Forms.Padding(4);
            this.SolidSquare.Name = "SolidSquare";
            this.SolidSquare.Size = new System.Drawing.Size(60, 31);
            this.SolidSquare.TabIndex = 33;
            this.SolidSquare.Text = "■";
            this.SolidSquare.UseVisualStyleBackColor = true;
            // 
            // Square
            // 
            this.Square.AutoSize = true;
            this.Square.Font = new System.Drawing.Font("宋体", 16F);
            this.Square.Location = new System.Drawing.Point(583, 334);
            this.Square.Margin = new System.Windows.Forms.Padding(4);
            this.Square.Name = "Square";
            this.Square.Size = new System.Drawing.Size(60, 31);
            this.Square.TabIndex = 32;
            this.Square.Text = "□";
            this.Square.UseVisualStyleBackColor = true;
            // 
            // SolidTriangle
            // 
            this.SolidTriangle.AutoSize = true;
            this.SolidTriangle.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidTriangle.Location = new System.Drawing.Point(412, 413);
            this.SolidTriangle.Margin = new System.Windows.Forms.Padding(4);
            this.SolidTriangle.Name = "SolidTriangle";
            this.SolidTriangle.Size = new System.Drawing.Size(60, 31);
            this.SolidTriangle.TabIndex = 31;
            this.SolidTriangle.Text = "▲";
            this.SolidTriangle.UseVisualStyleBackColor = true;
            // 
            // Triangle
            // 
            this.Triangle.AutoSize = true;
            this.Triangle.Font = new System.Drawing.Font("宋体", 16F);
            this.Triangle.Location = new System.Drawing.Point(412, 334);
            this.Triangle.Margin = new System.Windows.Forms.Padding(4);
            this.Triangle.Name = "Triangle";
            this.Triangle.Size = new System.Drawing.Size(60, 31);
            this.Triangle.TabIndex = 30;
            this.Triangle.Text = "△";
            this.Triangle.UseVisualStyleBackColor = true;
            // 
            // SolidCircle
            // 
            this.SolidCircle.AutoSize = true;
            this.SolidCircle.Font = new System.Drawing.Font("宋体", 16F);
            this.SolidCircle.Location = new System.Drawing.Point(240, 413);
            this.SolidCircle.Margin = new System.Windows.Forms.Padding(4);
            this.SolidCircle.Name = "SolidCircle";
            this.SolidCircle.Size = new System.Drawing.Size(60, 31);
            this.SolidCircle.TabIndex = 29;
            this.SolidCircle.Text = "●";
            this.SolidCircle.UseVisualStyleBackColor = true;
            // 
            // Circle
            // 
            this.Circle.AutoSize = true;
            this.Circle.Font = new System.Drawing.Font("宋体", 16F);
            this.Circle.Location = new System.Drawing.Point(240, 334);
            this.Circle.Margin = new System.Windows.Forms.Padding(4);
            this.Circle.Name = "Circle";
            this.Circle.Size = new System.Drawing.Size(60, 31);
            this.Circle.TabIndex = 28;
            this.Circle.Text = "○";
            this.Circle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 284);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(179, 281);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "选择点要素的形状：";
            // 
            // ClassBreaksPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 721);
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
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "ClassBreaksPoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "点要素分级渲染";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
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
    }
}