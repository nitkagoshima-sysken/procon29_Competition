using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// プリフェッチングファイルを読み込むための静的クラスです。
    /// </summary>
    static class PrefetchingFileReader
    {
        /// <summary>
        /// ボット関連のプリフェッチングファイルの読み込みです。
        /// </summary>
        static void BotsTsv()
        {
            // "Prefetching" というディレクトリが存在しない場合、作成する
            if (!Directory.Exists("Prefetching"))
            {
                Directory.CreateDirectory("Prefetching");
            }
            if (System.IO.File.Exists(@".\Prefetching\Bots.tsv"))
            {
                var reader = new TsvReader(@".\Prefetching\Bots.tsv");
                reader.ReadTsvFile();
                reader.ConvertToTsvData();
                var result = reader.TsvData;

                if (result.ContainsKey("A"))
                {
                    MainForm.Log.WriteLine("[Prefetching] Bot \"" + result["A"][0] + "\" was read on my team by Bot.tsv", Color.SkyBlue);
                    MainForm.ConnectBot(0, result["A"][0]);
                }
                if (result.ContainsKey("B"))
                {
                    MainForm.Log.WriteLine("[Prefetching] Bot \"" + result["B"][0] + "\" was read on opponent team by Bot.tsv", Color.SkyBlue);
                    MainForm.ConnectBot(1, result["B"][0]);
                }
            }
            // "Bots.tsv" というディレクトリが存在しない場合、作成する
            else
            {
                using (var file = System.IO.File.Create(@".\Prefetching\Bots.tsv"))
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
        }
    }
}
