using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace Visualizer
        {
            /// <summary>
            /// PQR形式のデータを表します。
            /// </summary>
            struct PQRData
            {
                Size size;
                int[,] fields;
                Point one, two;

                /// <summary>
                /// フィールドの大きさを表します。
                /// </summary>
                internal Size Size { get => size; set => size = value; }
                /// <summary>
                /// フィールドを表します。
                /// </summary>
                public int[,] Fields { get => fields; set => fields = value; }
                /// <summary>
                /// 1人目のエージェントの位置を表します。
                /// </summary>
                public Point One { get => one; set => one = value; }
                /// <summary>
                /// 2人目のエージェントの位置を表します。
                /// </summary>
                public Point Two { get => two; set => two = value; }
            }

            /// <summary>
            /// サイズを表します。
            /// </summary>
            struct Size
            {
                int height, width;

                /// <summary>
                /// 高さを表します。
                /// </summary>
                public int Height { get => height; set => height = value; }
                /// <summary>
                /// 幅を表します。
                /// </summary>
                public int Width { get => width; set => width = value; }
                public Size(int height, int width) { this.height = height; this.width = width; }

            }

            /// <summary>
            /// いろいろなデータを宣言させます。
            /// </summary>
            static class DataConverter
            {

                /// <summary>
                /// スペース区切りのCSV形式の文字列をListに変換します。
                /// </summary>
                /// <param name="str">変換する文字列</param>
                /// <returns></returns>
                public static List<int> SpaceCSVToList(string str)
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
                /// PQR形式の文字列をPQR構造体に変換します。
                /// </summary>
                /// <param name="str">変換する文字列</param>
                /// <returns></returns>
                public static PQRData ToPQRData(string str)
                {
                    PQRData pqr = new PQRData();

                    var list = new List<int>();

                    //前後の改行を削除しておく
                    str = str.Trim(new char[] { '\r', '\n' });
                    //1行のCSVから各フィールドを取得するための正規表現
                    System.Text.RegularExpressions.Regex regex =
                        new System.Text.RegularExpressions.Regex(
                            @"^(?<size>\d+\s\d+\x3A)(?<row>(-?\d+\s?)+\x3A)+(?<one>-?\d+\s-?\d+\x3A)(?<two>\d+\s\d+\x3A)$",
                            System.Text.RegularExpressions.RegexOptions.None);

                    System.Text.RegularExpressions.MatchCollection mc = regex.Matches(str);

                    foreach (System.Text.RegularExpressions.Match m in mc)
                    {
                        list.Clear();
                        list = SpaceCSVToList(m.Groups["size"].Value.Replace(":", " "));
                        if (list.Count != 2) throw new Exception();
                        pqr.Size = new Size(list[0], list[1]);

                        pqr.Fields = new int[pqr.Size.Height, pqr.Size.Width];
                        for (int i = 0; i < m.Groups["row"].Captures.Count; i++)
                        {
                            list.Clear();
                            list = SpaceCSVToList(m.Groups["row"].Captures[i].Value.Replace(":", " "));
                            for (int j = 0; j < list.Count; j++)
                            {
                                pqr.Fields[i, j] = list[j];
                            }
                        }

                        list.Clear();
                        list = SpaceCSVToList(m.Groups["one"].Value.Replace(":", " "));
                        pqr.One = new Point(list[1] - 1, list[0] - 1);

                        list.Clear();
                        list = SpaceCSVToList(m.Groups["two"].Value.Replace(":", " "));
                        pqr.Two = new Point(list[1] - 1, list[0] - 1);
                    }

                    return pqr;
                }
            }
        }
    }
}