using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procon29_Visualizer
{
    public partial class CreateNewForm : Form
    {
        private string selectPQRFile;
        private int maxTrun;

        public string SelectPQRFile { get => selectPQRFile; set => selectPQRFile = value; }
        public int MaxTrun { get => maxTrun; set => maxTrun = value; }

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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            openFileDialog.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            openFileDialog.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            openFileDialog.Filter = "PQRファイル(*.pqr)|*.pqr|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //1番目の「PQRファイル」が選択されているようにする
            openFileDialog.FilterIndex = 1;
            //タイトルを設定する
            openFileDialog.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            openFileDialog.RestoreDirectory = true;

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

        //MaxTrunのKeyPressイベントハンドラ
        private void MaxTurnTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (e.KeyChar < '0' || '9' < e.KeyChar)
            //{
            //    //押されたキーが 0～9でない場合は、イベントをキャンセルする
            //    e.Handled = true;
            //}
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SelectPQRFile = SelectedPQRFileNameLabel.Text;
            MaxTrun = int.Parse(MaxTurnTextBox.Text);
            Visible = false;
        }
    }
}
