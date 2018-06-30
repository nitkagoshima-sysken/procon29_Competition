using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace nitkagoshima_sysken
{
    namespace procon29_Competition
    {
        namespace Visualizer
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
                    Application.Run(new MainForm());
                }
            }
        }
    }
}