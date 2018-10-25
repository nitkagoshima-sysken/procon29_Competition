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
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
        }

        public NameForm(Calc calc)
        {
            InitializeComponent();
            NameTextBox1.Text = calc.Agents[Team.A, AgentNumber.One].Name;
            NameTextBox2.Text = calc.Agents[Team.A, AgentNumber.Two].Name;
        }
    }
}
