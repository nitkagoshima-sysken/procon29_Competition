using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon29_disp
{
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
        /// そのマスにエージェントがいるかを表します。
        /// <code>isPoint[0,0]</code>で先攻チームの1人目のエージェントがいるかを表します。
        /// <code>isPoint[0,1]</code>で先攻チームの2人目のエージェントがいるかを表します。
        /// <code>isPoint[1,0]</code>で後攻チームの1人目のエージェントがいるかを表します。
        /// <code>isPoint[1,1]</code>で後攻チームの2人目のエージェントがいるかを表します。
        /// </summary>
        private bool[,] isAgent;
        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// <code>isArea[0]</code>で先攻チームのタイルが置かれているかを表します。
        /// <code>isArea[1]</code>で後攻チームのタイルが置かれているかを表します。
        /// </summary>
        private bool[] isArea;

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
        /// そのマスにエージェントがいるかを設定、または取得します。
        /// <code>isPoint[0,0]</code>で先攻チームの1人目のエージェントがいるかを設定、または取得します。
        /// <code>isPoint[0,1]</code>で先攻チームの2人目のエージェントがいるかを設定、または取得します。
        /// <code>isPoint[1,0]</code>で後攻チームの1人目のエージェントがいるかを設定、または取得します。
        /// <code>isPoint[1,1]</code>で後攻チームの2人目のエージェントがいるかを設定、または取得します。
        /// </summary>
        public bool[,] IsPlayer
        {
            get => isAgent;
            set
            {
                if (value.GetLength(0) != 2 || value.GetLength(1) != 2) throw new ArgumentException();
                else isAgent = value;
            }
        }

        /// <summary>
        /// そのマスに先攻チームのエージェントがいるかを取得します。
        /// </summary>
        public bool IsPlayerOfFilstTeam { get => IsPlayer[0, 0] || IsPlayer[0, 1]; }
 
        /// <summary>
        /// そのマスに後攻チームのエージェントがいるかを取得します。
        /// </summary>
        public bool IsPlayerOfLastTeam { get => IsPlayer[1, 0] || IsPlayer[1, 1]; }

        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// <code>isArea[0]</code>で先攻チームのタイルが置かれているかを表します。
        /// <code>isArea[1]</code>で後攻チームのタイルが置かれているかを表します。
        /// </summary>
        public bool[] IsArea
        {
            get => isArea;
            set
            {
                if (value.Length != 2) throw new ArgumentException();
                else isArea = value;
            }
        }

        /// <summary>
        /// Fieldの初期化を行います。
        /// </summary>
        public Field()
        {
            Point = 0;
            IsPlayer = new bool[2, 2];
            IsArea = new bool[2];
        }

    }
}
