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
    public partial class TimeMeasurementForm : Form
    {
        public TimeMeasurementForm()
        {
            InitializeComponent();

            TimeMeasurementGridView.ColumnCount = 2;
            TimeMeasurementGridView.Columns[0].HeaderText = "項目";
            TimeMeasurementGridView.Columns[1].HeaderText = "処理時間";
        }
    }
}
