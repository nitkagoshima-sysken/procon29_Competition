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

            foreach (var item in MainForm.TimeDataList)
            {
                TimeMeasurementGridView.Rows.Add(item.InspectionItem, item.ProcessingTime);
            }
        }
    }
}
