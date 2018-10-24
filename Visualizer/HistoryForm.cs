using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 試合終了後に試合を見返すためのフォームです。
    /// </summary>
    public partial class HistoryForm : Form
    {
        /// <summary>
        /// 表示する計算機を設定または取得します。
        /// </summary>
        public Calc Calc { get; set; }

        public Logger Log { get; set; }

        /// <summary>
        /// 初期化します。
        /// </summary>
        public HistoryForm()
        {
            InitializeComponent();
        }

        public HistoryForm(Calc calc)
        {
            InitializeComponent();

            Calc = calc;
            Log = new Logger(InfoLabel);
            TurnTrackBar.Minimum = 0;
            TurnTrackBar.Maximum = calc.MaxTurn;
            TurnTrackBar.Value = 0;
            Showing();
            Serif();
        }

        private void HistoryForm_Resize(object sender, EventArgs e)
        {
            if (Calc != null) Showing();
        }

        private void TurnTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Showing();
            Serif();
        }

        private void Showing()
        {
            Show show;
            show = new Show(Calc, pictureBox1);
            show.NoCursorShowing(TurnTrackBar.Value);

        }

        private Color TeamColor(int i)
        {
            return (i == 0) ? Color.Orange : Color.Lime;
        }

        private Color TeamColor(Team i)
        {
            return TeamColor((int)i);
        }

        private string TeamName(Team i)
        {
            return (i == Team.A) ? "オレンジチーム" : "ライムチーム";
        }


        int rs001 = 0;
        private string Kaizen_serif()
        {
            var str = new string[]
            {
                "こういうミスはできるだけなくした方がいいと思いますけどねえ。",
                "初歩的なミスですねー。",
                "1ターンでも無駄にすると、負けてしまうかもしれないこのゲームの特性からして、このミスは致命的ですね。",
            };
            return str[(rs001++)%str.Length];
        }

        int rs002 = 0;
        private string Colision_Serif()
        {
            var str = new string[]
            {
                "なるほど、原因は敵との衝突ですね。",
                "原因は相手のチームのエージェントに衝突したようですね。",
                "敵と衝突したようですね。",
                "ありがちなことですが、敵のチームのエージェントと衝突して行動できなかったみたいですね。",
                "敵との衝突！",
            };
            return str[(rs002++) % str.Length];
        }

        private void Serif()
        {
            var random = new Random();
            InfoLabel.Text = string.Empty;
            int turn = TurnTrackBar.Value;

            if (turn != 0)
            {
                Log.WriteLine("オレンジチームの合計得点：" + Calc.History[turn].Field.TotalPoint(Team.A) + "点", Color.Orange);
                Log.WriteLine("ライムチームの合計得点：" + Calc.History[turn].Field.TotalPoint(Team.B) + "点", Color.Lime);
                Log.WriteLine("");

                if ((Calc.History[turn].Field.TotalPoint(Team.A) - Calc.History[turn].Field.TotalPoint(Team.B)) *
                    (Calc.History[turn - 1].Field.TotalPoint(Team.A) - Calc.History[turn - 1].Field.TotalPoint(Team.B))
                    < 0)
                {
                    Log.WriteLine("ここで逆転しました！");
                    Log.WriteLine("白熱した戦いです。");
                    Log.WriteLine("");
                }

                if (turn < 5 &&
                    turn != Calc.MaxTurn &&
                    (Calc.History[turn].Field.TotalPoint(Team.A) == Calc.History[turn].Field.TotalPoint(Team.B)))
                {
                    Log.WriteLine("現在、両チーム同点です。");
                    Log.WriteLine("熱い戦いが繰り広げられています！");
                    Log.WriteLine("");
                }
            }

            if (turn == 0)
            {
                Log.WriteLine(
                    "さあ、今回の試合は" + Calc.MaxTurn + "ターンの試合です。" +
                    "下にあるトラックバーをスライドすると表示されるターンが変更されるので、そこを操作しましょう。");
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        Log.Write("一方、");
                    }
                    Log.Write(((i == 0) ? "オレンジ" : "ライム") + "チーム", TeamColor(i));
                    Log.Write("は、");
                    if (MainForm.Bot[i].Body != null)
                    {
                        Log.Write(MainForm.Bot[i].AssemblyName.Name, TeamColor(i));
                    }
                    else if (MainForm.Bot[i].AssemblyName.Name == "Human")
                    {
                        Log.Write("人間", TeamColor(i));
                    }
                    else if (MainForm.Bot[i].AssemblyName.Name == "Hydro Go Bot")
                    {
                        Log.Write("Hydro Go Bot", TeamColor(i));
                    }
                    Log.WriteLine("が操作します。");
                }
            }
            else if (Calc.MaxTurn - turn == 10)
            {
                Log.WriteLine("さて、試合も残すところ、あと10ターンになりました。");
            }
            else if (Calc.MaxTurn - turn == 1)
            {
                Log.WriteLine("最後の1ターンです。");
            }
            else if (0 < Calc.MaxTurn - turn && Calc.MaxTurn - turn <= 10)
            {
                Log.WriteLine("さて、試合はあと" + (Calc.MaxTurn - turn) + "ターンで終了します。");
            }
            else if (turn == Calc.MaxTurn)
            {
                Log.WriteLine("終～了～！");
                Log.Write("勝者は");
                if (Calc.Field.TotalPoint(Team.A) > Calc.Field.TotalPoint(Team.B))
                {
                    Log.Write("オレンジチーム", Color.Orange);
                }
                else if (Calc.Field.TotalPoint(Team.A) < Calc.Field.TotalPoint(Team.B))
                {
                    Log.Write("ライムチーム", Color.Lime);
                }
                else
                {
                    Log.Write("・・・どうやら引き分けのよう");
                }
                Log.WriteLine("です！");
                if (Calc.Field.TotalPoint(Team.A) * Calc.Field.TotalPoint(Team.B) != 0)
                {
                    var rate = (double)Calc.Field.TotalPoint(Team.A) / Calc.Field.TotalPoint(Team.B);
                    if (rate <= 0.5)
                    {
                        Log.Write("オレンジチームの2倍以上の差をつけて");
                        Log.Write("ライムチーム", Color.Lime);
                        Log.WriteLine("が勝ちました。");
                        Log.WriteLine("圧勝です！");
                    }
                    else if (rate >= 2.0)
                    {
                        Log.Write("ライムチームの2倍以上の差をつけて");
                        Log.Write("オレンジチーム", Color.Orange);
                        Log.WriteLine("が勝ちました。");
                        Log.WriteLine("圧勝です！");
                    }
                    else if (
                        0 < Math.Abs(Calc.Field.TotalPoint(Team.A) - Calc.Field.TotalPoint(Team.B)) &&
                        Math.Abs(Calc.Field.TotalPoint(Team.A) - Calc.Field.TotalPoint(Team.B)) < 10)
                    {
                        Log.WriteLine("両者の差なんとたったの" + (Math.Abs(Calc.Field.TotalPoint(Team.A) - Calc.Field.TotalPoint(Team.B))) + "点と、僅差で勝ちました！");
                        Log.WriteLine("激戦でしたね。");
                    }
                    Log.WriteLine("");
                }
            }

            if (turn != 0)
            {
                foreach (var agent in Calc.History[turn].Agents)
                {
                    var asc = Calc.History[turn - 1].AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData;
                    var dest = Calc.History[turn - 1].AgentsActivityData[agent.Team, agent.AgentNumber].Destination;

                    if (asc == AgentStatusCode.SucceededInMoving)
                    {
                        Log.Write(agent.Name, TeamColor(agent.Team));
                        Log.WriteLine("が" + dest + "のところへ移動しました。");
                        Log.Write("これにより、" + TeamName(agent.Team) + "の得点が");
                        if (Calc.Field[dest].Point > 0)
                        {
                            Log.WriteLine(Calc.Field[dest].Point + "点増えました。");
                        }
                        else if (Calc.Field[dest].Point < 0)
                        {
                            Log.WriteLine(-Calc.Field[dest].Point + "点減ってしまいました。");
                        }
                        else
                        {
                            Log.WriteLine("増えるかと思いきや、実は移動したタイルはなんと0点なんですね。");
                            Log.WriteLine("なぜそこに移動したか、なにか考えがあるのでしょうか。");
                        }
                    }
                    else if (asc == AgentStatusCode.SucceededInRemovingOpponentTile)
                    {
                        Log.Write(agent.Name, TeamColor(agent.Team));
                        Log.WriteLine("が" + dest + "にあった敵のタイルを取り外しました。");
                        Log.Write("これにより、" + TeamName(agent.Team.Opponent()) + "の得点が");
                        if (Calc.Field[dest].Point < 0)
                        {
                            Log.WriteLine(-Calc.Field[dest].Point + "点増えてしまいした。");
                        }
                        else if (Calc.Field[dest].Point > 0)
                        {
                            Log.WriteLine(Calc.Field[dest].Point + "点減りました。");
                        }
                        else
                        {
                            Log.WriteLine("減るかと思いきや、実は取り外したタイルはなんと0点なんですね。");
                            Log.WriteLine("なぜそこのタイルを取り外したか、なにか考えがあるのでしょうか。");
                        }
                    }
                    else if (asc == AgentStatusCode.SucceededInRemovingOurTile)
                    {
                        Log.WriteLine("ここでボットには珍しい自分チームのタイルの取り外しです。");
                        Log.Write(agent.Name, TeamColor(agent.Team));
                        Log.WriteLine("がなんと" + dest + "にあった自分のタイルを取り外しました。");
                        Log.Write("これにより、" + TeamName(agent.Team) + "の得点が");
                        if (Calc.Field[dest].Point > 0)
                        {
                            Log.WriteLine(Calc.Field[dest].Point + "点増えました。");
                        }
                        else if (Calc.Field[dest].Point < 0)
                        {
                            Log.WriteLine(-Calc.Field[dest].Point + "点減ってしまいました。");
                        }
                        else
                        {
                            Log.WriteLine("増えるかと思いきや、実は取り外したタイルはなんと0点なんですね。");
                            Log.WriteLine("なぜそこのタイルを外したか、なにか考えがあるのでしょうか。");
                        }
                    }
                    else if (asc == AgentStatusCode.SucceededNotToDoAnything)
                    {
                        Log.Write(agent.Name, TeamColor(agent.Team));
                        Log.WriteLine("がサボっています。");
                        Log.WriteLine("動いていません。");
                        if (turn > 1 &&
                            Calc.History[turn - 2].AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData == AgentStatusCode.SucceededNotToDoAnything)
                        {
                            Log.WriteLine("しかも、2回連続ですね。");
                        }
                        Log.WriteLine("一体、何の意味があるのでしょうか。");
                    }
                    else if (asc.IsFailed())
                    {
                        var str = new string[] { "おおっと、", "ここで、", "おやおや、", "おおっとここで、", };
                        Log.Write(str[random.Next(0, str.Length)]);
                        Log.Write(agent.Name, TeamColor(agent.Team));
                        Log.Write("が、");
                        switch (asc.ToAction())
                        {
                            case AgentStatusCodeAction.Movement:
                                Log.WriteLine("移動に失敗したようです。");
                                break;
                            case AgentStatusCodeAction.RemovementOurTile:
                                Log.WriteLine("自分のタイルを外すのに失敗したようです。");
                                break;
                            case AgentStatusCodeAction.RemovementOpponentTile:
                                Log.WriteLine("敵のタイルを外すのに失敗したようです。");
                                break;
                        }
                        switch (asc)
                        {
                            case AgentStatusCode.FailedInMovingByCollisionWithEachOther:
                                Log.WriteLine(Colision_Serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByCollisionWithEachOther:
                                Log.WriteLine("原因を調べてみると、相手のチームのエージェントと衝突したようですね。");
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByCollisionWithEachOther:
                                Log.WriteLine("んーなるほど、相手のチームのエージェントと衝突したようですね。");
                                break;
                            case AgentStatusCode.FailedInMovingBySelfCollision:
                                Log.WriteLine("自分のチームのエージェントと衝突したようです。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileBySelfCollision:
                                Log.WriteLine("自分のチームのエージェントと衝突しているようです。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileBySelfCollision:
                                Log.WriteLine("自分のチームのエージェントと衝突して失敗したようです。");
                                Log.WriteLine(Kaizen_serif());
                                Log.WriteLine("特に、自分たちの同士のミスはないようにしてほしいですね。");
                                break;
                            case AgentStatusCode.FailedInMovingByTryingToGoOutOfTheField:
                                Log.WriteLine("フィールド外に出ようとしていたみたいですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByTryingToGoOutOfTheField:
                                Log.WriteLine("どうやら、存在しないフィールド外にある自分のタイルを取ろうとしていたみたいですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByTryingToGoOutOfTheField:
                                Log.WriteLine("どうやら、フィールド外にある存在しない敵のチームのタイルを取ろうとしていたみたいですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInMovingByBeingNotMooreNeighborhood:
                                Log.WriteLine("自分の周りの1マス以外の遠く離れた場所へ移動しようとしたみたいですね。");
                                Log.WriteLine("空でも飛びたいのでしょうか。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByBeingNotMooreNeighborhood:
                                Log.WriteLine("自分の周りの1マス以外の遠く離れた場所にある自分のチームのタイルを取り外そうとしたみたいですね。");
                                Log.WriteLine("まあ、誰にでも黒歴史がありますが、");
                                Log.WriteLine("今回は離れていても、消したい記憶があったのでしょうか。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByBeingNotMooreNeighborhood:
                                Log.WriteLine("自分の周りの1マス以外の遠く離れた場所にある敵チームのタイルを取り外そうとしたみたいですね。");
                                Log.WriteLine("まるでICBMです。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToMoveBecauseYouAreThereAlready:
                                Log.WriteLine("自分の場所へ移動しようとしたみたいですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToRemoveTilesFromYourTeamBecauseYouAreThere:
                                Log.WriteLine("今いる自分の場所にあるタイルを外そうとしたみたいですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInMovingByCollisionWithTheLazyOurTeam:
                                Log.WriteLine("動かない味方のエージェントと衝突しましたね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileWithTheLazyOurTeam:
                                Log.WriteLine("動かない味方のエージェントと衝突しましたね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInMovingByCollisionWithTheLazyOpponent:
                                Log.WriteLine("動かない味方のエージェントと衝突しましたね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileWithTheLazyOpponent:
                                Log.WriteLine("動かない敵のエージェントと衝突しましたね。");
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByDoingTileNotExist:
                                Log.WriteLine("存在しないはずのタイルを取り除こうとしたみたいですね。");
                                Log.WriteLine("まあ、4Iには存在しないはずの4限目があるので、どちらかといえばそれを取り除いてほしいのですが、");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByDoingTileNotExist:
                                Log.WriteLine("存在しないはずのタイルを取り除こうとしたみたいですね。");
                                Log.WriteLine("まあ、4Iには存在しないはずの4限目があるので、どちらかといえばそれを取り除いてほしいのですが、");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInMovingByInvolvedInOtherCollisions:
                                Log.WriteLine("移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、移動に失敗したようですね。");
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByInvolvedInOtherCollisions:
                                Log.WriteLine("移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、自分のチームからタイルを取り除くことに失敗したようですね。");
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByInvolvedInOtherCollisions:
                                Log.WriteLine("移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、相手のチームからタイルを取り除くことに失敗したようですね。");
                                break;
                            case AgentStatusCode.FailedInMovingByTryingItWithoutRemovingTheOpponentTile:
                                Log.WriteLine("敵のタイルが置いてあるマスを取り外さずに、移動しようとしたため、移動に失敗したようですね。");
                                Log.WriteLine(Kaizen_serif());
                                break;
                            case AgentStatusCode.FailedInMovingByUnkownError:
                                Log.WriteLine("不明なエラーによって、移動に失敗したようです。");
                                Log.WriteLine("Visualizer開発者にご連絡ください。");
                                break;
                            case AgentStatusCode.FailedInRemovingOurTileByUnkownError:
                                Log.WriteLine("不明なエラーによって、自分のチームからタイルを取り除くことに失敗したようです。");
                                Log.WriteLine("Visualizer開発者にご連絡ください。");
                                break;
                            case AgentStatusCode.FailedInRemovingOpponentTileByUnkownError:
                                Log.WriteLine("不明なエラーによって、相手のチームからタイルを取り除くことに失敗したようです。");
                                Log.WriteLine("Visualizer開発者にご連絡ください。");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
