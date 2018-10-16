using System.Diagnostics;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class TimeMeasurementForm : Form
    {
        /// <summary>
        /// 時間計測を表示するフォームです。
        /// </summary>
        public TimeMeasurementForm()
        {
            InitializeComponent();

            var sw = new Stopwatch();
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
