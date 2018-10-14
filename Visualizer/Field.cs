using System;
using System.Collections;
using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 競技フィールドを表します。
    /// </summary>
    [Serializable]
    public class Field : IEnumerable<Cell>
    {
        Cell[,] Array { get; set; }

        /// <summary>
        /// Cellを列挙します。
        /// </summary>
        /// <returns>列挙されたセル</returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (Cell item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Cell item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            foreach (Cell item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        public int Width => Array.GetLength(0);

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        public int Height => Array.GetLength(1);

        /// <summary>
        /// Agentsを初期化します。
        /// </summary>
        public Field()
        {
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="width">フィールドの幅</param>
        /// <param name="height">フィールドの高さ</param>
        public Field(int width, int height)
        {
            Array = new Cell[width, height];
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="width">フィールドの幅</param>
        /// <param name="height">フィールドの高さ</param>
        /// <param name="field">フィールドのデータ</param>
        public Field(int width, int height, int[,] field)
        {
            Array = new Cell[width, height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    this[x, y] = new Cell { Point = field[y, x], Coordinate = new Coordinate(x, y) };
                }
            }
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="field">コピーしたいフィールド</param>
        public Field(Field field)
        {
            Array = new Cell[field.Width, field.Height];
            foreach (var item in field)
                this[item.Coordinate] = new Cell(item);
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="x">横の座標</param>
        /// <param name="y">縦の座標</param>
        /// <returns></returns>
        public Cell this[int x, int y]
        {
            set { Array[x, y] = value; }
            get { return Array[x, y]; }
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="coordinate">座標</param>
        /// <returns></returns>
        public Cell this[Coordinate coordinate]
        {
            set { Array[coordinate.X, coordinate.Y] = value; }
            get { return Array[coordinate.X, coordinate.Y]; }
        }

        /// <summary>
        /// フィールド上で上下反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public Coordinate FlipVertical(Coordinate point) => new Coordinate(point.X, Height - 1 - point.Y);

        /// <summary>
        /// フィールド上で左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public Coordinate FlipHorizontal(Coordinate point) => new Coordinate(Width - 1 - point.X, point.Y);

        /// <summary>
        /// フィールド上で上下左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public Coordinate FlipHorizontalAndVertical(Coordinate point) => new Coordinate(Width - 1 - point.X, Height - 1 - point.Y);

        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="obj"></param>
        public void Add(System.Object obj)
        {
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// 上下対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsVerticallySymmetrical => Array.VerticallySymmetricalCheck();

        /// <summary>
        /// 左右対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsHorizontallySymmetrical => Array.HorizontallySymmetricalCheck();

        /// <summary>
        /// フィールド上の対象の座標にマスが存在するかしないかを返します
        /// </summary>
        /// <param name="coordinate">対象の座標</param>
        /// <returns></returns>
        public bool CellExist(Coordinate coordinate)
        {
            return CellExist(coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// フィールド上の対象の座標にマスが存在するかしないかを返します
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CellExist(int x, int y)
        {
            return (0 <= x) && (x < Width) && (0 <= y) && (y < Height); 
        }

        /// <summary>
        /// 移動先に敵のタイルが置いてないか判定します。
        /// </summary>
        /// <param name="agent">対象のエージェント</param>
        /// <param name="arrow">移動方向</param>
        /// <returns></returns>
        public bool IsMovable(Agent agent, Arrow arrow)
        {
            return !this[agent.Position + arrow].IsTileOn[agent.Team.Opponent()];
        }

        /// <summary>
        /// 移動先に自分のタイルが置いてあるか判定します。
        /// </summary>
        /// <param name="agent">対象のエージェント</param>
        /// <param name="arrow">移動方向</param>
        /// <returns></returns>
        public bool IsRemovableOurTile(Agent agent, Arrow arrow)
        {
            return this[agent.Position + arrow].IsTileOn[agent.Team];
        }

        /// <summary>
        /// 移動先に敵のタイルが置いてあるか判定します。
        /// </summary>
        /// <param name="agent">対象のエージェント</param>
        /// <param name="arrow">移動方向</param>
        /// <returns></returns>
        public bool IsRemovableOpponentTile(Agent agent, Arrow arrow)
        {
            return this[agent.Position + arrow].IsTileOn[agent.Team.Opponent()];
        }
    }
}
