using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public partial class ConfrontationForm : Form
    {
        public ConfrontationForm()
        {
            InitializeComponent();
        }

        private void TurnEndButton_Click(object sender, EventArgs e)
        {
            Run();
        }

        private Task Run()
        {
            var log = new Logger(new RichTextBox());
            var tasks = new List<Task>();
            var random = new Random();
            var task = Task.Run(() =>
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
                var calc = new Calc(40, field, agents);
                Invoke(new delegate1(Add), id, calc.Turn, calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                while (calc.Turn < calc.MaxTurn)
                {
                    try
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
                        calc.MoveAgent(action);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("caught!");
                        throw;
                    }
                    Invoke(new delegate1(Edit), id, calc.Turn, calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                }
            });
            return Task.WhenAll(task);
        }

        delegate void delegate1(string id, int turn, int orange, int lime);
        delegate void delegate2(Calc calc, Logger logger, dynamic[] bot);

        private void Add(string id, int turn, int orange, int lime)
        {
            DataGridView.Rows.Add(id, turn, orange, lime);
        }

        private void Edit(string id, int turn, int orange, int lime)
        {
            var row = DataGridView.Rows.Cast<DataGridViewRow>().First(row2 => row2.Cells[0].Value.ToString() == id);
            row.Cells[1].Value = turn;
            row.Cells[2].Value = orange;
            row.Cells[3].Value = lime;
            if (orange >= lime)
            {
                row.Cells[2].Style.BackColor = Color.Orange;
                row.Cells[2].Style.ForeColor = Color.Black;
            }
            else
            {
                row.Cells[2].Style.BackColor = Color.FromArgb(64,64,64);
                row.Cells[2].Style.ForeColor = Color.LightGray;
            }
            if (lime >= orange)
            {
                row.Cells[3].Style.BackColor = Color.Lime;
                row.Cells[3].Style.ForeColor = Color.Black;
            }
            else
            {
                row.Cells[3].Style.BackColor = Color.FromArgb(64, 64, 64);
                row.Cells[3].Style.ForeColor = Color.LightGray;
            }
        }

        private void TurnEnd(Calc calc, Logger logger, dynamic[] bot)
        {
            var action = new AgentsActivityData();
            if (bot[0] != null)
            {
                dynamic answer;
                lock (bot[0])
                {
                    bot[0].OurTeam = Team.A;
                    bot[0].Log = logger;
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
                    bot[1].Log = logger;
                    bot[1].Question(new Calc(calc));
                    answer = bot[1].Answer();
                }
                action[Team.B, AgentNumber.One] = answer[0];
                action[Team.B, AgentNumber.Two] = answer[1];
            }
            calc.MoveAgent(action);
        }
    }
}
