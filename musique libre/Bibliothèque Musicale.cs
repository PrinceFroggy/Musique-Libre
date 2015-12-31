using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Timers;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net;

namespace musique_libre
{
    #region Credit

    /*

     *      DOWNLOADER
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

    class Bibliothèque_Musicale
    {
        #region Variables

        static string root = default(string);

        internal struct Song
        {
            internal string artist;
            internal string title;

            internal string artwork;
        }

        static Song song;

        static bool thelock = default(bool);

        #endregion

        internal class Library_Verification
        {
            internal static class Library_Verifier
            {
                internal static bool Library_Verify()
                {
                    bool ret = default(bool);

                    try
                    {
                        root = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre").ToString() + "\\";

                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "musique libre"));

                        ret = true;
                    }
                    catch (Exception)
                    {
                        root = null;

                        ret = false;

                        throw;
                    }

                    return ret;
                }
            }

            internal static bool InitiateVerifier()
            {
                bool ret = default(bool);

                ret = Library_Verifier.Library_Verify();

                return ret;
            }
        }

        internal class Library_Download
        {
            internal static class Song_Downloader
            {
                #region Variable

                static bool padlock = default(bool);

                #endregion

                internal static bool Song_Download(MusicDownloader _downloader, int _option, string _url, WebBrowser _browser1)
                {
                    bool ret = default(bool);
                    
                    switch (_option)
                    {
                        case 0:
                            #region Youtube Downloader
                            try
                            {
                                _browser1.Navigate(new Uri("about:blank"));
                                _browser1.Navigate("http://www.youtube-mp3.org/");

                                System.Timers.Timer delay1 = new System.Timers.Timer();
                                delay1.Elapsed += new System.Timers.ElapsedEventHandler(LoadComplete);
                                delay1.Interval = 10000;
                                delay1.Enabled = true;

                                while (!padlock)
                                {
                                    Application.DoEvents();
                                }

                                delay1.Enabled = false;

                                padlock = default(bool);

                                _browser1.Document.GetElementById("youtube-url").SetAttribute("value", _url);
                                _browser1.Document.GetElementById("submit").InvokeMember("onclick");

                                System.Timers.Timer delay2 = new System.Timers.Timer();
                                delay2.Elapsed += new System.Timers.ElapsedEventHandler(LoadComplete);
                                delay2.Interval = 90000;
                                delay2.Enabled = true;

                                while (!padlock)
                                {
                                    Application.DoEvents();
                                }

                                delay2.Enabled = false;

                                padlock = default(bool);

                                HtmlDocument hyperlink = _browser1.Document;
                                HtmlElementCollection collection = hyperlink.GetElementById("dl_link").GetElementsByTagName("a");

                                string name = default(string);

                                try
                                {
                                    name = _browser1.Document.GetElementById("title").InnerText.Substring(7);
                                }
                                catch (Exception)
                                {
                                    name = _browser1.Document.GetElementById("title").InnerText;
                                }

                                int count = Regex.Matches(name, "-").Count;

                                if (count == 2)
                                {
                                    int index = name.LastIndexOf("-");

                                    song.artist = name.Substring(0, index);
                                    song.title = name.Substring(index + 1);
                                }
                                else if (count == 1)
                                {
                                    int index = name.IndexOf("-");

                                    song.artist = name.Substring(0, index);
                                    song.title = name.Substring(index + 1);
                                }
                                else
                                {
                                    song.title = name;
                                }

                                _downloader.DataTransfer(root, song.artist, song.title);

                                string url = default(string);

                                foreach (HtmlElement href in collection)
                                {
                                    MatchCollection murl = Regex.Matches(href.OuterHtml, "\"([^\"]*)\"");

                                    if (murl[0].ToString().Replace("\"", "").Length > 38)
                                    {
                                        url = murl[0].ToString().Replace("\"", "");

                                        _downloader.padlock = true;

                                        href.InvokeMember("click");

                                        break;
                                    }
                                }

                                ret = true;

                                thelock = false;
                            }
                            catch
                            {
                                ret = false;

                                thelock = true;
                            }
                            #endregion
                            break;
                        case 1:
                            #region Soundcloud Downloader
                            #endregion
                            break;
                        case 2:
                            #region Bandcamp Downloader
                            #endregion
                            break;
                    }

                    return ret;
                }

                internal static void LoadComplete(object source, ElapsedEventArgs e)
                {
                    padlock = true;
                }
            }

            internal static class Artwork_Downloader
            {
                internal static void Artwork_Download(MusicDownloader _downloader)
                {
                    try
                    {
                        WebClient response = new WebClient();

                        string source = default(string);

                        if (!string.IsNullOrEmpty(song.artist))
                        {
                            source = response.DownloadString("https://itunesartwork.dodoapps.io/?query=" + song.title + "-" + song.artist + "&entity=album&country=us");
                        }
                        else
                        {
                            source = response.DownloadString("https://itunesartwork.dodoapps.io/?query=" + song.title + "&entity=album&country=us");
                        }

                        if (!string.IsNullOrEmpty(source))
                        {
                            song.artwork = source.Split(',')[0];
                            song.artwork = song.artwork.Replace("\"", "");
                            song.artwork = song.artwork.Replace("[{url:", "");
                            song.artwork = song.artwork.Replace(@"\", "");

                            _downloader.DataTransfer(root, song.artist, song.title, song.artwork);
                        }
                    }
                    catch
                    {

                    }
                }
            }

            internal static bool InitiateDownloader(MusicDownloader downloader, int option, string url, WebBrowser browser1)
            {
                bool ret = default(bool);
                ret = Song_Downloader.Song_Download(downloader, option, url, browser1);

                while (!ret)
                {
                    Application.DoEvents();

                    if (thelock)
                    {
                        return ret;
                    }
                }
                
                thelock = default(bool);
                
                Artwork_Downloader.Artwork_Download(downloader);

                return ret;
            }
        }

        internal class Library_Tag
        {
            internal static class Tag_Editor
            {
                internal static bool Tag_Edit()
                {
                    bool ret = default(bool);

                    return ret;
                }
            }

            internal static bool InitiateEditor()
            {
                bool ret = default(bool);

                return ret;
            }
        }

        internal static bool Verification()
        {
            bool ret = default(bool);
            ret = Library_Verification.InitiateVerifier();

            while (!ret)
            {
                Application.DoEvents();
            }

            return ret;
        }

        internal static bool Download(MusicDownloader _downloader, int _option, string _url, WebBrowser _browser1)
        {
            song = new Song();

            thelock = default(bool);

            bool ret = default(bool);
            ret = Library_Download.InitiateDownloader(_downloader, _option, _url, _browser1);

            while (!ret)
            {
                Application.DoEvents();

                if (thelock)
                {
                    _downloader.DataTransfer(thelock);

                    break;
                }
            }

            thelock = default(bool);

            return ret;
        }

        internal static bool Tag()
        {
            bool ret = default(bool);
            ret = true;

            while (!ret)
            {
                Application.DoEvents();
            }

            return ret;
        }
    }
}