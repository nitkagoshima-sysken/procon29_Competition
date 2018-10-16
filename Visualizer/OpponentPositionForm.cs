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
    public partial class OpponentPositionForm : Form
    {
        public OpponentPositionForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (OpponentPosition1X.Text != string.Empty &&
                OpponentPosition1Y.Text != string.Empty &&
                OpponentPosition2X.Text != string.Empty &&
                OpponentPosition2Y.Text != string.Empty)
            {
                Hide();
            }
            else
            {
                MessageBox.Show("空欄を埋めてください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
