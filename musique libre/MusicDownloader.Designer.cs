namespace musique_libre
{
    partial class MusicDownloader
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.transparentPanel1 = new musique_libre.TransparentPanel();
            this.panelProgressBar1 = new musique_libre.PanelProgressBar();
            this.cueTextBox1 = new musique_libre.CueTextBox();
            this.transparentPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 100);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(353, 199);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.TabStop = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // transparentPanel1
            // 
            this.transparentPanel1.Controls.Add(this.panelProgressBar1);
            this.transparentPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transparentPanel1.Location = new System.Drawing.Point(12, 9);
            this.transparentPanel1.Name = "transparentPanel1";
            this.transparentPanel1.Size = new System.Drawing.Size(353, 28);
            this.transparentPanel1.TabIndex = 4;
            this.transparentPanel1.Click += new System.EventHandler(this.transparentPanel1_Click);
            // 
            // panelProgressBar1
            // 
            this.panelProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.panelProgressBar1.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(252)))), ((int)(((byte)(176)))));
            this.panelProgressBar1.BorderColor = System.Drawing.Color.Transparent;
            this.panelProgressBar1.EmptyColor = System.Drawing.Color.Transparent;
            this.panelProgressBar1.Location = new System.Drawing.Point(3, 3);
            this.panelProgressBar1.Name = "panelProgressBar1";
            this.panelProgressBar1.ProgressMaximumValue = 1;
            this.panelProgressBar1.ShowBorder = false;
            this.panelProgressBar1.Size = new System.Drawing.Size(347, 22);
            this.panelProgressBar1.TabIndex = 0;
            this.panelProgressBar1.TabStop = false;
            this.panelProgressBar1.Value = 0;
            this.panelProgressBar1.Visible = false;
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cueTextBox1.CueText = "...";
            this.cueTextBox1.Font = new System.Drawing.Font("Miramonte", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cueTextBox1.Location = new System.Drawing.Point(12, 9);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(353, 28);
            this.cueTextBox1.TabIndex = 0;
            this.cueTextBox1.TabStop = false;
            this.cueTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MusicDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(377, 47);
            this.ControlBox = false;
            this.Controls.Add(this.transparentPanel1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.cueTextBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusicDownloader";
            this.Opacity = 0.66D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.transparentPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CueTextBox cueTextBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        internal PanelProgressBar panelProgressBar1;
        internal TransparentPanel transparentPanel1;

    }
}