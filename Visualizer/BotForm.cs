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
    public partial class BotForm : Form
    {
        public BotForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void OragenBotOpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName = "",
                Filter = "ダイナミックリンクライブラリ(*.dll)|*.dll|すべてのファイル(*.*)|*.*",
                FilterIndex = 1,
                Title = "開くファイルを選択してください",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedOrangeBotNameLabel.Text = openFileDialog.FileName;
            }
        }

        private void LimeBotOpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName = "",
                Filter = "ダイナミックリンクライブラリ(*.dll)|*.dll|すべてのファイル(*.*)|*.*",
                FilterIndex = 1,
                Title = "開くファイルを選択してください",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedLimeBotNameLabel.Text = openFileDialog.FileName;
            }
        }

        private void OrangeBotKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (OrangeBotKindComboBox.SelectedIndex)
            {
                case 0:
                    OrageBotOpenButton.Visible = false;
                    break;
                case 1:
                    OrageBotOpenButton.Visible = true;
                    break;
                case 2:
                    OrageBotOpenButton.Visible = false;
                    break;
            }
        }

        private void LimeBotKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LimeBotKindComboBox.SelectedIndex)
            {
                case 0:
                    LimeBotOpenButton.Visible = false;
                    break;
                case 1:
                    LimeBotOpenButton.Visible = true;
                    break;
                case 2:
                    LimeBotOpenButton.Visible = false;
                    break;
            }
        }
    }
}
