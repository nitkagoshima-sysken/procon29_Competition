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
                SelectedOrangeBotNameLabel.Text = openFileDialog.FileName;
            }
        }
    }
}
