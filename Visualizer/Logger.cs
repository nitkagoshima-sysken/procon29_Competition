using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// Procon29におけるログの管理を行います。
    /// </summary>
    class Logger
    {
        /// <summary>
        /// ログとして書き込むリッチテキストを設定します
        /// </summary>
        private RichTextBox richTextBox;

        /// <summary>
        /// ログとして書き込むリッチテキストを設定または取得します
        /// </summary>
        public RichTextBox RichTextBox { get => richTextBox; set => richTextBox = value; }

        /// <summary>
        /// Procon29_Loggerを初期化します
        /// </summary>
        /// <param name="richTextBox">ログとして書き込むリッチテキストを設定します</param>
        public Logger(RichTextBox richTextBox)
        {
            RichTextBox = richTextBox;
            Console.WriteLine("");
        }

        /// <summary>
        /// 指定した文字列を指定した色でログに書き込みます。
        /// </summary>
        /// <param name="color">文字色</param>
        /// <param name="message">ログに書き込むメッセージ</param>
        public void Write(Color color, string message)
        {
            RichTextBox.SelectionColor = color;
            RichTextBox.SelectedText = message;

            //カレット位置を末尾に移動
            RichTextBox.SelectionStart = RichTextBox.Text.Length;
            //テキストボックスにフォーカスを移動
            RichTextBox.Focus();
            //カレット位置までスクロール
            RichTextBox.ScrollToCaret();
        }

        /// <summary>
        /// 指定した文字列を指定した色でログに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="color">文字色</param>
        /// <param name="message">ログに書き込むメッセージ</param>
        public void WriteLine(Color color, string message)
        {
            RichTextBox.SelectionColor = color;
            RichTextBox.SelectedText = message + "\n";

            //カレット位置を末尾に移動
            RichTextBox.SelectionStart = RichTextBox.Text.Length;
            //テキストボックスにフォーカスを移動
            RichTextBox.Focus();
            //カレット位置までスクロール
            RichTextBox.ScrollToCaret();
        }
    }
}
