using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 計算機の拡張メソッドを定義するためのクラスです。
    /// </summary>
    static class CalcExtension
    {
        /// <summary>
        /// glaz-egy氏による hydro_go_bot と通信を行う際に用いる引数を計算機から生成します。
        /// </summary>
        /// <param name="calc">対象の計算機</param>
        /// <param name="ourteam">自分のチームを指定します。</param>
        /// <returns></returns>
        public static string HydroGoBotAdapter(this Calc calc, Team ourteam)
        {
            var arg = string.Empty;
            // フィールドのポイントを特定の文字列に変換します。
            for (int x = 0; x < calc.Field.Width; x++)
            {
                arg += "{";
                for (int y = 0; y < calc.Field.Height; y++)
                {
                    arg += calc.Field[x, y].Point.ToString() + ", ";
                }
                arg += "}, ";
            }
            arg = arg.Replace(", }", " }");
            arg += "\n";
            // 自分チームのエージェントがいる場所を特定の文字列に変換します。
            foreach (var agent in calc.Agents[ourteam])
            {
                arg += agent.Position + ", ";
            }
            arg += "\n";
            // 敵チームのエージェントがいる場所を特定の文字列に変換します。
            foreach (var agent in calc.Agents[ourteam.Opponent()])
            {
                arg += agent.Position + ", ";
            }
            arg += "\n";
            // 自分チームのタイルが置かれているマスすべてを特定の文字列に変換します。
            foreach (var coordinate in from cell in calc.Field where cell.IsTileOn[ourteam] select cell.Coordinate)
            {
                arg += coordinate.ToString() + ", ";
            }
            arg += "\n";
            // 敵チームのタイルが置かれているマスすべてを特定の文字列に変換します。
            foreach (var coordinate in from cell in calc.Field where cell.IsTileOn[ourteam.Opponent()] select cell.Coordinate)
            {
                arg += coordinate.ToString() + ", ";
            }
            arg += "\n";
            // 自分チームが移動できる場所すべてを特定の文字列に変換します。
            foreach (var agent in calc.Agents[ourteam])
            {
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    if (calc.Field.CellExist(agent.Position + arrow) && calc.Field.IsMovable(agent, arrow))
                    {
                        arg += (agent.Position + arrow).ToString() + ", ";
                    }
                }
            }
            arg += "\n";
            // 敵チームが移動できる場所すべてを特定の文字列に変換します。
            foreach (var agent in calc.Agents[ourteam.Opponent()])
            {
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    if (calc.Field.CellExist(agent.Position + arrow) && calc.Field.IsMovable(agent, arrow))
                    {
                        arg += (agent.Position + arrow).ToString() + ", ";
                    }
                }
            }
            arg += "\n";
            arg = arg.Replace(", \n", "\n");
            return arg;
        }
    }
}
