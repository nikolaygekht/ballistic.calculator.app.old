using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReticleEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MathEx.ExternalBallistic.Serialization.Windows.Serialization.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppForm());
        }
    }
}
