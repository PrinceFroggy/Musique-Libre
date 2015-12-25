namespace musique_libre
{
    partial class PanelProgressBar
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
            this.ProgressFull = new System.Windows.Forms.Panel();
            this.GlossPanelLeft = new System.Windows.Forms.Panel();
            this.ProgressEmpty = new System.Windows.Forms.Panel();
            this.GlossPanelRight = new System.Windows.Forms.Panel();
            this.ProgressFull.SuspendLayout();
            this.ProgressEmpty.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressFull
            // 
            this.ProgressFull.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(252)))), ((int)(((byte)(176)))));
            this.ProgressFull.Controls.Add(this.GlossPanelLeft);
            this.ProgressFull.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProgressFull.Location = new System.Drawing.Point(0, 0);
            this.ProgressFull.Name = "ProgressFull";
            this.ProgressFull.Size = new System.Drawing.Size(36, 28);
            this.ProgressFull.TabIndex = 0;
            // 
            // GlossPanelLeft
            // 
            this.GlossPanelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(252)))), ((int)(((byte)(176)))));
            this.GlossPanelLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.GlossPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.GlossPanelLeft.Name = "GlossPanelLeft";
            this.GlossPanelLeft.Size = new System.Drawing.Size(36, 16);
            this.GlossPanelLeft.TabIndex = 0;
            // 
            // ProgressEmpty
            // 
            this.ProgressEmpty.Controls.Add(this.GlossPanelRight);
            this.ProgressEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressEmpty.Location = new System.Drawing.Point(36, 0);
            this.ProgressEmpty.Name = "ProgressEmpty";
            this.ProgressEmpty.Size = new System.Drawing.Size(317, 28);
            this.ProgressEmpty.TabIndex = 1;
            // 
            // GlossPanelRight
            // 
            this.GlossPanelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(252)))), ((int)(((byte)(176)))));
            this.GlossPanelRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.GlossPanelRight.Location = new System.Drawing.Point(0, 0);
            this.GlossPanelRight.Name = "GlossPanelRight";
            this.GlossPanelRight.Size = new System.Drawing.Size(317, 16);
            this.GlossPanelRight.TabIndex = 0;
            // 
            // PanelProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProgressEmpty);
            this.Controls.Add(this.ProgressFull);
            this.DoubleBuffered = true;
            this.Name = "PanelProgressBar";
            this.Size = new System.Drawing.Size(353, 28);
            this.ProgressFull.ResumeLayout(false);
            this.ProgressEmpty.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ProgressFull;
        private System.Windows.Forms.Panel GlossPanelLeft;
        private System.Windows.Forms.Panel ProgressEmpty;
        private System.Windows.Forms.Panel GlossPanelRight;
    }
}
