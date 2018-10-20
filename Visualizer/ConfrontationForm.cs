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
            for (int i = 0; i < 8; i++)
            {
                var task = Task.Run(() =>
                {
                    var id = FieldGenerator.GetRandomID(random.Next() + i);
                    var field_generator = new FieldGenerator(id);
                    var field = field_generator.Generate();
                    var agents = new Agents();
                    var coordinates = field_generator.AgentPositionGenerate();
                    agents[Team.A, AgentNumber.One].Position = coordinates[0];
                    agents[Team.A, AgentNumber.Two].Position = coordinates[1];
                    agents[Team.B, AgentNumber.One].Position = new Coordinate(MainForm.ComplementEnemysPosition(field, coordinates[0]));
                    agents[Team.B, AgentNumber.Two].Position = new Coordinate(MainForm.ComplementEnemysPosition(field, coordinates[1]));
                    var calc = new Calc(40, field, agents);
                    Invoke(new delegate1(Add), id, calc.Turn, calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                    while (calc.Turn < calc.MaxTurn)
                    {
                        TurnEnd(calc, log);
                        //Invoke(new delegate2(TurnEnd), calc, log);
                        try
                        {
                            Invoke(new delegate1(Edit), id, calc.Turn, calc.Field.TotalPoint(Team.A), calc.Field.TotalPoint(Team.B));
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Caught!");
                        }
                    }
                });
                tasks.Add(task);
            }
            return Task.WhenAll(tasks);
        }

        delegate void delegate1(string id, int turn, int orange, int lime);
        delegate void delegate2(Calc calc, Logger logger);

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
        }

        private void TurnEnd(Calc calc, Logger logger)
        {
            var action = new AgentsActivityData();
            if (MainForm.Bot[0] != null)
            {
                dynamic answer;
                lock (MainForm.Bot[0])
                {
                    MainForm.Bot[0].OurTeam = Team.A;
                    MainForm.Bot[0].Log = logger;
                    MainForm.Bot[0].Question(new Calc(calc));
                    answer = MainForm.Bot[0].Answer();
                }
                action[Team.A, AgentNumber.One] = answer[0];
                action[Team.A, AgentNumber.Two] = answer[1];
            }
            if (MainForm.Bot[1] != null)
            {
                dynamic answer;
                lock (MainForm.Bot[1])
                {
                    MainForm.Bot[1].OurTeam = Team.B;
                    MainForm.Bot[1].Log = logger;
                    MainForm.Bot[1].Question(new Calc(calc));
                    answer = MainForm.Bot[1].Answer();
                }
                action[Team.B, AgentNumber.One] = answer[0];
                action[Team.B, AgentNumber.Two] = answer[1];
            }
            calc.MoveAgent(action);
        }
    }
}
