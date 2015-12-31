using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;

namespace musique_libre
{
    public partial class MusicDownloader : Form
    {
        #region Variable

        public bool padlock = default(bool);
        public bool thelock = default(bool);

        public string url = default(string);

        public string root = default(string);
        public string artist = default(string);
        public string title = default(string);
        public string artwork = default(string);

        WebClient downloader = default(WebClient);

        #endregion

        #region PInvoke Helpers

        private const int CS_DROPSHADOW = 0x20000;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int L, int T, int R, int B, int W, int H);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        [DllImport("wininet.dll")]
        private static extern long DeleteUrlCacheEntry(string lpszUrlName);

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

        #region DataTransfer

        public void DataTransfer(bool _thelock)
        {
            thelock = _thelock;
        }

        public void DataTransfer(string _root, string _artist, string _title)
        {
            root = _root;
            artist = _artist;
            title = _title;
        }

        public void DataTransfer(string _root, string _artist, string _title, string _artwork)
        {
            root = _root;
            artist = _artist;
            title = _title;
            artwork = _artwork;
        }

        #endregion

        #region ArtworkDownloader

        public void ArtworkDownloader()
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(artwork, root + artist + "\\" + title + ".jpg");
                client.Dispose();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region ClearCache

        void ClearCacheTempFile()
        {
            string args = "";
            args = ("InetCpl.cpl,ClearMyTracksByProcess 8");

            System.Diagnostics.Process process = null;
            System.Diagnostics.ProcessStartInfo processStartInfo;

            processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\Rundll32.exe";
           
            if ((System.Environment.OSVersion.Version.Major >= 6))
            {
                processStartInfo.Verb = "runas";
            }

            processStartInfo.Arguments = args;
            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = true;
            
            try
            {
                process = System.Diagnostics.Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (!(process == null))
                {
                    process.Dispose();
                }
            }
        }

        #endregion

        #region MusicDownloader

        public MusicDownloader()
        {
            InitializeComponent();

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 16, 16));

            webBrowser1.Navigate(new Uri("about:blank"));
        }

        #endregion

        #region TransparentPanel

        private void transparentPanel1_Click(object sender, EventArgs e)
        {
            padlock = default(bool);

            string url = Clipboard.GetText();

            if (url.StartsWith("http") || url.StartsWith("https"))
            {
                int option = default(int);

                if (url.Contains("youtube"))
                {
                    option = 0;
                }
                else if (url.Contains("soundcloud"))
                {
                    option = 1;
                }
                else if (url.Contains("bandcamp"))
                {
                    option = 2;
                }

                cueTextBox1.CueText.Remove(0);

                transparentPanel1.Cursor = Cursors.No;

                panelProgressBar1.Visible = true;

                bool ret = default(bool);

                ret = Bibliothèque_Musicale.Download(this, option, url, webBrowser1);

                while (!ret)
                {
                    Application.DoEvents();

                    if (thelock)
                    {
                        break;
                    }
                }

                if (!ret && thelock)
                {
                    MessageBox.Show("Error downloading!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Clipboard.Clear();

                    cueTextBox1.CueText = "...";

                    panelProgressBar1.Visible = false;
                    panelProgressBar1.Value = 0;
                    panelProgressBar1.ProgressMaximumValue = 1;

                    transparentPanel1.Cursor = Cursors.Hand;
                    transparentPanel1.Refresh();

                    this.Refresh();

                    InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);

                    ClearCacheTempFile();

                    System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");

                    DeleteUrlCacheEntry(url);
                }

                ret = default(bool);
                thelock = default(bool);
            }
        }

        #endregion

        #region WebBrowsers

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Default;

                if (padlock)
                {
                    e.Cancel = true;

                    url = e.Url.ToString();

                    if (artist != null)
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre\\" + artist));

                        root = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre").ToString() + "\\" + artist + "\\";
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre\\" + "Various Artist"));

                        root = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre").ToString() + "\\" + System.DateTime.Now.ToString("dd-MM-yyyy (HH-mm-ss)") + "\\";
                    }

                    downloader = new WebClient();
                    downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Complete);
                    downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Progress);
                    downloader.DownloadFileAsync(new Uri(url), root + title + ".mp3");
                }
            }
            catch (WebException)
            {

            }
        }

        public void Progress(object sender, DownloadProgressChangedEventArgs e)
        {
            panelProgressBar1.Value = e.ProgressPercentage;
        }

        public void Complete(object sender, AsyncCompletedEventArgs e)
        {
            if (padlock)
            {
                Clipboard.Clear();

                cueTextBox1.CueText = "...";

                panelProgressBar1.Visible = false;
                panelProgressBar1.Value = 0;
                panelProgressBar1.ProgressMaximumValue = 1;

                transparentPanel1.Cursor = Cursors.Hand;
                transparentPanel1.Refresh();

                this.Refresh();

                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);

                ClearCacheTempFile();

                System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");

                DeleteUrlCacheEntry(url);

                ArtworkDownloader();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void webBrowser2_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        #endregion
    }
}
