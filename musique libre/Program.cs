using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace musique_libre
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ret = default(bool);

            ret = Bibliothèque_Musicale.Verification();

            if (ret)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MusicPlayer());
            }
        }
    }
}
