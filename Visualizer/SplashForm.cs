using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();

            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            label3.Text = "Version 1." + version.FileMinorPart + "." + version.FileBuildPart;
        }

        public static SplashForm Form { get; set; } = null;

        public static void ShowSplash()
        {
            if (Form == null)
            {
                Application.Idle += new EventHandler(Application_Idle);
                Form = new SplashForm();
                Form.Show();
            }
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            if (Form != null && Form.IsDisposed == false)
            {
                Form.Close();
            }
            Form = null;
            Application.Idle -= new EventHandler(Application_Idle);
        }
    }
}
