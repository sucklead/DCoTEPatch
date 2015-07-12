//#undef FX1_1

using System;
using System.Windows.Forms;

namespace DCoTEPatch
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !FX1_1
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#endif
            Application.Run(new Form1());
        }
    }
}