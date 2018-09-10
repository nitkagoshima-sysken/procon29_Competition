using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    [Serializable]
    public class XmlCell
    {
        /// <summary>
        /// そのマスにおける、ポイントを設定、または取得します。
        /// ポイントには、-16以上16以下の整数値の点数が設定、または取得されます。
        /// ただし、0以下の点数のポイントは、少数しか存在しません。
        /// </summary>
        public int Point { get; set; } = 0;

        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// </summary>
        public bool[] IsTileOn { get; set; } = new bool[2];

        /// <summary>
        /// そのマスがタイルに囲まれているかを表します。
        /// </summary>
        public bool[] IsEnclosed { get; set; } = new bool[2];

        /// <summary>
        /// そのマスがフィールドのどこにあるかを表します。
        /// </summary>
        public Coordinate Coordinate { get; set; } = new Coordinate();

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        public XmlCell()
        {
        }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        /// <param name="cell">コピーするマスを指定します。</param>
        public XmlCell(Cell cell)
        {
            Point = cell.Point;
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                IsTileOn[(int)team] = cell.IsTileOn[team];
                IsEnclosed[(int)team] = cell.IsEnclosed[team];
            }
            Coordinate = new Coordinate(cell.Coordinate);
        }
    }
}
