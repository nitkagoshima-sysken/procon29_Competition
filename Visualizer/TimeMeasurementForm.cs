using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class TimeMeasurementForm : Form
    {
        public TimeMeasurementForm()
        {
            InitializeComponent();

            var sw = new Stopwatch();
            var sw2 = new Stopwatch();

            sw.Start();
            new Calc(new XmlCalc(MainForm.Calc).DeepClone());
            TimeMeasurementGridView.Rows.Add("Deep Clone", sw.ElapsedMilliseconds);
            sw.Reset();
            if (MainForm.Bot[0] != null)
            {
                sw.Start();
                MainForm.Bot[0].OurTeam = Team.A;
                MainForm.Bot[0].Log = MainForm.BotLog;
                MainForm.Bot[0].Question(MainForm.Calc);
                sw2.Start();
                MainForm.Bot[0].Answer();
                sw2.Stop();
                sw.Stop();
                TimeMeasurementGridView.Rows.Add(MainForm.BotName[0] + " (Our Team)", sw.ElapsedMilliseconds);
                TimeMeasurementGridView.Rows.Add(MainForm.BotName[0] + " Answer (Our Team)", sw2.ElapsedMilliseconds);
                sw.Reset();
                sw2.Reset();
            }
            if (MainForm.Bot[1] != null)
            {
                sw.Start();
                MainForm.Bot[1].OurTeam = Team.A;
                MainForm.Bot[1].Log = MainForm.BotLog;
                MainForm.Bot[1].Question(MainForm.Calc);
                sw2.Start();
                MainForm.Bot[1].Answer();
                sw2.Stop();
                sw.Stop();
                TimeMeasurementGridView.Rows.Add(MainForm.BotName[1] + " (Opponent Team)", sw.ElapsedMilliseconds);
                TimeMeasurementGridView.Rows.Add(MainForm.BotName[1] + " Answer (Our Team)", sw2.ElapsedMilliseconds);
                sw.Reset();
            }
        }
    }
}
