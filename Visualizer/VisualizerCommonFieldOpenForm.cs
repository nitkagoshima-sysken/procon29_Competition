using System;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class VisualizerCommonFieldOpenForm : Form
    {
        /// <summary>
        /// VisualizerCommonFieldOpenForm を初期化します。
        /// </summary>
        public VisualizerCommonFieldOpenForm()
        {
            InitializeComponent();
        }

        private void FieldKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisualizerCommonFieldCode4Label.Text = FieldKindComboBox.Text.Remove(FieldKindComboBox.Text.IndexOf(' ')).ToString();
        }

        private void FieldSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisualizerCommonFieldCode5Label.Text = FieldSizeComboBox.Text[0].ToString();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
