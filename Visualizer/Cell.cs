using System;
using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 競技フィールドにおける任意の1マスのデータ構造を表します。
    /// </summary>
    [Serializable]
    public class Cell
    {
        /// <summary>
        /// そのマスにおける、ポイントを設定、または取得します。
        /// ポイントには、-16以上16以下の整数値の点数が設定、または取得されます。
        /// ただし、0以下の点数のポイントは、少数しか存在しません。
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// </summary>
        public TeamBool IsTileOn { get; set; }

        /// <summary>
        /// そのマスがタイルに囲まれているかを表します。
        /// </summary>
        public TeamBool IsEnclosed { get; set; }

        /// <summary>
        /// そのマスがフィールドのどこにあるかを表します。
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        public Cell()
        {
            Point = 0;
            IsTileOn = new TeamBool();
            IsEnclosed = new TeamBool();
            Coordinate = new Coordinate();
        }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        /// <param name="cell">コピーするマスを指定します。</param>
        public Cell(Cell cell)
        {
            Point = cell.Point;
            IsTileOn = new TeamBool(cell.IsTileOn);
            IsEnclosed = new TeamBool(cell.IsEnclosed);
            Coordinate = new Coordinate(cell.Coordinate);
        }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        /// <param name="coordinate">セルの座標を指定します。</param>
        public Cell(Coordinate coordinate)
        {
            Point = 0;
            IsTileOn = new TeamBool();
            IsEnclosed = new TeamBool();
            Coordinate = coordinate;
        }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        /// <param name="cell">コピーするマスを指定します。</param>
        public Cell(XmlCell cell)
        {
            Point = cell.Point;
            IsTileOn = new TeamBool();
            IsEnclosed = new TeamBool();
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                IsTileOn[team] = cell.IsTileOn[(int)team];
                IsEnclosed[team] = cell.IsEnclosed[(int)team];
            }
            Coordinate = new Coordinate(cell.Coordinate);
        }

        /// <summary>
        /// 人間が判読できる文字列に変換します
        /// </summary>
        /// <returns></returns>
        public override string ToString() => String.Format("{{Point:{0}, IsTileOn: {1}, IsEnclosed: {2}, Coordinate: {3}}}", Point, IsTileOn, IsEnclosed, Coordinate);
    }
}
