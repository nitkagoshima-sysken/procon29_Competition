using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class Program
    {
        [DllImport("Procon29_Algo.dll")]
        static extern void Algorithm();
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Splashを表示します。
            SplashForm.ShowSplash();

            Application.Run(new MainForm());
        }
    }
}
