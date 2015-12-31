using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace musique_libre
{
    public partial class MusicLibrary : Form
    {
        #region Variable

        private MusicPlayer musicPlayer;

        #endregion

        #region PInvoke Helpers

        private const int CS_DROPSHADOW = 0x20000;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int L, int T, int R, int B, int W, int H);

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

        public void PopulateTree(string dir, TreeNodeCollection nodes)
        {
            DirectoryInfo directory = new DirectoryInfo(dir);
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                TreeNode t = new TreeNode(d.Name);
                nodes.Add(t);
                PopulateTree(d.FullName, t.Nodes);
            }
            foreach (FileInfo f in directory.GetFiles("*.mp3"))
            {
                TreeNode t = new TreeNode(f.Name);
                nodes.Add(t);
            }
        }

        public MusicLibrary(MusicPlayer player)
        {
            InitializeComponent();

            musicPlayer = player as MusicPlayer;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 16, 16));

            PopulateTree(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre").ToString(), treeView1.Nodes);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            PopulateTree(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre").ToString(), treeView1.Nodes);
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode CurrentNode = e.Node;

            string path = CurrentNode.FullPath;

            e.Cancel = true; 
        }
    }
}
