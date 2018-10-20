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
    public partial class CreateNewForm : Form
    {
        VisualizerCommonFieldOpenForm VisualizerCommonFieldOpenForm { get; set; } = new VisualizerCommonFieldOpenForm();

        /// <summary>
        /// CreateNewForm を初期化します。
        /// </summary>
        public CreateNewForm()
        {
            InitializeComponent();

            VisualizerCommonFieldOpenForm.OKButton.Click += VisualizerCommonFieldOpenForm_OKButton_Click;

            SelectedPQRFileNameLabel.Text = FieldGenerator.RandomID;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FieldKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FieldKindComboBox.SelectedIndex)
            {
                case 0:
                    SlecetPQRFileButton.Visible = false;
                    break;
                case 1:
                    SlecetPQRFileButton.Visible = true;
                    break;
                case 2:
                    SlecetPQRFileButton.Visible = true;
                    break;
            }
        }

        private void SlecetPQRFileButton_Click(object sender, EventArgs e)
        {
            switch (FieldKindComboBox.SelectedIndex)
            {
                case 1:
                    VisualizerCommonFieldOpenForm.ShowDialog();
                    break;
                case 2:
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        FileName = "",
                        Filter = "PQRファイル(*.pqr)|*.pqr|すべてのファイル(*.*)|*.*",
                        FilterIndex = 1,
                        Title = "開くファイルを選択してください",
                        RestoreDirectory = true
                    };
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SelectedPQRFileNameLabel.Text = openFileDialog.FileName;
                    }
                    break;
            }
        }

        private void VisualizerCommonFieldOpenForm_OKButton_Click(object sender, EventArgs e)
        {
            SelectedPQRFileNameLabel.Text = VisualizerCommonFieldOpenForm.VisualizerCommonFieldCode4Label.Text;
            SelectedPQRFileNameLabel.Text += VisualizerCommonFieldOpenForm.VisualizerCommonFieldCode5Label.Text;
            SelectedPQRFileNameLabel.Text += ((int)(VisualizerCommonFieldOpenForm.NumericUpDown.Value)).ToString("D3");
            VisualizerCommonFieldOpenForm.Hide();
        }
    }
}
