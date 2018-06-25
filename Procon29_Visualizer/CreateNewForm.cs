using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procon29_Visualizer
{
    public partial class CreateNewForm : Form
    {
        private string selectPQRFile;
        private int maxTrun;

        public string SelectPQRFile { get => selectPQRFile; set => selectPQRFile = value; }
        public int MaxTrun { get => maxTrun; set => maxTrun = value; }

        public CreateNewForm()
        {
            InitializeComponent();
            //フォームが最大化されないようにする
            this.MaximizeBox = false;
            //フォームが最小化されないようにする
            this.MinimizeBox = false;
        }

    }
}
