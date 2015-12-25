using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Timers;
using System.Text.RegularExpressions;
using System.Drawing;

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
            internal string title;
            internal string artist;

            internal Image artwork;
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

                internal static bool Song_Download(Form2 _form2, int _option, Song _song, string _url, WebBrowser _browser1)
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
                                delay1.Interval = 60000;
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
                                delay2.Interval = 120000;
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

                                    _song.title = name.Substring(index + 1);
                                    _song.artist = name.Substring(0, index);
                                }
                                else if (count == 1)
                                {
                                    int index = name.IndexOf("-");

                                    _song.title = name.Substring(index + 1);
                                    _song.artist = name.Substring(0, index);
                                }
                                else
                                {
                                    _song.title = name;
                                }

                                _form2.DataTransfer(root, _song.title, _song.artist);

                                string url = default(string);

                                foreach (HtmlElement href in collection)
                                {
                                    MatchCollection murl = Regex.Matches(href.OuterHtml, "\"([^\"]*)\"");

                                    if (murl[0].ToString().Replace("\"", "").Length > 38)
                                    {
                                        url = murl[0].ToString().Replace("\"", "");

                                        _form2.padlock = true;

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
                #region Variable

                static bool padlock = default(bool);

                #endregion

                internal static bool Artwork_Download(Form2 _form2, Song _song, WebBrowser _browser2)
                {
                    bool ret = default(bool);

                    _browser2.Navigate(new Uri("about:blank"));

                    _browser2.Navigate("https://bendodson.com/code/itunes-artwork-finder/");

                    System.Timers.Timer delay1 = new System.Timers.Timer();
                    delay1.Elapsed += new System.Timers.ElapsedEventHandler(LoadComplete);
                    delay1.Interval = 60000;
                    delay1.Enabled = true;

                    while (!padlock)
                    {
                        Application.DoEvents();
                    }

                    return ret;
                }

                internal static void LoadComplete(object source, ElapsedEventArgs e)
                {
                    padlock = true;
                }
            }

            internal static bool InitiateDownloader(Form2 form2, int option, Song song, string url, WebBrowser browser1, WebBrowser browser2)
            {
                bool ret = default(bool);
                ret = Song_Downloader.Song_Download(form2, option, song, url, browser1);

                while (!ret)
                {
                    Application.DoEvents();

                    if (thelock)
                    {
                        return ret;
                    }
                }

                thelock = default(bool);

                ret = default(bool);
                ret = Artwork_Downloader.Artwork_Download(form2, song, browser2);

                ret = true;

                while (!ret)
                {
                    Application.DoEvents();

                    if (thelock)
                    {
                        return ret;
                    }
                }

                thelock = default(bool);

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

        internal static bool Download(Form2 _form2, int _option, string _url, WebBrowser _browser1, WebBrowser _browser2)
        {
            bool ret = default(bool);

            song = new Song();

            ret = Library_Download.InitiateDownloader(_form2, _option, song, _url, _browser1, _browser2);

            while (!ret)
            {
                Application.DoEvents();

                if (thelock)
                {
                    _form2.DataTransfer(thelock);

                    return ret;
                }
            }

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