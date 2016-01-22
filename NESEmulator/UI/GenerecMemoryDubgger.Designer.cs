namespace NESEmulator.UI
{
    partial class GenerecMemoryDubgger
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
            this.memoryDebugger1 = new NESEmulator.UI.MemoryDebugger();
            this.SuspendLayout();
            // 
            // memoryDebugger1
            // 
            this.memoryDebugger1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryDebugger1.Location = new System.Drawing.Point(0, 0);
            this.memoryDebugger1.Name = "memoryDebugger1";
            this.memoryDebugger1.Size = new System.Drawing.Size(550, 369);
            this.memoryDebugger1.TabIndex = 0;
            // 
            // GenerecMemoryDubgger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 369);
            this.Controls.Add(this.memoryDebugger1);
            this.Name = "GenerecMemoryDubgger";
            this.Text = "GenerecMemoryDubgger";
            this.ResumeLayout(false);

        }

        #endregion

        private MemoryDebugger memoryDebugger1;

    }
}