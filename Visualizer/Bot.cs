using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// ボットを表します。
    /// </summary>
    public struct Bot
    {
        /// <summary>
        /// ボット本体を表します。
        /// </summary>
        public dynamic Body { get; set; }

        /// <summary>
        /// ボットのパスを表します。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// アセンブリの情報を表します。
        /// </summary>
        public AssemblyName AssemblyName { get; set; }

        /// <summary>
        /// ボットを接続します。
        /// </summary>
        /// <param name="path">ボットのパス</param>
        public static Bot Connect(string path)
        {
            var bot = new Bot();
            try
            {
                var assembly = Assembly.LoadFrom(path);
                bot.AssemblyName = assembly.GetName();
                Console.WriteLine(bot.AssemblyName.Name);
                bot.Body = Activator.CreateInstance(assembly.GetType("nitkagoshima_sysken.Procon29." + bot.AssemblyName.Name + "." + bot.AssemblyName.Name));
                bot.Path = path;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("DLLファイルが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MissingMethodException)
            {
                MessageBox.Show("メソッドが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MissingFieldException)
            {
                MessageBox.Show("フィールドが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("不明なエラーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bot;
        }
    }
}
