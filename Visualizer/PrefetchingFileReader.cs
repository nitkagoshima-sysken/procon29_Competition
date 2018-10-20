using System;
using System.Drawing;
using System.IO;

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
        public static void BotsTsv(MainForm mainForm)
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
                    MainForm.Log.WriteLine("[Prefetching] Bot \"" + result["A"][0] + "\" was read on orange team by Bot.tsv", Color.SkyBlue);
                    MainForm.ConnectBot(0, result["A"][0]);
                }
                if (result.ContainsKey("B"))
                {
                    MainForm.Log.WriteLine("[Prefetching] Bot \"" + result["B"][0] + "\" was read on lime team by Bot.tsv", Color.SkyBlue);
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

        /// <summary>
        /// 計算機関連のプリフェッチングファイルの読み込みです。
        /// </summary>
        public static void CalcTsv(MainForm mainForm)
        {
            if (System.IO.File.Exists(@".\Prefetching\Calc.tsv"))
            {
                var reader = new TsvReader(@".\Prefetching\Calc.tsv");
                reader.ReadTsvFile();
                reader.ConvertToTsvData();
                var result = reader.TsvData;

                if (result.ContainsKey("Pqr"))
                {
                    MainForm.Log.WriteLine("[Prefetching] PQR File \"" + result["Pqr"][0] + "\" was read by Calc.tsv", Color.SkyBlue);
                    mainForm.OpenPQRFile(result["Pqr"][0].Trim());
                    Console.WriteLine("\"" + result["Pqr"][0] + "\"");
                }
                if (result.ContainsKey("MaxTurn"))
                {
                    MainForm.Log.WriteLine("[Prefetching] Max Turn " + result["MaxTurn"][0] + " was read by Calc.tsv", Color.SkyBlue);
                    MainForm.Calc.MaxTurn = int.Parse(result["MaxTurn"][0]);
                }
            }
            // "Calc.tsv" というディレクトリが存在しない場合、作成する
            else
            {
                using (var file = System.IO.File.Create(@".\Prefetching\Calc.tsv"))
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
        }

        /// <summary>
        /// ファイルパス関連のプリフェッチングファイルの読み込みです。
        /// </summary>
        public static void FilePathTsv(MainForm mainForm)
        {
            if (System.IO.File.Exists(@".\Prefetching\FilePath.tsv"))
            {
                var reader = new TsvReader(@".\Prefetching\FilePath.tsv");
                reader.ReadTsvFile();
                reader.ConvertToTsvData();
                var result = reader.TsvData;

                if (result.ContainsKey("QRCodeReader"))
                {
                    MainForm.Log.WriteLine("[Prefetching] QRCodeReader File Path \"" + result["Pqr"][0].Trim() + "\" was read by FilePath.tsv", Color.SkyBlue);
                    mainForm.FieldDataGenerator_FilePath = result["QRCodeReader"][0].Trim();
                }
                if (result.ContainsKey("FieldDataGenerator"))
                {
                    MainForm.Log.WriteLine("[Prefetching] FieldDataGenerator File Path \"" + result["FieldDataGenerator"][0].Trim() + " was read by FilePath.tsv", Color.SkyBlue);
                    mainForm.FieldDataGenerator_FilePath = result["FieldDataGenerator"][0].Trim();
                }
            }
            // "FilePath.tsv" というディレクトリが存在しない場合、作成する
            else
            {
                using (var file = System.IO.File.Create(@".\Prefetching\FilePath.tsv"))
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
