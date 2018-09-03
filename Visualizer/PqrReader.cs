using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// PQRファイルを読み込むためのクラスです。
    /// </summary>
    public class PqrReader
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
        /// PQRデータを設定または取得します。
        /// </summary>
        public string Stream { get; set; }

        /// <summary>
        /// PqrReaderを初期化します。
        /// </summary>
        public PqrReader()
        {
        }

        /// <summary>
        /// PqrReaderを初期化します。
        /// </summary>
        /// <param name="path">読み込まれる完全なファイルパス。</param>
        public PqrReader(string path)
        {
            Path = path;
        }

        /// <summary>
        /// PqrReaderを初期化します。
        /// </summary>
        /// <param name="path">読み込まれる完全なファイルパス。</param>
        /// <param name="encoding">使用する文字エンコーディング。</param>
        public PqrReader(string path, Encoding encoding)
        {
            Path = path;
            Encoding = encoding;
        }

        /// <summary>
        /// Pathで指定されたファイルを読み込みます。
        /// </summary>
        public void ReadPqrFile()
        {
            if (Encoding == null)
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    ReadToEndPqrFile(sr);
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(Path, Encoding))
                {
                    ReadToEndPqrFile(sr);
                }
            }
        }

        private void ReadToEndPqrFile(StreamReader sr)
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

        /// <summary>
        /// ストリームをリストに変換します。
        /// </summary>
        /// <returns></returns>
        private List<int> ConvertToList(string str)
        {
            var list = new List<int>();

            //前後の改行を削除しておく
            str = str.Trim(new char[] { '\r', '\n' });
            //1行のCSVから各フィールドを取得するための正規表現
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(
                    @"(-?\d*\s)+",
                    System.Text.RegularExpressions.RegexOptions.None);

            //1つの行からフィールドを取り出す
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(str))
            {
                for (int i = 0; i < match.Groups[1].Captures.Count; i++)
                {
                    string field = match.Groups[1].Captures[i].Value;
                    //前後の空白を削除
                    list.Add(int.Parse(field));
                }
            }
            return list;
        }

        /// <summary>
        /// ストリームをPQR構造体に変換します。
        /// </summary>
        /// <returns></returns>
        public PqrData ConvertToPqrData()
        {
            PqrData pqr = new PqrData();
            var list = new List<int>();

            //前後の改行を削除しておく
            var str = Stream.Trim(new char[] { '\r', '\n' });
            //1行のCSVから各フィールドを取得するための正規表現
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(
                    @"^(?<size>\d+\s\d+\x3A)(?<row>(-?\d+\s?)+\x3A)+(?<one>-?\d+\s-?\d+\x3A)(?<two>\d+\s\d+\x3A)$",
                    System.Text.RegularExpressions.RegexOptions.None);

            System.Text.RegularExpressions.MatchCollection mc = regex.Matches(str);

            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                list.Clear();
                list = ConvertToList(m.Groups["size"].Value.Replace(":", " "));
                pqr.Size = new Size(list[0], list[1]);

                pqr.Fields = new int[pqr.Size.Height, pqr.Size.Width];
                for (int i = 0; i < m.Groups["row"].Captures.Count; i++)
                {
                    list.Clear();
                    list = ConvertToList(m.Groups["row"].Captures[i].Value.Replace(":", " "));
                    for (int j = 0; j < list.Count; j++)
                    {
                        pqr.Fields[i, j] = list[j];
                    }
                }

                list.Clear();
                list = ConvertToList(m.Groups["one"].Value.Replace(":", " "));
                pqr.One = new Coordinate(list[1] - 1, list[0] - 1);

                list.Clear();
                list = ConvertToList(m.Groups["two"].Value.Replace(":", " "));
                pqr.Two = new Coordinate(list[1] - 1, list[0] - 1);
            }
            return pqr;
        }
    }
}

