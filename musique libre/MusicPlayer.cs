using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace musique_libre
{
    public partial class MusicPlayer : Form
    {
        #region Credit

        /*

     *  Custom rounded edges
     *   ----------------
     *  /   CREATED BY   \
     * /    ILLUMINATI    \
     * --------------------
     * ANDREW JUSTIN SOLESA
     * --------------------
     * GOOMBA SHROOM KASMER
     * --------------------
     * 
     
         */

        #endregion

        #region Variables

        MusicDownloader musicDownloader;
        MusicLibrary musicLibrary;

        public bool padlock = default(bool);

        #endregion

        #region PInvoke Helpers

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private const int CS_DROPSHADOW = 0x20000;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int L, int T, int R, int B, int W, int H);

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr oCallback);

        #endregion

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion

        #region Music Play

        public void PlaySound(string root)
        {
            string command;

            command = "close MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);

            command = "open \"" + root + "\" type mpegvideo alias MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);

            command = "play MediaFile";
            command += " REPEAT";
            mciSendString(command, null, 0, IntPtr.Zero);

            pictureBox3.Image = Properties.Resources.stop;

            padlock = true;
        }

        #endregion

        #region MusicPlayer

        public MusicPlayer()
        {
            InitializeComponent();

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 16, 16));

            contextMenuStrip1.Cursor = Cursors.Hand;

            musicDownloader = new MusicDownloader();
            musicDownloader.Show();
            musicDownloader.Hide();

            musicLibrary = new MusicLibrary(this);
            musicLibrary.Show();
            musicLibrary.Hide();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.Help;

                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (transparentPanel1.Bounds.Contains(e.Location) && !transparentPanel1.Visible)
            {
                transparentPanel1.Show();
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;

            musicDownloader.Location = new Point(this.Location.X - 106, this.Location.Y + 173);

            if (musicDownloader.Visible)
            {
                musicLibrary.Location = new Point(musicDownloader.Location.X, musicDownloader.Location.Y + 54);
            }
            else
            {
                musicLibrary.Location = new Point(this.Location.X - 106, this.Location.Y + 173);
            }
        }

        #endregion

        #region TransparentPanel

        private void transparentPanel1_MouseLeave(object sender, EventArgs e)
        {
            transparentPanel1.Hide();
        }

        #endregion

        #region ContextMenuStrip ToolStripItems

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (downloadToolStripMenuItem.Checked == false)
            {
                musicDownloader.Show();

                musicLibrary.Location = new Point(musicDownloader.Location.X, musicDownloader.Location.Y + 54);

                downloadToolStripMenuItem.Checked = true;
            }
            else
            {
                musicDownloader.Hide();

                musicLibrary.Location = new Point(this.Location.X - 106, this.Location.Y + 173);

                downloadToolStripMenuItem.Checked = false;
            }
        }

        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (libraryToolStripMenuItem.Checked == false)
            {
                musicLibrary.Show();

                libraryToolStripMenuItem.Checked = true;
            }
            else
            {
                musicLibrary.Hide();

                libraryToolStripMenuItem.Checked = false;
            }
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.paypal.me/AndrewSolesa");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://pastebin.com/raw.php?i=87kLi1qY");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (padlock)
            {
                pictureBox3.Image = Properties.Resources.play;

                padlock = false;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.stop;

                padlock = true;
            }
        }
    }
}
