using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class BotLogForm : Form
    {
        public BotLogForm()
        {
            InitializeComponent();
        }

        private void BotLogForm_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
