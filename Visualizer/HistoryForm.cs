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
    public partial class HistoryForm : Form
    {
        public Calc Calc { get; set; }

        public HistoryForm()
        {
            InitializeComponent();
        }

        public HistoryForm(Calc calc)
        {
            InitializeComponent();

            Calc = calc;
            TurnTrackBar.Minimum = 0;
            TurnTrackBar.Maximum = calc.MaxTurn;
            TurnTrackBar.Value = 0;
            Showing();
        }

        private void HistoryForm_Resize(object sender, EventArgs e)
        {
            Showing();
        }

        private void TurnTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Showing();
        }

        private void Showing()
        {
            Show show;
            show = new Show(Calc, pictureBox1);
            show.Showing(TurnTrackBar.Value);
            if (TurnTrackBar.Value == 0)
            {
                InfoLabel.Text = "全" + TurnTrackBar.Maximum + "ターンのこの試合を振り返ってみるといい。\n"
                    + "このフィールドは0ターン目、つまり初期状態を示している。\n"
                    + "下にあるトラックバーをスライドすると表示されるターンが変更される。\n";
            }
            else
            {
                InfoLabel.Text = TurnTrackBar.Maximum + "ターン中" + TurnTrackBar.Value + "ターン目\n";
                var turn = TurnTrackBar.Value;
                InfoLabel.Text +=
                    "我がチームの領域ポイントは、"
                     + Calc.History[turn].Field.Sum(cell => (cell.IsTileOn[Team.A] == true) ? cell.Point : 0)
                     + "点になり、囲みポイントは、"
                     + Calc.History[turn].Field.Sum(cell => (cell.IsEnclosed[Team.A] == true) ? Math.Abs(cell.Point) : 0)
                     + "点になった。\n"
                     + "一方で、敵チームの領域ポイントは、"
                     + Calc.History[turn].Field.Sum(cell => (cell.IsTileOn[Team.B] == true) ? cell.Point : 0)
                     + "点になり、囲みポイントは、"
                     + Calc.History[turn].Field.Sum(cell => (cell.IsEnclosed[Team.B] == true) ? Math.Abs(cell.Point) : 0)
                     + "点になった。\n";
            }
            if (TurnTrackBar.Value == TurnTrackBar.Maximum)
            {
                InfoLabel.Text += "これにて、試合は終了した。";
            }
        }

    }
}
