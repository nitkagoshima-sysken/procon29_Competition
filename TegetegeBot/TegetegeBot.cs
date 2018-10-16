using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace TegetegeBot
        {
            /* あなたが参照すべきサイト
             * 
             * ボットの作り方を知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/How-to-talk-with-.dll
             * 
             * エージェントについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agent
             * 
             * エージェントステータスコードについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agent-Status-Code
             * 
             * Calcについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Calc
             * 
             * マスについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Cell
             * 
             * Coordinateについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Coordinate
             * 
             * フィールドについて知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Field
             * 
             * FieldにおけるLINQの有効的な使い方を知りたい場合は
             * https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/How-to-use-LINQ
             * 
             */

            /// <summary>
            /// テスト用プログラム
            /// このボットは、
            /// エージェントの周りのマスに行ってみて、
            /// その中で一番、得点が高かったマスを計算するボットです。
            /// </summary>
            class TegetegeBot : Bot.Bot
            {
                /// <summary>
                /// 初期化するところです
                /// </summary>
                /// <param name="team"></param>
                public TegetegeBot() : base() { }

                /// <summary>
                /// あなたが、どこにエージェントを行かせるかを、
                /// この関数に書きます。
                /// </summary>
                /// <returns>どこにエージェントに行かせるか</returns>
                public override AgentActivityData[] Answer()
                {
                    var result = new AgentActivityData[2];

                    // 一人目のエージェントの最高得点を保存するために使う変数
                    var agnt1maxp = -16;
                    // 二人目のエージェントの最高得点を保存するために使う変数
                    var agnt2maxp = -16;
                    // 一人目のエージェントがどこに行くかを保存するために使う変数
                    Coordinate agnt1pos = new Coordinate();
                    // 二人目のエージェントがどこに行くかを保存するために使う変数
                    Coordinate agnt2pos = new Coordinate();

                    /*
                     * これはクエリ式というもので、
                     * まるで、データベースで使うSQL文のようにデータを扱えるようになっている
                     * var list = 
                     * エージェントが行けるマスをリストにする変数をここで宣言している。
                     * from cell in Calc.Field
                     * 《from句》どのデータテーブルからデータを読むか
                     * 今回の場合は、「Calcのフィールドから、マスを一つずつ取り出して、それにcellという名前をつける」
                     * where cell.Coordinate.ChebyshevDistance(Calc.Agents[Team, AgentNumber.One].Position) == 1
                     * 《where句》取り出したいデータに対する条件
                     * 今回の場合は、「取り出したマス(cell)の座標から一人目のエージェントのいる場所までのチェビシェフ距離がちょうど1なら条件に適している」
                     * （つまり、エージェントの隣のマスなら条件に適している）
                     * select cell.Coordinate;
                     * 《select句》条件にヒットしたデータの何を取り出すか
                     * 今回の場合は、条件にヒットしたマス（つまり、エージェントの隣のマス）の座標を取り出して、それをリスト化する
                     * 
                     * …つまり、この行では、一人目のエージェントの隣のマスの一覧をリストにしている。
                     * C#のクエリ式についてはネットで調べれば、もっと詳しく載っているので、そちらを参照した方がいい。
                     */
                    var list =
                        from cell in Calc.Field
                        where cell.Coordinate.ChebyshevDistance(Calc.Agents[OurTeam, AgentNumber.One].Position) == 1
                        select cell.Coordinate;
                    // 取り出したリストから一つずつ座標を取り出して、for文のように繰り返し処理を行う。
                    foreach (var coordinate in list)
                    {
                        var trying = new AgentActivityData[2]
                        {
                            // Bot側のチーム、1人目のエージェントがcoordinateに移動する
                            new AgentActivityData(AgentStatusCode.RequestMovement, coordinate),
                            // Bot側のチーム、2人目のエージェントは何もしない
                            new AgentActivityData(AgentStatusCode.RequestNotToDoAnything)
                        };

                        /*
                         * Simulate関数
                         * これはもしもそこへ行ったらどうなるかをシミュレートしてくれる関数です。
                         */
                        var c = Simulate(team: OurTeam, action: trying);

                        // 今までの中で一番、得点が高かったら、得点とその座標を更新する
                        if (agnt1maxp < c.Field.TotalPoint(OurTeam))
                        {
                            agnt1maxp = c.Field.TotalPoint(OurTeam);
                            agnt1pos = new Coordinate(coordinate);
                        }
                    }
                    // Bot側のチーム、1人目のエージェントがagnt1posに行くことが確定する
                    result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt1pos);

                    /*
                     * 実は、エージェントの場所から1ターンでいける範囲は、
                     * Arrowを使ってもっと簡単に実装できます。
                     * Arrowはenum型で、
                     * Arrow.Up で 上に行く
                     * Arrow.Rightで 右に行く
                     * とかできます。
                     * foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                     * で、Arrowのすべての要素を一つずつ実行できるので、
                     * エージェントのいるところから、周り1マス分のマスに関して、
                     * すべてシミュレーションできます。
                     */
                    foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                    {
                        var c = Simulate(
                            OurTeam,
                            AgentNumber.Two,
                            new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow));

                        if (agnt2maxp < c.Field.TotalPoint(OurTeam))
                        {
                            agnt2maxp = c.Field.TotalPoint(OurTeam);
                            agnt2pos = new Coordinate(Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow);
                        }
                    }
                    result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt2pos);
                    return result;
                }
            }
        }
    }
}