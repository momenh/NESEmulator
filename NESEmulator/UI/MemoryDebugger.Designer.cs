namespace NESEmulator.UI
{
    partial class MemoryDebugger
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbViewMode = new System.Windows.Forms.ComboBox();
            this.lblViewMode = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lblStartIndex = new System.Windows.Forms.Label();
            this.txtStartIndex = new System.Windows.Forms.TextBox();
            this.txtEndIndex = new System.Windows.Forms.TextBox();
            this.lblEndIndex = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblMemoryName = new System.Windows.Forms.Label();
            this.dgvMemoryGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemoryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbViewMode
            // 
            this.cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewMode.FormattingEnabled = true;
            this.cmbViewMode.Items.AddRange(new object[] {
            "HEX - 16",
            "DEC - 10"});
            this.cmbViewMode.Location = new System.Drawing.Point(77, 6);
            this.cmbViewMode.Name = "cmbViewMode";
            this.cmbViewMode.Size = new System.Drawing.Size(75, 21);
            this.cmbViewMode.TabIndex = 1;
            this.cmbViewMode.SelectedIndexChanged += new System.EventHandler(this.cmbViewMode_SelectedIndexChanged);
            // 
            // lblViewMode
            // 
            this.lblViewMode.AutoSize = true;
            this.lblViewMode.Location = new System.Drawing.Point(13, 9);
            this.lblViewMode.Name = "lblViewMode";
            this.lblViewMode.Size = new System.Drawing.Size(58, 13);
            this.lblViewMode.TabIndex = 0;
            this.lblViewMode.Text = "View Mode";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(323, 382);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 0;
            this.lineShape1.X2 = 554;
            this.lineShape1.Y1 = 70;
            this.lineShape1.Y2 = 70;
            // 
            // lblStartIndex
            // 
            this.lblStartIndex.AutoSize = true;
            this.lblStartIndex.Location = new System.Drawing.Point(12, 36);
            this.lblStartIndex.Name = "lblStartIndex";
            this.lblStartIndex.Size = new System.Drawing.Size(34, 13);
            this.lblStartIndex.TabIndex = 3;
            this.lblStartIndex.Text = "S-INX";
            // 
            // txtStartIndex
            // 
            this.txtStartIndex.Location = new System.Drawing.Point(52, 33);
            this.txtStartIndex.Name = "txtStartIndex";
            this.txtStartIndex.Size = new System.Drawing.Size(60, 20);
            this.txtStartIndex.TabIndex = 4;
            // 
            // txtEndIndex
            // 
            this.txtEndIndex.Location = new System.Drawing.Point(158, 33);
            this.txtEndIndex.Name = "txtEndIndex";
            this.txtEndIndex.Size = new System.Drawing.Size(60, 20);
            this.txtEndIndex.TabIndex = 6;
            // 
            // lblEndIndex
            // 
            this.lblEndIndex.AutoSize = true;
            this.lblEndIndex.Location = new System.Drawing.Point(117, 36);
            this.lblEndIndex.Name = "lblEndIndex";
            this.lblEndIndex.Size = new System.Drawing.Size(35, 13);
            this.lblEndIndex.TabIndex = 5;
            this.lblEndIndex.Text = "E-IND";
            // 
            // txtLength
            // 
            this.txtLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLength.Location = new System.Drawing.Point(254, 33);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(60, 20);
            this.txtLength.TabIndex = 8;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(224, 36);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(24, 13);
            this.lblLength.TabIndex = 7;
            this.lblLength.Text = "Len";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(224, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 20);
            this.textBox1.TabIndex = 10;
            // 
            // lblMemoryName
            // 
            this.lblMemoryName.AutoSize = true;
            this.lblMemoryName.Location = new System.Drawing.Point(164, 9);
            this.lblMemoryName.Name = "lblMemoryName";
            this.lblMemoryName.Size = new System.Drawing.Size(54, 13);
            this.lblMemoryName.TabIndex = 9;
            this.lblMemoryName.Text = "Mem-Nam";
            // 
            // dgvMemoryGrid
            // 
            this.dgvMemoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMemoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemoryGrid.Location = new System.Drawing.Point(0, 87);
            this.dgvMemoryGrid.Name = "dgvMemoryGrid";
            this.dgvMemoryGrid.Size = new System.Drawing.Size(323, 292);
            this.dgvMemoryGrid.TabIndex = 11;
            // 
            // MemoryDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvMemoryGrid);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblMemoryName);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.txtEndIndex);
            this.Controls.Add(this.lblEndIndex);
            this.Controls.Add(this.txtStartIndex);
            this.Controls.Add(this.lblStartIndex);
            this.Controls.Add(this.cmbViewMode);
            this.Controls.Add(this.lblViewMode);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "MemoryDebugger";
            this.Size = new System.Drawing.Size(323, 382);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemoryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblViewMode;
        private System.Windows.Forms.ComboBox cmbViewMode;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label lblStartIndex;
        private System.Windows.Forms.TextBox txtStartIndex;
        private System.Windows.Forms.TextBox txtEndIndex;
        private System.Windows.Forms.Label lblEndIndex;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblMemoryName;
        private System.Windows.Forms.DataGridView dgvMemoryGrid;
    }
}
