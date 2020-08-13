using System;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Gehtsoft.BallisticCalculator.UI;

namespace Gehtsoft.BallisticCalculator
{
    static class Program
    {
        static string mAppPath;

        internal static string GetSubDir(string name)
        {
            return Path.Combine(mAppPath, name);
        }

        static AppForm mAppForm;

        internal static AppForm MainForm
        {
            get
            {
                return mAppForm;
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MathEx.ExternalBallistic.Serialization.Windows.Serialization.Init();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileInfo fi = new FileInfo(executingAssembly.Location);
            mAppPath = fi.DirectoryName;

            if (!Directory.Exists(GetSubDir("ammo")))
                Directory.CreateDirectory(GetSubDir("ammo"));
            if (!Directory.Exists(GetSubDir("reticles")))
                Directory.CreateDirectory(GetSubDir("reticles"));
            if (!Directory.Exists(GetSubDir("traces")))
                Directory.CreateDirectory(GetSubDir("traces"));

            BulletInfoInput.AmmoDir = GetSubDir("ammo");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mAppForm = new AppForm();
            Application.Run(mAppForm);
        }
    }
}
