using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon29_disp
{
    /// <summary>
    /// チームが先攻か後攻かを表すための列挙体
    /// </summary>
    enum Team
    {
        /// <summary>
        /// チームAを表します。
        /// </summary>
        A,
        /// <summary>
        /// チームBを表します。
        /// </summary>
        B
    }

    /// <summary>
    /// チームのエージェントを表すための列挙体
    /// </summary>
    enum Agent
    {
        /// <summary>
        /// 1人目のエージェントを表します。
        /// </summary>
        One,
        /// <summary>
        /// 2人目のエージェントを表します。
        /// </summary>
        Two
    }

    /// <summary>
    /// 競技フィールドにおける任意の1マスのデータ構造を表します。
    /// </summary>
    public class Field
    {
        /// <summary>
        /// そのマスにおける、ポイントを表します。
        /// ポイントには、-16以上16以下の整数値の点数が付与されます。
        /// ただし、0以下の点数のポイントは、少数しか存在しません。
        /// </summary>
        private int point;
        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// <code>isArea[0]</code>で先攻チームのタイルが置かれているかを表します。
        /// <code>isArea[1]</code>で後攻チームのタイルが置かれているかを表します。
        /// </summary>
        private bool[] isDirectArea;
        private bool[] isIndirectArea;
        private bool[] isAreaCheck;

        /// <summary>
        /// そのマスにおける、ポイントを設定、または取得します。
        /// ポイントには、-16以上16以下の整数値の点数が設定、または取得されます。
        /// ただし、0以下の点数のポイントは、少数しか存在しません。
        /// </summary>
        public int Point
        {
            get
            {
                return point;
            }
            set
            {
                if (value < -16 || 16 < value)
                {
                    throw new ArgumentOutOfRangeException("[ERR1] 'point' was not between -16 and 16");
                }
                else
                {
                    point = value;
                }
            }
        }

        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// </summary>
        public bool[] IsDirectArea { get => isDirectArea; set => isDirectArea = value; }
        /// <summary>
        /// そのマスがタイルに囲まれているかを表します。
        /// </summary>
        public bool[] IsIndirectArea { get => isIndirectArea; set => isIndirectArea = value; }
        /// <summary>
        /// そのマスの囲み判定が行われたかを表します。
        /// </summary>
        public bool[] IsAreaCheck { get => isAreaCheck; set => isAreaCheck = value; }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        public Field()
        {
            Point = 0;
            IsDirectArea = new bool[2];
            IsIndirectArea = new bool[2];
            IsAreaCheck = new bool[2];
        }
    }
}
