using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class PqrDataExpansion
    {
        /// <summary>
        /// PQR形式の文字列をPQR構造体に変換します。
        /// </summary>
        /// <param name="str">変換する文字列</param>
        /// <returns></returns>
        public static PqrData ToPQRData(string str)
        {
            PqrData pqr = new PqrData();

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
                list = DataConverter.SpaceCSVToList(m.Groups["size"].Value.Replace(":", " "));
                if (list.Count != 2) throw new Exception();
                pqr.Size = new Size(list[0], list[1]);

                pqr.Fields = new int[pqr.Size.Height, pqr.Size.Width];
                for (int i = 0; i < m.Groups["row"].Captures.Count; i++)
                {
                    list.Clear();
                    list = DataConverter.SpaceCSVToList(m.Groups["row"].Captures[i].Value.Replace(":", " "));
                    for (int j = 0; j < list.Count; j++)
                    {
                        pqr.Fields[i, j] = list[j];
                    }
                }

                list.Clear();
                list = DataConverter.SpaceCSVToList(m.Groups["one"].Value.Replace(":", " "));
                pqr.One = new Coordinate(list[1] - 1, list[0] - 1);

                list.Clear();
                list = DataConverter.SpaceCSVToList(m.Groups["two"].Value.Replace(":", " "));
                pqr.Two = new Coordinate(list[1] - 1, list[0] - 1);
            }

            return pqr;
        }
    }
}
