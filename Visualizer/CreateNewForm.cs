using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class CreateNewForm : Form
    {
        /// <summary>
        /// 開くPQRファイルを設定または取得します。
        /// </summary>
        public string SelectPQRFile { get; set; }

        /// <summary>
        /// 最大ターンを設定または取得します。
        /// </summary>
        public int MaxTrun { get; set; }

        /// <summary>
        /// 自分のチームにつくボットを設定または取得します。
        /// </summary>
        dynamic Bot0 { get; set; }

        /// <summary>
        /// 敵のチームにつくボットを設定または取得します。
        /// </summary>
        dynamic Bot1 { get; set; }
        
        /// <summary>
        /// 新たなCalcクラスを作成します。
        /// </summary>
        public Calc NewCalc { get; set; }

        /// <summary>
        /// 初期化します。
        /// </summary>
        public CreateNewForm()
        {
            InitializeComponent();
            //フォームが最大化されないようにする
            this.MaximizeBox = false;
            //フォームが最小化されないようにする
            this.MinimizeBox = false;
        }

        private void SelectPQRFileButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog openFileDialog = new OpenFileDialog
            {

                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                FileName = "",
                //はじめに表示されるフォルダを指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                InitialDirectory = "",
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                Filter = "PQRファイル(*.pqr)|*.pqr|すべてのファイル(*.*)|*.*",
                //[ファイルの種類]ではじめに選択されるものを指定する
                //1番目の「PQRファイル」が選択されているようにする
                FilterIndex = 1,
                //タイトルを設定する
                Title = "開くファイルを選択してください",
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                RestoreDirectory = true
            };

            //ダイアログを表示する
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedPQRFileNameLabel.Text = openFileDialog.FileName;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SelectPQRFile = SelectedPQRFileNameLabel.Text;
            MainForm.MaxTurn = int.Parse(MaxTurnTextBox.Text);
            MainForm.Bot[0] = Bot0;
            MainForm.Bot[1] = Bot1;
            Visible = false;
        }

        private void SelectBotButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                FileName = "",
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                Filter = "ダイナミックリンクライブラリ(*.dll)|*.dll|すべてのファイル(*.*)|*.*",
                //[ファイルの種類]ではじめに選択されるものを指定する
                //1番目の「PQRファイル」が選択されているようにする
                FilterIndex = 1,
                //タイトルを設定する
                Title = "開くファイルを選択してください",
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                RestoreDirectory = true
            };

            //ダイアログを表示する
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Assembly m = Assembly.LoadFrom(openFileDialog.FileName);

                    System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(openFileDialog.FileName, @"^[A-Z]:\\(.*\\)+(?<file>.*).dll$");

                    foreach (System.Text.RegularExpressions.Match match in mc)
                    {
                        Bot1 = Activator.CreateInstance(m.GetType("nitkagoshima_sysken.Procon29." + match.Groups["file"].Value + "." + match.Groups["file"].Value));
                        MainForm.BotName[1] = match.Groups["file"].Value;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("不正なdllです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SelectBotButton0_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                FileName = "",
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                Filter = "ダイナミックリンクライブラリ(*.dll)|*.dll|すべてのファイル(*.*)|*.*",
                //[ファイルの種類]ではじめに選択されるものを指定する
                //1番目の「PQRファイル」が選択されているようにする
                FilterIndex = 1,
                //タイトルを設定する
                Title = "開くファイルを選択してください",
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                RestoreDirectory = true
            };

            //ダイアログを表示する
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Assembly m = Assembly.LoadFrom(openFileDialog.FileName);

                    System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(openFileDialog.FileName, @"^[A-Z]:\\(.*\\)+(?<file>.*).dll$");

                    foreach (System.Text.RegularExpressions.Match match in mc)
                    {
                        Bot0 = Activator.CreateInstance(m.GetType("nitkagoshima_sysken.Procon29." + match.Groups["file"].Value + "." + match.Groups["file"].Value));
                        MainForm.BotName[0] = match.Groups["file"].Value;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("不正なdllです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
