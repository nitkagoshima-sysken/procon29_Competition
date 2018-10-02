using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class TsvReader
    {
        /// <summary>
        /// PQRファイルのパスを設定または取得します。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// エンコーディング方式を設定または取得します。
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// TSVデータを設定または取得します。
        /// </summary>
        public Dictionary<string, List<string>> TsvData { get; set; } = new Dictionary<string, List<string>>();

        public string Stream { get; private set; }

        /// <summary>
        /// TsvReaderを初期化します。
        /// </summary>
        public TsvReader()
        {
        }

        /// <summary>
        /// TsvReaderを初期化します。
        /// </summary>
        /// <param name="path">読み込まれる完全なファイルパス。</param>
        public TsvReader(string path)
        {
            Path = path;
        }

        /// <summary>
        /// PqrReaderを初期化します。
        /// </summary>
        /// <param name="path">読み込まれる完全なファイルパス。</param>
        /// <param name="encoding">使用する文字エンコーディング。</param>
        public TsvReader(string path, Encoding encoding)
        {
            Path = path;
            Encoding = encoding;
        }

        /// <summary>
        /// Pathで指定されたファイルを読み込みます。
        /// </summary>
        public void ReadTsvFile()
        {
            if (Encoding == null)
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    ReadToEndTsvFile(sr);
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(Path, Encoding))
                {
                    ReadToEndTsvFile(sr);
                }
            }
        }

        private void ReadToEndTsvFile(StreamReader sr)
        {
            try
            {
                Stream = sr.ReadToEnd();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("存在しないディレクトリです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("存在しないファイルです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("何らかの原因によってファイルの読み込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void ConvertToTsvData()
        {
            foreach (var line in Stream.Split('\n'))
            {
                string command = line.Split('\t')[0];
                if (!TsvData.ContainsKey(command))
                {
                    TsvData.Add(command, new List<string>());
                    TsvData[command].AddRange(line.Split('\t'));
                    TsvData[command].RemoveAt(0);
                }
            }
        }
    }
}
