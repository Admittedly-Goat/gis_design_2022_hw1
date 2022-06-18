
namespace MyMapObjectsDemo2022
{
    partial class RenderLayer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.pageSetupDialog2 = new System.Windows.Forms.PageSetupDialog();
            this.pageSetupDialog3 = new System.Windows.Forms.PageSetupDialog();
            this.tabControlRenderType = new System.Windows.Forms.TabControl();
            this.tabPageSimple = new System.Windows.Forms.TabPage();
            this.buttonSimpleConfirm = new System.Windows.Forms.Button();
            this.buttonSimpleShowSymbol = new System.Windows.Forms.Button();
            this.tabPageUnique = new System.Windows.Forms.TabPage();
            this.buttonUniqueValueConfirm = new System.Windows.Forms.Button();
            this.dataGridViewUniqueValue = new System.Windows.Forms.DataGridView();
            this.ColumnSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUniqueValueLoadAll = new System.Windows.Forms.Button();
            this.labelUniqueValue_选择字段 = new System.Windows.Forms.Label();
            this.comboBoxUniqueValueSelectField = new System.Windows.Forms.ComboBox();
            this.tabPageClassBreak = new System.Windows.Forms.TabPage();
            this.buttonClassBreakConfirm = new System.Windows.Forms.Button();
            this.buttonClassBreakLoadAll = new System.Windows.Forms.Button();
            this.dataGridViewClassBreak = new System.Windows.Forms.DataGridView();
            this.ColumnClassBreakSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClassBreakValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClassBreakValueCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelClassNumber = new System.Windows.Forms.Label();
            this.numericUpDownClassNumber = new System.Windows.Forms.NumericUpDown();
            this.labelClassBreak_选择字段 = new System.Windows.Forms.Label();
            this.comboBoxClassBreakSelectField = new System.Windows.Forms.ComboBox();
            this.tabControlRenderType.SuspendLayout();
            this.tabPageSimple.SuspendLayout();
            this.tabPageUnique.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUniqueValue)).BeginInit();
            this.tabPageClassBreak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClassBreak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClassNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControlRenderType
            // 
            this.tabControlRenderType.Controls.Add(this.tabPageSimple);
            this.tabControlRenderType.Controls.Add(this.tabPageUnique);
            this.tabControlRenderType.Controls.Add(this.tabPageClassBreak);
            this.tabControlRenderType.Location = new System.Drawing.Point(12, 12);
            this.tabControlRenderType.Name = "tabControlRenderType";
            this.tabControlRenderType.SelectedIndex = 0;
            this.tabControlRenderType.Size = new System.Drawing.Size(507, 383);
            this.tabControlRenderType.TabIndex = 1;
            // 
            // tabPageSimple
            // 
            this.tabPageSimple.Controls.Add(this.buttonSimpleConfirm);
            this.tabPageSimple.Controls.Add(this.buttonSimpleShowSymbol);
            this.tabPageSimple.Location = new System.Drawing.Point(4, 22);
            this.tabPageSimple.Name = "tabPageSimple";
            this.tabPageSimple.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSimple.Size = new System.Drawing.Size(499, 357);
            this.tabPageSimple.TabIndex = 0;
            this.tabPageSimple.Text = "单一符号";
            this.tabPageSimple.UseVisualStyleBackColor = true;
            // 
            // buttonSimpleConfirm
            // 
            this.buttonSimpleConfirm.Location = new System.Drawing.Point(206, 182);
            this.buttonSimpleConfirm.Name = "buttonSimpleConfirm";
            this.buttonSimpleConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonSimpleConfirm.TabIndex = 1;
            this.buttonSimpleConfirm.Text = "确定";
            this.buttonSimpleConfirm.UseVisualStyleBackColor = true;
            this.buttonSimpleConfirm.Click += new System.EventHandler(this.buttonSimpleConfirm_Click);
            // 
            // buttonSimpleShowSymbol
            // 
            this.buttonSimpleShowSymbol.Location = new System.Drawing.Point(163, 90);
            this.buttonSimpleShowSymbol.Name = "buttonSimpleShowSymbol";
            this.buttonSimpleShowSymbol.Size = new System.Drawing.Size(158, 50);
            this.buttonSimpleShowSymbol.TabIndex = 0;
            this.buttonSimpleShowSymbol.UseVisualStyleBackColor = true;
            this.buttonSimpleShowSymbol.Click += new System.EventHandler(this.buttonSimpleShowSymbol_Click);
            // 
            // tabPageUnique
            // 
            this.tabPageUnique.Controls.Add(this.buttonUniqueValueConfirm);
            this.tabPageUnique.Controls.Add(this.dataGridViewUniqueValue);
            this.tabPageUnique.Controls.Add(this.buttonUniqueValueLoadAll);
            this.tabPageUnique.Controls.Add(this.labelUniqueValue_选择字段);
            this.tabPageUnique.Controls.Add(this.comboBoxUniqueValueSelectField);
            this.tabPageUnique.Location = new System.Drawing.Point(4, 22);
            this.tabPageUnique.Name = "tabPageUnique";
            this.tabPageUnique.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUnique.Size = new System.Drawing.Size(499, 357);
            this.tabPageUnique.TabIndex = 1;
            this.tabPageUnique.Text = "唯一值";
            this.tabPageUnique.UseVisualStyleBackColor = true;
            // 
            // buttonUniqueValueConfirm
            // 
            this.buttonUniqueValueConfirm.Location = new System.Drawing.Point(343, 286);
            this.buttonUniqueValueConfirm.Name = "buttonUniqueValueConfirm";
            this.buttonUniqueValueConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonUniqueValueConfirm.TabIndex = 5;
            this.buttonUniqueValueConfirm.Text = "确定";
            this.buttonUniqueValueConfirm.UseVisualStyleBackColor = true;
            this.buttonUniqueValueConfirm.Click += new System.EventHandler(this.buttonUniqueValueConfirm_Click);
            // 
            // dataGridViewUniqueValue
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUniqueValue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUniqueValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUniqueValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSymbol,
            this.ColumnValue,
            this.ColumnCount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUniqueValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewUniqueValue.EnableHeadersVisualStyles = false;
            this.dataGridViewUniqueValue.Location = new System.Drawing.Point(51, 97);
            this.dataGridViewUniqueValue.Name = "dataGridViewUniqueValue";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUniqueValue.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewUniqueValue.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewUniqueValue.RowTemplate.Height = 23;
            this.dataGridViewUniqueValue.Size = new System.Drawing.Size(367, 150);
            this.dataGridViewUniqueValue.TabIndex = 4;
            this.dataGridViewUniqueValue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUniqueValue_CellContentClick);
            // 
            // ColumnSymbol
            // 
            this.ColumnSymbol.HeaderText = "符号";
            this.ColumnSymbol.Name = "ColumnSymbol";
            this.ColumnSymbol.ReadOnly = true;
            this.ColumnSymbol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnSymbol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnValue
            // 
            this.ColumnValue.HeaderText = "值";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.ReadOnly = true;
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "数量";
            this.ColumnCount.Name = "ColumnCount";
            // 
            // buttonUniqueValueLoadAll
            // 
            this.buttonUniqueValueLoadAll.Location = new System.Drawing.Point(51, 286);
            this.buttonUniqueValueLoadAll.Name = "buttonUniqueValueLoadAll";
            this.buttonUniqueValueLoadAll.Size = new System.Drawing.Size(75, 23);
            this.buttonUniqueValueLoadAll.TabIndex = 3;
            this.buttonUniqueValueLoadAll.Text = "加载所有值";
            this.buttonUniqueValueLoadAll.UseVisualStyleBackColor = true;
            this.buttonUniqueValueLoadAll.Click += new System.EventHandler(this.buttonUniqueValueLoadAll_Click);
            // 
            // labelUniqueValue_选择字段
            // 
            this.labelUniqueValue_选择字段.AutoSize = true;
            this.labelUniqueValue_选择字段.Location = new System.Drawing.Point(17, 38);
            this.labelUniqueValue_选择字段.Name = "labelUniqueValue_选择字段";
            this.labelUniqueValue_选择字段.Size = new System.Drawing.Size(53, 12);
            this.labelUniqueValue_选择字段.TabIndex = 1;
            this.labelUniqueValue_选择字段.Text = "选择字段";
            // 
            // comboBoxUniqueValueSelectField
            // 
            this.comboBoxUniqueValueSelectField.FormattingEnabled = true;
            this.comboBoxUniqueValueSelectField.Location = new System.Drawing.Point(76, 35);
            this.comboBoxUniqueValueSelectField.Name = "comboBoxUniqueValueSelectField";
            this.comboBoxUniqueValueSelectField.Size = new System.Drawing.Size(121, 20);
            this.comboBoxUniqueValueSelectField.TabIndex = 0;
            // 
            // tabPageClassBreak
            // 
            this.tabPageClassBreak.Controls.Add(this.buttonClassBreakConfirm);
            this.tabPageClassBreak.Controls.Add(this.buttonClassBreakLoadAll);
            this.tabPageClassBreak.Controls.Add(this.dataGridViewClassBreak);
            this.tabPageClassBreak.Controls.Add(this.labelClassNumber);
            this.tabPageClassBreak.Controls.Add(this.numericUpDownClassNumber);
            this.tabPageClassBreak.Controls.Add(this.labelClassBreak_选择字段);
            this.tabPageClassBreak.Controls.Add(this.comboBoxClassBreakSelectField);
            this.tabPageClassBreak.Location = new System.Drawing.Point(4, 22);
            this.tabPageClassBreak.Name = "tabPageClassBreak";
            this.tabPageClassBreak.Size = new System.Drawing.Size(499, 357);
            this.tabPageClassBreak.TabIndex = 2;
            this.tabPageClassBreak.Text = "分级";
            this.tabPageClassBreak.UseVisualStyleBackColor = true;
            // 
            // buttonClassBreakConfirm
            // 
            this.buttonClassBreakConfirm.Location = new System.Drawing.Point(376, 286);
            this.buttonClassBreakConfirm.Name = "buttonClassBreakConfirm";
            this.buttonClassBreakConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonClassBreakConfirm.TabIndex = 6;
            this.buttonClassBreakConfirm.Text = "确定";
            this.buttonClassBreakConfirm.UseVisualStyleBackColor = true;
            this.buttonClassBreakConfirm.Click += new System.EventHandler(this.buttonClassBreakConfirm_Click);
            // 
            // buttonClassBreakLoadAll
            // 
            this.buttonClassBreakLoadAll.Location = new System.Drawing.Point(60, 286);
            this.buttonClassBreakLoadAll.Name = "buttonClassBreakLoadAll";
            this.buttonClassBreakLoadAll.Size = new System.Drawing.Size(75, 23);
            this.buttonClassBreakLoadAll.TabIndex = 5;
            this.buttonClassBreakLoadAll.Text = "添加所有值";
            this.buttonClassBreakLoadAll.UseVisualStyleBackColor = true;
            this.buttonClassBreakLoadAll.Click += new System.EventHandler(this.buttonClassBreakLoadAll_Click);
            // 
            // dataGridViewClassBreak
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClassBreak.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewClassBreak.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClassBreak.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnClassBreakSymbol,
            this.ColumnClassBreakValue,
            this.ColumnClassBreakValueCount});
            this.dataGridViewClassBreak.EnableHeadersVisualStyles = false;
            this.dataGridViewClassBreak.Location = new System.Drawing.Point(60, 105);
            this.dataGridViewClassBreak.Name = "dataGridViewClassBreak";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClassBreak.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewClassBreak.RowTemplate.Height = 23;
            this.dataGridViewClassBreak.Size = new System.Drawing.Size(391, 150);
            this.dataGridViewClassBreak.TabIndex = 4;
            this.dataGridViewClassBreak.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClassBreak_CellContentClick);
            // 
            // ColumnClassBreakSymbol
            // 
            this.ColumnClassBreakSymbol.HeaderText = "符号";
            this.ColumnClassBreakSymbol.Name = "ColumnClassBreakSymbol";
            // 
            // ColumnClassBreakValue
            // 
            this.ColumnClassBreakValue.HeaderText = "值";
            this.ColumnClassBreakValue.Name = "ColumnClassBreakValue";
            // 
            // ColumnClassBreakValueCount
            // 
            this.ColumnClassBreakValueCount.HeaderText = "数量";
            this.ColumnClassBreakValueCount.Name = "ColumnClassBreakValueCount";
            // 
            // labelClassNumber
            // 
            this.labelClassNumber.AutoSize = true;
            this.labelClassNumber.Location = new System.Drawing.Point(251, 37);
            this.labelClassNumber.Name = "labelClassNumber";
            this.labelClassNumber.Size = new System.Drawing.Size(41, 12);
            this.labelClassNumber.TabIndex = 3;
            this.labelClassNumber.Text = "分级数";
            // 
            // numericUpDownClassNumber
            // 
            this.numericUpDownClassNumber.Location = new System.Drawing.Point(319, 33);
            this.numericUpDownClassNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownClassNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownClassNumber.Name = "numericUpDownClassNumber";
            this.numericUpDownClassNumber.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownClassNumber.TabIndex = 2;
            this.numericUpDownClassNumber.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // labelClassBreak_选择字段
            // 
            this.labelClassBreak_选择字段.AutoSize = true;
            this.labelClassBreak_选择字段.Location = new System.Drawing.Point(19, 37);
            this.labelClassBreak_选择字段.Name = "labelClassBreak_选择字段";
            this.labelClassBreak_选择字段.Size = new System.Drawing.Size(53, 12);
            this.labelClassBreak_选择字段.TabIndex = 1;
            this.labelClassBreak_选择字段.Text = "选择字段";
            // 
            // comboBoxClassBreakSelectField
            // 
            this.comboBoxClassBreakSelectField.FormattingEnabled = true;
            this.comboBoxClassBreakSelectField.Location = new System.Drawing.Point(78, 34);
            this.comboBoxClassBreakSelectField.Name = "comboBoxClassBreakSelectField";
            this.comboBoxClassBreakSelectField.Size = new System.Drawing.Size(121, 20);
            this.comboBoxClassBreakSelectField.TabIndex = 0;
            // 
            // RenderLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 406);
            this.Controls.Add(this.tabControlRenderType);
            this.Name = "RenderLayer";
            this.Text = "图层符号设置";
            this.tabControlRenderType.ResumeLayout(false);
            this.tabPageSimple.ResumeLayout(false);
            this.tabPageUnique.ResumeLayout(false);
            this.tabPageUnique.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUniqueValue)).EndInit();
            this.tabPageClassBreak.ResumeLayout(false);
            this.tabPageClassBreak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClassBreak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClassNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog2;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog3;
        private System.Windows.Forms.TabControl tabControlRenderType;
        private System.Windows.Forms.TabPage tabPageSimple;
        private System.Windows.Forms.TabPage tabPageUnique;
        private System.Windows.Forms.TabPage tabPageClassBreak;
        private System.Windows.Forms.Button buttonSimpleShowSymbol;
        private System.Windows.Forms.Button buttonSimpleConfirm;
        private System.Windows.Forms.Label labelUniqueValue_选择字段;
        private System.Windows.Forms.ComboBox comboBoxUniqueValueSelectField;
        private System.Windows.Forms.Button buttonUniqueValueLoadAll;
        private System.Windows.Forms.DataGridView dataGridViewUniqueValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
        private System.Windows.Forms.Button buttonUniqueValueConfirm;
        private System.Windows.Forms.DataGridView dataGridViewClassBreak;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClassBreakSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClassBreakValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClassBreakValueCount;
        private System.Windows.Forms.Label labelClassNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownClassNumber;
        private System.Windows.Forms.Label labelClassBreak_选择字段;
        private System.Windows.Forms.ComboBox comboBoxClassBreakSelectField;
        private System.Windows.Forms.Button buttonClassBreakLoadAll;
        private System.Windows.Forms.Button buttonClassBreakConfirm;
    }
}