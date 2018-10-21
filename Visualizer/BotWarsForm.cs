using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// ボット同士を戦わせます。
    /// </summary>
    public partial class BotWarsForm : Form
    {

        CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        CancellationToken CancellationToken;
        List<Task> tasks = new List<Task>();

        /// <summary>
        /// BotWarsForm を初期化します。
        /// </summary>
        public BotWarsForm()
        {
            InitializeComponent();
            DataGridView.Columns["Orange"].HeaderText = MainForm.BotName[0];
            DataGridView.Columns["Lime"].HeaderText = MainForm.BotName[1];
            CancellationToken = CancellationTokenSource.Token;
        }

        private void TurnEndButton_Click(object sender, EventArgs e)
        {
            tasks.Add(Run(CancellationToken));
        }

        private Task Run(CancellationToken cancellationToken)
        {
            var log = new Logger(new RichTextBox());
            var tasks = new List<Task>();
            var random = new Random();
            return Task.Run(() =>
            {
                var id = FieldGenerator.GetRandomID(random.Next());
                var field_generator = new FieldGenerator(id);
                var field = field_generator.Generate();
                var agents = new Agents();
                var coordinates = field_generator.AgentPositionGenerate(field);

                var bot = new dynamic[2];
                if (MainForm.BotName[0] != "Human")
                {
                    Assembly m = Assembly.LoadFrom(MainForm.BotPath[0]);
                    MatchCollection mc = Regex.Matches(MainForm.BotPath[0], @"^[A-Z]:\\(.*\\)+(?<file>.*).dll$");
                    foreach (Match match in mc)
                    {
                        bot[0] = Activator.CreateInstance(m.GetType("nitkagoshima_sysken.Procon29." + match.Groups["file"].Value + "." + match.Groups["file"].Value));
                    }
                }
                if (MainForm.BotName[1] != "Human")
                {
                    Assembly m = Assembly.LoadFrom(MainForm.BotPath[1]);
                    MatchCollection mc = Regex.Matches(MainForm.BotPath[1], @"^[A-Z]:\\(.*\\)+(?<file>.*).dll$");
                    foreach (Match match in mc)
                    {
                        bot[1] = Activator.CreateInstance(m.GetType("nitkagoshima_sysken.Procon29." + match.Groups["file"].Value + "." + match.Groups["file"].Value));
                    }
                }

                agents[Team.A, AgentNumber.One].Position = coordinates[0];
                agents[Team.A, AgentNumber.Two].Position = coordinates[1];
                agents[Team.B, AgentNumber.One].Position = new Coordinate(MainForm.ComplementEnemysPosition(field, coordinates[0]));
                agents[Team.B, AgentNumber.Two].Position = new Coordinate(MainForm.ComplementEnemysPosition(field, coordinates[1]));
                var calc = new Calc(MainForm.MaxTurn, field, agents);
                Invoke(new add_delegate(Add), id, calc.Turn, calc.Field.Sum(cell => cell.Point), calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                while (calc.Turn < calc.MaxTurn)
                {
                    var action = new AgentsActivityData();
                    if (bot[0] != null)
                    {
                        dynamic answer;
                        lock (bot[0])
                        {
                            bot[0].OurTeam = Team.A;
                            bot[0].Log = log;
                            bot[0].Question(new Calc(calc));
                            answer = bot[0].Answer();
                        }
                        action[Team.A, AgentNumber.One] = answer[0];
                        action[Team.A, AgentNumber.Two] = answer[1];
                    }
                    if (bot[1] != null)
                    {
                        dynamic answer;
                        lock (bot[1])
                        {
                            bot[1].OurTeam = Team.B;
                            bot[1].Log = log;
                            bot[1].Question(new Calc(calc));
                            answer = bot[1].Answer();
                        }
                        action[Team.B, AgentNumber.One] = answer[0];
                        action[Team.B, AgentNumber.Two] = answer[1];
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Canceled!");
                        return "Canceled";
                    }
                    calc.MoveAgent(action);
                    Invoke(new edit_delegate(Edit), id, calc.Turn, calc.Field.Sum(cell => cell.Point), calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                }
                Invoke(new end_delegate(End), id);
                return "Succeed";
            });
        }

        delegate void add_delegate(string id, int turn, int total, int orange, int lime);
        delegate void edit_delegate(string id, int turn, int total, int orange, int lime);
        delegate void end_delegate(string id);

        private void Add(string id, int turn, int total, int orange, int lime)
        {
            var orangepercent = ((double)orange / total).ToString("P");
            var limepercent = ((double)lime / total).ToString("P");
            DataGridView.Rows.Add(id, turn, total, orange, orangepercent, lime, limepercent);
        }

        private void Edit(string id, int turn, int total, int orange, int lime)
        {
            var row = DataGridView.Rows.Cast<DataGridViewRow>().First(row2 => row2.Cells[0].Value.ToString() == id);
            row.Cells["Turn"].Value = turn;
            row.Cells["Orange"].Value = orange;
            row.Cells["Lime"].Value = lime;
            var orangepercent = ((double)orange / total).ToString("P");
            var limepercent = ((double)lime / total).ToString("P");
            try
            {
                row.Cells["OrangePercent"].Value = orangepercent;
            }
            catch (Exception)
            {
                Console.WriteLine("CAUGHT!");
                throw;
            }
            row.Cells["LimePercent"].Value = limepercent;
            if (orange >= lime)
            {
                row.Cells["Orange"].Style.BackColor = Color.Orange;
                row.Cells["OrangePercent"].Style.BackColor = Color.Orange;
                row.Cells["Orange"].Style.ForeColor = Color.Black;
                row.Cells["OrangePercent"].Style.ForeColor = Color.Black;
            }
            else
            {
                row.Cells["Orange"].Style.BackColor = Color.FromArgb(32, 32, 32);
                row.Cells["OrangePercent"].Style.BackColor = Color.FromArgb(32, 32, 32);
                row.Cells["Orange"].Style.ForeColor = Color.LightGray;
                row.Cells["OrangePercent"].Style.ForeColor = Color.LightGray;
            }
            if (lime >= orange)
            {
                row.Cells["Lime"].Style.BackColor = Color.Lime;
                row.Cells["LimePercent"].Style.BackColor = Color.Lime;
                row.Cells["Lime"].Style.ForeColor = Color.Black;
                row.Cells["LimePercent"].Style.ForeColor = Color.Black;
            }
            else
            {
                row.Cells["Lime"].Style.BackColor = Color.FromArgb(32, 32, 32);
                row.Cells["LimePercent"].Style.BackColor = Color.FromArgb(32, 32, 32);
                row.Cells["Lime"].Style.ForeColor = Color.LightGray;
                row.Cells["LimePercent"].Style.ForeColor = Color.LightGray;
            }
        }

        private void End(string id)
        {
            var row = DataGridView.Rows.Cast<DataGridViewRow>().First(row2 => row2.Cells[0].Value.ToString() == id);
            row.Cells["Turn"].Style.ForeColor = Color.Red;
        }

        private void BotWarsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancellationTokenSource.Cancel();

            if (tasks.Count == 0) return;

            StreamWriter writer = new StreamWriter(MainForm.BotName[0] + " vs " + MainForm.BotName[1] + ".csv", true);

            foreach (var row in DataGridView.Rows.Cast<DataGridViewRow>())
            {
                try
                {
                    foreach (var cell in row.Cells.Cast<DataGridViewCell>())
                    {
                        writer.Write(cell.Value.ToString() + "\t");
                    }
                    writer.WriteLine(DateTime.Now);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Ignore!");
                }
            }
            writer.Close();
        }
    }
}
