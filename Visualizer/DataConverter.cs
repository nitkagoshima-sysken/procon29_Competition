using System;
using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
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
    }
}